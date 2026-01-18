using FactoryControll.API.Areas.Autenticacao.Controllers;
using FactoryControll.API.Areas.Autenticacao.Models;
using FactoryControll.InfraData.Models.Autenticacao;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Moq;

namespace FactoryControll.Tests.UnitTests.Controllers
{
    public class AutenticacaoControllerTests
    {
        private readonly Mock<UserManager<User>> _userManagerMock;
        private readonly Mock<SignInManager<User>> _signInManagerMock;
        private readonly Mock<IConfiguration> _configMock;
        private readonly AutenticacaoController _controller;

        public AutenticacaoControllerTests()
        {
            var userStoreMock = new Mock<IUserStore<User>>();
            _userManagerMock = new Mock<UserManager<User>>(userStoreMock.Object, null, null, null, null, null, null, null, null);
            var contextAccessor = new Mock<Microsoft.AspNetCore.Http.IHttpContextAccessor>();
            var userPrincipalFactory = new Mock<IUserClaimsPrincipalFactory<User>>();
            _signInManagerMock = new Mock<SignInManager<User>>(
                _userManagerMock.Object,
                contextAccessor.Object,
                userPrincipalFactory.Object,
                null, null, null, null
            );
            _configMock = new Mock<IConfiguration>();
            _controller = new AutenticacaoController(_userManagerMock.Object, _configMock.Object, _signInManagerMock.Object);
        }

        [Fact]
        public async Task Login_ReturnsUnauthorized_WhenUserNotFound()
        {
            // Arrange
            var loginDto = new UserLoginDto { Email = "test@test.com", Password = "password" };
            _userManagerMock.Setup(x => x.FindByEmailAsync(loginDto.Email)).ReturnsAsync((User)null);

            // Act
            var result = await _controller.Login(loginDto);

            // Assert
            Assert.IsType<UnauthorizedResult>(result);
        }

        [Fact]
        public async Task Login_ReturnsUnauthorized_WhenPasswordInvalid()
        {
            // Arrange
            var user = new User { Email = "test@test.com", UserName = "test" };
            var loginDto = new UserLoginDto { Email = "test@test.com", Password = "wrongpassword" };
            _userManagerMock.Setup(x => x.FindByEmailAsync(loginDto.Email)).ReturnsAsync(user);
            _userManagerMock.Setup(x => x.CheckPasswordAsync(user, loginDto.Password)).ReturnsAsync(false);

            // Act
            var result = await _controller.Login(loginDto);

            // Assert
            Assert.IsType<UnauthorizedResult>(result);
        }

        [Fact]
        public async Task Login_ReturnsOk_WithToken_WhenCredentialsValid()
        {
            // Arrange
            var user = new User { Email = "test@test.com", UserName = "test" };
            var loginDto = new UserLoginDto { Email = "test@test.com", Password = "password" };
            _userManagerMock.Setup(x => x.FindByEmailAsync(loginDto.Email)).ReturnsAsync(user);
            _userManagerMock.Setup(x => x.CheckPasswordAsync(user, loginDto.Password)).ReturnsAsync(true);
            _userManagerMock.Setup(x => x.GetRolesAsync(user)).ReturnsAsync(new List<string> { "User" });

            _configMock.Setup(x => x["Jwt:Issuer"]).Returns("issuer");
            _configMock.Setup(x => x["Jwt:Audience"]).Returns("audience");
            _configMock.Setup(x => x["Jwt:Key"]).Returns("6CP9fYFJkH3wAf9gHpbXf+JrXYM9gAvyUwZ++oFaPxo8eJIl3x8vNnKLDLeWJK1OwVG7VnM9sPAxM+HyObURZQ==\r\n");

            // Act
            var result = await _controller.Login(loginDto);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.NotNull(okResult.Value);
            var valueType = okResult.Value.GetType();
            Assert.NotNull(valueType.GetProperty("token"));
            Assert.NotNull(valueType.GetProperty("expiration"));
        }
    }

}
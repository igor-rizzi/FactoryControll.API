using FactoryControll.API.Areas.Autenticacao.Models;
using FactoryControll.InfraData.Models.Autenticacao;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Linq;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace FactoryControll.API.Areas.Autenticacao.Controllers
{
    [Authorize(Roles = "Administrador")]
    [ApiController]
    [Route("api/[controller]")]
    public class AutenticacaoController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly IConfiguration _config;
        private readonly SignInManager<User> _signInManager;

        public AutenticacaoController(UserManager<User> userManager, IConfiguration config, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _config = config;
            _signInManager = signInManager;
        }

        [HttpPost("login")]
        [AllowAnonymousAttribute]
        public async Task<IActionResult> Login(UserLoginDto model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null || !await _userManager.CheckPasswordAsync(user, model.Password))
                return Unauthorized();

            var roles = await _userManager.GetRolesAsync(user);
            var authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

            authClaims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                expires: DateTime.Now.AddHours(2),
                claims: authClaims,
                signingCredentials: new SigningCredentials(
                    new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"])),
                    SecurityAlgorithms.HmacSha256)
            );

            return Ok(new
            {
                token = new JwtSecurityTokenHandler().WriteToken(token),
                expiration = token.ValidTo
            });
        }

        [HttpPost("recuperar-senha")]
        [AllowAnonymousAttribute]
        public async Task<IActionResult> RecuperarSenha(UserLoginDto model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);

            if (user == null)
                return Ok();

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);

            var encodedToken = Uri.EscapeDataString(token);

            var resetLink = $"{_config["FrontEnd:ResetPasswordUrl"]}?email={model.Email}&token={encodedToken}";

            // Aqui você envia o email (SMTP, SendGrid, etc)
            // emailService.Send(model.Email, resetLink);

            return Ok();
        }
    }
}

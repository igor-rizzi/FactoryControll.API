using AutoMapper;
using FactoryControll.API.Areas.Administracao.Controllers;
using FactoryControll.API.Areas.Administracao.Models;
using FactoryControll.Application.Interfaces.Services;
using FactoryControll.Domain.Entities.Administracao;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace FactoryControll.Tests.UnitTests.Controllers
{
    public class CargoControllerTests
    {
        private readonly Mock<IBaseCrudService<Cargo>> _crudServiceMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly CargoController _controller;

        public CargoControllerTests()
        {
            _crudServiceMock = new Mock<IBaseCrudService<Cargo>>();
            _mapperMock = new Mock<IMapper>();

            // Usando reflexão para injetar dependências protegidas
            _controller = new CargoController();
            typeof(CargoController)
                .GetField("_crudService", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
                ?.SetValue(_controller, _crudServiceMock.Object);
            typeof(CargoController)
                .GetField("_mapper", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
                ?.SetValue(_controller, _mapperMock.Object);
        }

        [Fact]
        public async Task Inserir_DeveRetornarOk_QuandoCargoValido()
        {
            // Arrange
            var model = new CargoModel { Nome = "Analista", Descricao = "Analista de Sistemas" };
            var cargo = new Cargo { Nome = model.Nome, Descricao = model.Descricao };
            _mapperMock.Setup(m => m.Map<Cargo>(model)).Returns(cargo);
            _crudServiceMock.Setup(s => s.InsertAndSaveAsync(It.IsAny<Cargo>())).ReturnsAsync(cargo);

            // Act
            var resultado = await _controller.Inserir(model);

            // Assert
            Assert.IsType<OkObjectResult>(resultado);
        }

        [Fact]
        public async Task Editar_DeveRetornarOk_QuandoCargoEditado()
        {
            // Arrange
            var model = new CargoModel { Id = 1, Nome = "Gerente", Descricao = "Gerente de Projetos" };
            var cargo = new Cargo { Id = 1, Nome = model.Nome, Descricao = model.Descricao };
            _mapperMock.Setup(m => m.Map<Cargo>(model)).Returns(cargo);
            _crudServiceMock.Setup(s => s.UpdateAndSaveAsync(It.IsAny<Cargo>())).ReturnsAsync(cargo);

            // Act
            var resultado = await _controller.Editar(model);

            // Assert
            Assert.IsType<OkObjectResult>(resultado);
        }

        [Fact]
        public async Task Deletar_DeveRetornarOk_QuandoCargoDeletado()
        {
            // Arrange
            long id = 1;
            _crudServiceMock.Setup(s => s.DeleteAndSaveAsync(id)).Returns(Task.CompletedTask);

            // Act
            var resultado = await _controller.Deletar(id);

            // Assert
            Assert.IsType<OkResult>(resultado);
        }

        [Fact]
        public async Task Listar_DeveRetornarOk_ComListaDeCargos()
        {
            // Arrange
            var cargos = new List<Cargo>
        {
            new Cargo { Id = 1, Nome = "Analista", Descricao = "Analista de Sistemas" },
            new Cargo { Id = 2, Nome = "Gerente", Descricao = "Gerente de Projetos" }
        };
            _crudServiceMock.Setup(s => s.GetAll()).Returns(cargos.AsQueryable());
            _mapperMock.Setup(m => m.Map<List<CargoModel>>(It.IsAny<IEnumerable<Cargo>>()))
                .Returns(new List<CargoModel>
                {
                new CargoModel { Id = 1, Nome = "Analista", Descricao = "Analista de Sistemas" },
                new CargoModel { Id = 2, Nome = "Gerente", Descricao = "Gerente de Projetos" }
                });

            // Act
            var resultado = await _controller.Listar();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(resultado);
            Assert.IsType<List<CargoModel>>(okResult.Value);
        }
    }
}
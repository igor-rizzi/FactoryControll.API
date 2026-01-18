using AutoMapper;
using FactoryControll.API.Areas.Colaboradores.Models;
using FactoryControll.API.Common;
using FactoryControll.Application.Interfaces.Services;
using FactoryControll.Application.Models;
using FactoryControll.Application.Services;
using FactoryControll.Domain.Common;
using FactoryControll.Domain.Entities.Colaboradores;
using FactoryControll.InfraData.Models.Autenticacao;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace FactoryControll.API.Areas.Colaboradores.Controllers
{
    [Authorize(Roles = "Administrador")]
    [ApiController]
    [Route("api/[controller]")]
    public class ColaboradorController : CrudController<Colaborador, ColaboradorModel>
    {
        private readonly ICrudService<Colaborador> _crudColaboradorService;
        private readonly IColaboradorService _colaboradorService;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;

        public ColaboradorController(ICrudService<Colaborador> colaborador,
            IMapper mapper,
            UserManager<User> userManager,
            IColaboradorService colaboradorService)
        {
            _crudColaboradorService = colaborador;
            _mapper = mapper;
            _userManager = userManager;
            _colaboradorService = colaboradorService;
        }

        [HttpPost("inserir")]
        public override async Task<IActionResult> Inserir(ColaboradorModel model)
        {
            try
            {
                if (!Validar(model))
                {
                    return Json(new { Sucesso = false, Dados = model });
                }

                model.Id = 0;
                var entity = Mapper.Map<Colaborador>(model);

                await CrudService.InsertAndSaveAsync(entity);
                model.Id = entity.Id;

                var user = await _userManager.FindByEmailAsync(model.Email);

                if (user != null)
                {
                    entity.UsuarioId = user.UsuarioAppId;
                }
                else
                {
                    var newUser = new User
                    {
                        UserName = model.Email,
                        Email = model.Email,
                    };
                    var result = await _userManager.CreateAsync(newUser, "SenhaPadrão123!");

                    if (!result.Succeeded)
                    {
                        return Json(new { Sucesso = false, Mensagem = "Erro ao criar usuário: " + string.Join(", ", result.Errors.Select(e => e.Description)) });
                    }
                    await _userManager.AddToRoleAsync(newUser, "Colaborador");

                    _colaboradorService.CriarColaboradorAsync(new CriarColaboradorDto
                    {
                        Nome = model.Nome,
                        Email = model.Email
                    });

                    entity.UsuarioId = newUser.UsuarioAppId;
                }


                return Json(new { Sucesso = true, Dados = model });
            }
            catch (Exception ex)
            {
                if (ex is DBConcurrencyException) throw;

                throw new Exception(string.Format("Houve uma falha ao salvar as alterações!"), ex);
            }
        }

        [HttpPost("inserir-usuario-colaborador")]
        public async Task<IActionResult> InserirUsuarioColaborador(ColaboradorModel model)
        {
            return Ok();
        }
    }
}

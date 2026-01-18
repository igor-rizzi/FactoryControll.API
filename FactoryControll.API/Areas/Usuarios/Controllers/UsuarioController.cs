using FactoryControll.API.Areas.Autenticacao.Models;
using FactoryControll.API.Areas.Usuarios.Models;
using FactoryControll.API.Common;
using FactoryControll.Domain.Common;
using FactoryControll.Domain.Entities.Usuarios;
using FactoryControll.InfraData.Models.Autenticacao;
using FactoryControll.InfraFramework.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Extensions;

namespace FactoryControll.API.Areas.Usuarios.Controllers
{
    [Authorize(Roles = "Administrador")]
    [ApiController]
    [Route("api/[controller]")]
    public class UsuarioController : 
        CrudController<Usuario, UsuarioModel>
    {
        private readonly UserManager<User> _userManager;

        public UsuarioController(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        [HttpPost("deletar")]
        public override async Task<IActionResult> Deletar(long key)
        {
            var entity = await CrudService.GetByIdAsync(key);

            if (entity == null)
            {
                return NotFound("Usuário não encontrado!");
            }

            if (entity.UserLoginId != null)
            {
                await _userManager.DeleteAsync(await _userManager.FindByIdAsync(entity.UserLoginId));
            }

            await CrudService.DeleteAndSaveAsync(key);
            return Ok("Usuário deletado com sucesso!");
        }

        [HttpPost("inserir")]
        public override async Task<IActionResult> Inserir(UsuarioModel model)
        {
            if (!Validar(model))
            {
                return Json(new { Sucesso = false, Dados = model });
            }

            model.Id = 0;
            var entity = Mapper.Map<Usuario>(model);

            await CrudService.InsertAndSaveAsync(entity);
            model.Id = entity.Id;

            return (CriarUsuario(entity) is Task<IActionResult> taskResult) ? await taskResult : BadRequest("Erro ao inserir usuário.");
        }

        private async Task<IActionResult> CriarUsuario(Usuario usuarioApp)
        {
            var userExists = await _userManager.FindByEmailAsync(usuarioApp.Email);
            if (userExists != null)
                return BadRequest("Usuário já existe!");

            var user = new User
            {
                UsuarioAppId = usuarioApp.Id,
                Email = usuarioApp.Email,
                UserName = usuarioApp.Nome.Sanitize(),
                SecurityStamp = Guid.NewGuid().ToString(),
                PasswordHash = _userManager.PasswordHasher.HashPassword(null, usuarioApp.Senha)
            };

            var result = await _userManager.CreateAsync(user);

            if (!result.Succeeded)
                return BadRequest(result.Errors);

            usuarioApp.UserLoginId = user.Id;
            await CrudService.UpdateAndSaveAsync(usuarioApp);

            await _userManager.AddToRoleAsync(user, usuarioApp.TipoUsuario.GetDisplayName());

            return Ok("Usuário registrado com sucesso!");
        }
    }
}

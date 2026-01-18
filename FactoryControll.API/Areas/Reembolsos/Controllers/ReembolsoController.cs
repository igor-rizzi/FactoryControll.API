using FactoryControll.API.Areas.Reembolsos.Models;
using FactoryControll.API.Common;
using FactoryControll.Domain.Common;
using FactoryControll.Domain.Entities.Reembolsos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace FactoryControll.API.Areas.Reembolsos.Controllers
{
    [Authorize(Roles = "Administrador")]
    [ApiController]
    [Route("api/[controller]")]
    public class ReembolsoController : CrudController<Reembolso, ReembolsoModel>
    {

        public ReembolsoController()
        {
            
        }

        [Authorize(Policy = "ReembolsoCriar")]
        [HttpPost("inserir")]
        public override async Task<IActionResult> Inserir(ReembolsoModel model)
        {
            try
            {
                if (!Validar(model))
                {
                    return Json(new { Sucesso = false, Dados = model });
                }

                model.Id = 0;
                var entity = Mapper.Map<Reembolso>(model);

                await CrudService.InsertAndSaveAsync(entity);
                model.Id = entity.Id;

                return Json(new { Sucesso = true, Dados = model });
            }
            catch (Exception ex)
            {
                if (ex is DBConcurrencyException) throw;

                throw new Exception(string.Format("Houve uma falha ao salvar as alterações!"), ex);
            }
        }
    }
}

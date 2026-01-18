using AutoMapper;
using FactoryControll.Application.Interfaces;
using FactoryControll.Application.Interfaces.Services;
using FactoryControll.Domain.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace FactoryControll.API.Common
{
    public abstract class CrudController<TEntity, TModel> : Controller
        where TEntity : Entity
        where TModel : IModel
    {
        private IBaseCrudService<TEntity> _crudService = null!;
        private IMapper _mapper = null!;

        protected IBaseCrudService<TEntity> CrudService =>
            _crudService ??= HttpContext.RequestServices.GetRequiredService<IBaseCrudService<TEntity>>();

        protected IMapper Mapper =>
            _mapper ??= HttpContext.RequestServices.GetRequiredService<IMapper>();

        protected virtual bool Validar(TModel model) => ModelState.IsValid;

        [HttpPost("deletar")]
        public virtual async Task<IActionResult> Deletar(long key)
        {
            try
            {
                await CrudService.DeleteAndSaveAsync(key);

                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message, inativar = false });
            }
        }

        [HttpGet("editar/{id}")]
        public virtual async Task<IActionResult> Editar(long id)
        {
            var entity = await CrudService.GetAsNoTrackingAsync(id);

            return Ok(Mapper.Map<TModel>(entity));
        }

        [HttpPost("editar")]
        public virtual async Task<IActionResult> Editar(TModel model)
        {
            try
            {
                if (!Validar(model))
                {
                    return Json(model);
                }

                var entity = Mapper.Map<TEntity>(model);

                var retorno = await CrudService.UpdateAndSaveAsync(entity);

                return Ok(model);
            }
            catch (Exception ex)
            {
                if (ex is DbUpdateConcurrencyException) throw;

                throw new Exception(string.Format("Houve uma falha ao salvar as alterações!"), ex);
            }

        }

        [HttpPost("inserir")]
        public virtual async Task<IActionResult> Inserir(TModel model)
        {
            try
            {
                if (!Validar(model))
                {
                    return Json(new { Sucesso = false, Dados = model });
                }

                model.Id = 0;
                var entity = Mapper.Map<TEntity>(model);

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

        [HttpGet("listar")]
        public virtual async Task<IActionResult> Listar([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            var query = CrudService.Listar().AsQueryable();

            var total = query.CountAsync();

            var items = await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return Json(new
            {
                items,
                totalCount = total,
                page,
                pageSize
            });
        }
    }
}

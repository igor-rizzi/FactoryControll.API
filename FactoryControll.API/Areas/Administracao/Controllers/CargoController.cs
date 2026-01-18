using FactoryControll.API.Areas.Administracao.Models;
using FactoryControll.API.Common;
using FactoryControll.Domain.Entities.Administracao;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FactoryControll.API.Areas.Administracao.Controllers
{
    [Authorize(Roles = "Administrador")]
    [ApiController]
    [Route("api/[controller]")]
    public class CargoController : CrudController<Cargo, CargoModel>
    {
        public CargoController()
        {
            
        }
    }
}

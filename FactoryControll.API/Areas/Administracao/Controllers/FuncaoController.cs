using AutoMapper;
using FactoryControll.API.Areas.Administracao.Models;
using FactoryControll.API.Common;
using FactoryControll.Application.Interfaces.Services;
using FactoryControll.Domain.Entities.Administracao;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FactoryControll.API.Areas.Administracao.Controllers
{
    [Authorize(Roles = "Administrador")]
    [ApiController]
    [Route("api/[controller]")]
    public class FuncaoController : CrudController<Funcao, FuncaoModel>
    {
        private readonly IBaseCrudService<Funcao> _funcaoService;
        private readonly IMapper _mapper;

        public FuncaoController(IBaseCrudService<Funcao> funcaoService, IMapper mapper)
        {
            _funcaoService = funcaoService;
            _mapper = mapper;
        }

    }
}

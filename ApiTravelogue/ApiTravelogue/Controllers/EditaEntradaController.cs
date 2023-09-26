using ApiTravelogue.Models;
using ApiTravelogue.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace ApiTravelogue.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EditaEntradaController : ControllerBase
    {
        private readonly EntradaServices _entradaServices;

        public EditaEntradaController(EntradaServices entradaServices)
        {
            _entradaServices = entradaServices;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Entrada>> GetEntrada(string id)
        {
            var entrada = await _entradaServices.GetEntrada(id);

            if (entrada == null)
            {
                return NotFound("Entrada não encontrada");
            }
            else
            {
                return Ok(entrada);
            }
        }

    }
}

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
    public class ImagemController : ControllerBase
    {
        private readonly EntradaServices _entradaServices;

        public ImagemController(EntradaServices entradaServices)
        {
            _entradaServices = entradaServices;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Entrada>> GetImagens(string id)
        {
            var imagem = await _entradaServices.GetImagens(id);

            if (imagem == null)
            {
                return NotFound("Imagens não encontradas");
            }
            else
            {
                return Ok(imagem);
            }
        }

    }
}

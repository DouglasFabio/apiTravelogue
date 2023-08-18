using ApiTravelogue.Models;
using ApiTravelogue.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace ApiTravelogue.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ViagemController : ControllerBase
    {
        private readonly ViagemServices _viagemServices;

        public ViagemController(ViagemServices viagemServices)
        {
            _viagemServices = viagemServices;
        }

        [HttpGet]
        public async Task<List<Viagem>> GetViagens() => await _viagemServices.GetAsync();

        [HttpGet("{id}")]
        public async Task<ActionResult<Viagem>> GetViagem(string id)
        {
            var viagem = await _viagemServices.GetAsync(id);

            if (viagem == null)
            {
                return NotFound("Viagem não encontrada.");
            }

            return viagem;
        }

        [HttpPost]
        public async Task<Viagem> PostViagem(Viagem viagem)
        {
            await _viagemServices.CreateAsync(viagem);

            return viagem;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(string id, [FromBody] Viagem viagem)
        {
            if (viagem == null)
            {
                return NotFound("Viagem não encontrada.");
            }
            await _viagemServices.UpdateAsync(id, viagem);
            return Ok("Viagem atualizada com sucesso!");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            await _viagemServices.DeleteAsync(id);
            return Ok("Viagem deletada com sucesso!");
        }

    }
}

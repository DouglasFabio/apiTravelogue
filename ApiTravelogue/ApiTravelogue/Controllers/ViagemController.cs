using ApiTravelogue.Models;
using ApiTravelogue.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using System.Diagnostics;

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

        [HttpPost]
        public async Task<Viagem> PostViagem(Viagem viagem)
        {
            await _viagemServices.CreateAsync(viagem);

            return viagem;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(string id, [FromBody] Viagem viagem)
        {
            await _viagemServices.UpdateAsync(id, viagem);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            await _viagemServices.DeleteAsync(id);
            return NoContent();
        }

    }
}

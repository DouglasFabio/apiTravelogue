using ApiTravelogue.Models;
using ApiTravelogue.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
    }
}

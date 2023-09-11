﻿using ApiTravelogue.Models;
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
    public class EntradaController : ControllerBase
    {
        private readonly EntradaServices _entradaServices;

        public EntradaController(EntradaServices entradaServices)
        {
            _entradaServices = entradaServices;
        }

        [HttpGet]
        public async Task<List<Entrada>> GetEntradas() => await _entradaServices.GetEntradas();

        [HttpGet("{id}")]
        public async Task<ActionResult<Entrada>> GetEntrada(string id)
        {
            var entrada = await _entradaServices.GetAsync(id);
            var imagem = await _entradaServices.GetImagens(id);

            if (entrada == null)
            {
                return NotFound("Entrada não encontrada.");
            }
            else if (imagem == null)
            {
                return NotFound("Imagens não encontradas");
            }else if(entrada != null)
            {
                return Ok(entrada);
            }
            else
            {
                return Ok(imagem);
            }

           
        }

        [HttpPost]
        public async Task<Entrada> PostEntrada(Entrada entrada)
        {
            await _entradaServices.CreateAsync(entrada);

            return entrada;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(string id, [FromBody] Entrada entrada)
        {
            if (entrada == null)
            {
                return NotFound("Viagem não encontrada.");
            }
            await _entradaServices.UpdateAsync(id, entrada);
            return Ok("Viagem atualizada com sucesso!");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            await _entradaServices.DeleteAsync(id);
            return Ok("Viagem deletada com sucesso!");
        }

    }
}

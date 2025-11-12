using DavinTI.Application.DTOs;
using DavinTI.Application.Interfaces.Service;
using DavinTI.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DavinTI.Api.Controllers {
    [ApiController]
    [Route("api/[controller]")]
    public class ContatoController : ControllerBase {
        private readonly IContatoService _contatoService;

        public ContatoController(IContatoService contatoService) {
            _contatoService = contatoService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll() {
            var contatos = await _contatoService.GetAllAsync();
            return Ok(contatos);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id) {
            var contato = await _contatoService.GetByIdAsync(id);
            if (contato == null)
                return NotFound(new { message = "Contato não encontrado." });

            return Ok(contato);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ContatoCreateDto dto) {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var novoContato = await _contatoService.CreateAsync(dto);

            return CreatedAtAction(
                nameof(GetById),
                new { id = novoContato.IdContato },
                novoContato
            );
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update([FromBody] ContatoUpdateDto dto) {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var atualizado = await _contatoService.UpdateAsync(dto);
            if (atualizado == null)
                return NotFound(new { message = "Contato não encontrado." });

            return Ok(atualizado);
        }



        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id) {
            await _contatoService.DeleteAsync(id);
            return NoContent();
        }

        [HttpGet("comTelefones")]
        public async Task<IActionResult> GetContatosComTelefones() {
            var contatos = await _contatoService.GetAllComTelefonesAsync();
            return Ok(contatos);
        }
    }
}

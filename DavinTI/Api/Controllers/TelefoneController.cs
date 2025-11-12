using DavinTI.Application.DTOs;
using DavinTI.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DavinTI.Api.Controllers {
    [ApiController]
    [Route("api/[controller]")]
    public class TelefoneController : ControllerBase {
        private readonly ITelefoneService _service;

        public TelefoneController(ITelefoneService service) {
            _service = service;
        }

        [HttpGet("contato/{idContato:int}")]
        public async Task<IActionResult> GetAllByContato(int idContato) {
            var telefones = await _service.GetAllAsync(idContato);
            if (!telefones.Any())
                return NotFound(new { message = "Nenhum telefone encontrado para este contato." });

            return Ok(telefones);
        }

        [HttpGet("{id:int}/contato/{idContato:int}")]
        public async Task<IActionResult> GetById(int id, int idContato) {
            var telefone = await _service.GetByIdAsync(id, idContato);
            if (telefone == null)
                return NotFound(new { message = "Telefone não encontrado." });

            return Ok(telefone);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] TelefoneCreateDto dto) {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var telefone = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById),
                new { id = telefone.Id, idContato = telefone.IdContato },
                telefone);
        }

        [HttpPut("{id:int}/contato/{idContato:int}")]
        public async Task<IActionResult> Update(int id, int idContato, [FromBody] TelefoneUpdateDto dto) {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (id != dto.Id || idContato != dto.IdContato)
                return BadRequest(new { message = "IDs da rota e do corpo não coincidem." });

            var atualizado = await _service.UpdateAsync(dto);
            if (atualizado == null)
                return NotFound(new { message = "Telefone não encontrado para atualização." });

            return Ok(atualizado);
        }

        [HttpDelete("{id:int}/contato/{idContato:int}")]
        public async Task<IActionResult> Delete(int id, int idContato) {
            await _service.DeleteAsync(id, idContato);
            return NoContent();
        }
    }
}
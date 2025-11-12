using DavinTI.Application.DTOs;
using DavinTI.Application.Interfaces;
using DavinTI.Domain.Entities;
using DavinTI.Domain.Interfaces;

namespace DavinTI.Application.Services {
    public class TelefoneService : ITelefoneService {
        private readonly ITelefoneRepository _repo;

        public TelefoneService(ITelefoneRepository repo) {
            _repo = repo;
        }

        public async Task<IEnumerable<TelefoneReadDto>> GetAllAsync(int idContato) {
            var telefones = await _repo.GetAllAsync(idContato);
            return telefones.Select(t => new TelefoneReadDto {
                Id = t.Id,
                IdContato = t.IdContato,
                Numero = t.Numero
            });
        }

        public async Task<TelefoneReadDto?> GetByIdAsync(int id, int idContato) {
            var telefone = await _repo.GetByIdAsync(id, idContato);
            if (telefone == null)
                return null;

            return new TelefoneReadDto {
                Id = telefone.Id,
                IdContato = telefone.IdContato,
                Numero = telefone.Numero
            };
        }

        public async Task<TelefoneReadDto> CreateAsync(TelefoneCreateDto dto) {
            var telefone = new Telefone(dto.IdContato, dto.Numero);
            await _repo.CreateAsync(telefone);

            return new TelefoneReadDto {
                Id = telefone.Id,
                IdContato = telefone.IdContato,
                Numero = telefone.Numero
            };
        }

        public async Task<TelefoneReadDto?> UpdateAsync(TelefoneUpdateDto dto) {
            var existente = await _repo.GetByIdAsync(dto.Id, dto.IdContato);
            if (existente == null)
                return null;

            existente.AtualizarNumero(dto.Numero);
            var atualizado = await _repo.UpdateAsync(existente);

            return atualizado == null ? null : new TelefoneReadDto {
                Id = atualizado.Id,
                IdContato = atualizado.IdContato,
                Numero = atualizado.Numero
            };
        }

        public async Task DeleteAsync(int id, int idContato) {
            await _repo.DeleteAsync(id, idContato);
        }
    }
}
using DavinTI.Application.DTOs;
using DavinTI.Application.Interfaces.Service;
using DavinTI.Domain.Entities;
using DavinTI.Domain.Repository;

namespace DavinTI.Services {
    public class ContatoService : IContatoService {
        private readonly IContatoRepository _repo;

        public ContatoService(IContatoRepository repo) {
            _repo = repo;
        }

        public async Task<IEnumerable<ContatoReadDto>> GetAllAsync() {
            var contatos = await _repo.GetAllAsync();
            return contatos.Select(c => new ContatoReadDto {
                IdContato = c.IdContato,
                Nome = c.Nome,
                Idade = c.Idade
            });
        }

        public async Task<ContatoReadDto?> GetByIdAsync(int id) {
            var c = await _repo.GetByIdAsync(id);
            if (c == null) return null;
            return new ContatoReadDto { IdContato = c.IdContato, Nome = c.Nome, Idade = c.Idade };
        }

        public async Task<ContatoReadDto> CreateAsync(ContatoCreateDto dto) {
            var contato = new Contato(dto.Nome, dto.Idade);

            if (dto.Telefones != null && dto.Telefones.Any()) {
                foreach (var t in dto.Telefones)
                    contato.AdicionarTelefone(new Telefone(contato.IdContato,t.Numero));
            }

            await _repo.AddAsync(contato);

            return new ContatoReadDto {
                IdContato = contato.IdContato,
                Nome = contato.Nome,
                Idade = contato.Idade
            };
        }

        public async Task<ContatoReadDto?> UpdateAsync(ContatoUpdateDto dto) {
            var contato = await _repo.GetByIdComTelefonesAsync(dto.IdContato);
            if (contato == null) return null;

            contato.AtualizarNome(dto.Nome);
            contato.AtualizarIdade(dto.Idade);

            var existentes = contato.Telefones.ToList();

            foreach (var tel in dto.Telefones) {
                var e = existentes.FirstOrDefault(t => t.Id == tel.Id);
                if (e != null)
                    e.AtualizarNumero(tel.Numero);
                else
                    contato.AdicionarTelefone(new Telefone(contato.IdContato, tel.Numero));
            }

            foreach (var tel in existentes) {
                if (!dto.Telefones.Any(t => t.Id == tel.Id))
                    contato.RemoverTelefone(tel);
            }

            await _repo.UpdateAsync(contato);

            return new ContatoReadDto { IdContato = contato.IdContato, Nome = contato.Nome, Idade = contato.Idade };
        }

        public async Task DeleteAsync(int id) {
            var contato = await _repo.GetByIdAsync(id);
            if (contato == null) throw new KeyNotFoundException("Contato não encontrado.");
            await _repo.DeleteAsync(id);
        }

        public async Task<IEnumerable<ContatoComTelefonesDto>> GetAllComTelefonesAsync() {
            var contatos = await _repo.GetAllComTelefonesAsync();
            return contatos.Select(c => new ContatoComTelefonesDto {
                IdContato = c.IdContato,
                Nome = c.Nome,
                Idade = c.Idade,
                Telefones = c.Telefones.Select(t => new TelefoneReadDto { Id = t.Id, Numero = t.Numero }).ToList()
            });
        }
    }
}

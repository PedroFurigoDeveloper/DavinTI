using DavinTI.Application.DTOs;

namespace DavinTI.Application.Interfaces.Service {
    public interface IContatoService {
        Task<IEnumerable<ContatoReadDto>> GetAllAsync();
        Task<ContatoReadDto?> GetByIdAsync(int id);
        Task<ContatoReadDto> CreateAsync(ContatoCreateDto dto);
        Task<ContatoReadDto?> UpdateAsync(ContatoUpdateDto dto);
        Task DeleteAsync(int id);
        Task<IEnumerable<ContatoComTelefonesDto>> GetAllComTelefonesAsync();
    }
}

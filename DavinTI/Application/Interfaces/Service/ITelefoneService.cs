using DavinTI.Application.DTOs;

namespace DavinTI.Application.Interfaces {
    public interface ITelefoneService {
        Task<IEnumerable<TelefoneReadDto>> GetAllAsync(int idContato);
        Task<TelefoneReadDto?> GetByIdAsync(int id, int idContato);
        Task<TelefoneReadDto> CreateAsync(TelefoneCreateDto dto);
        Task<TelefoneReadDto?> UpdateAsync(TelefoneUpdateDto dto);
        Task DeleteAsync(int id, int idContato);
    }
}
using DavinTI.Domain.Entities;

namespace DavinTI.Domain.Interfaces {
    public interface ITelefoneRepository {
        Task<IEnumerable<Telefone>> GetAllAsync(int idContato);
        Task<Telefone?> GetByIdAsync(int id, int idContato);
        Task<Telefone> CreateAsync(Telefone telefone);
        Task<Telefone?> UpdateAsync(Telefone telefone);
        Task<bool> DeleteAsync(int id, int idContato);
    }
}
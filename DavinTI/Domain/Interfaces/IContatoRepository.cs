using DavinTI.Domain.Entities;

namespace DavinTI.Domain.Repository {
    public interface IContatoRepository {
        Task<IEnumerable<Contato>> GetAllAsync();
        Task<Contato?> GetByIdAsync(int id);
        Task<Contato?> GetByIdComTelefonesAsync(int id);
        Task<IEnumerable<Contato>> GetAllComTelefonesAsync();
        Task AddAsync(Contato contato);
        Task UpdateAsync(Contato contato);
        Task DeleteAsync(int id);
    }
}

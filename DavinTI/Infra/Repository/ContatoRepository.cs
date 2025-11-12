using DavinTI.Data;
using DavinTI.Domain.Entities;
using DavinTI.Domain.Repository;
using Microsoft.EntityFrameworkCore;

namespace DavinTI.Infra.Repository {
    public class ContatoRepository : IContatoRepository {
        private readonly DavinTIContext _context;

        public ContatoRepository(DavinTIContext context) {
            _context = context;
        }

        public async Task<IEnumerable<Contato>> GetAllAsync() {
            return await _context.Contato
                .AsNoTracking()
                .Include(c => c.Telefones)
                .ToListAsync();
        }

        public async Task<Contato?> GetByIdAsync(int id) {
            return await _context.Contato
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.IdContato == id);
        }

        public async Task AddAsync(Contato contato) {
            await _context.Contato.AddAsync(contato);
            await _context.SaveChangesAsync();
        }

        public async Task<Contato?> GetByIdComTelefonesAsync(int id) {
            return await _context.Contato
                .Include(c => c.Telefones)
                .FirstOrDefaultAsync(c => c.IdContato == id);
        }

        public async Task UpdateAsync(Contato contato) {
            _context.Contato.Update(contato);
            await _context.SaveChangesAsync();
        }


        public async Task DeleteAsync(int id) {
            var contato = await _context.Contato.FindAsync(id);
            if (contato != null) {
                _context.Contato.Remove(contato);
                await _context.SaveChangesAsync();
            }
        }
        public async Task<IEnumerable<Contato>> GetAllComTelefonesAsync() {
            return await _context.Contato
                .Include(c => c.Telefones)
                .ToListAsync();
        }

    }
}
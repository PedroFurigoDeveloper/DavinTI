using DavinTI.Data;
using DavinTI.Domain.Entities;
using DavinTI.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DavinTI.Infra.Repositories {
    public class TelefoneRepository : ITelefoneRepository {
        private readonly DavinTIContext _context;

        public TelefoneRepository(DavinTIContext context) {
            _context = context;
        }

        public async Task<IEnumerable<Telefone>> GetAllAsync(int idContato) {
            return await _context.Telefone
                .AsNoTracking()
                .Where(t => t.IdContato == idContato)
                .ToListAsync();
        }

        public async Task<Telefone?> GetByIdAsync(int id, int idContato) {
            return await _context.Telefone
                .AsNoTracking()
                .FirstOrDefaultAsync(t => t.Id == id && t.IdContato == idContato);
        }

        public async Task<Telefone> CreateAsync(Telefone telefone) {
            await _context.Telefone.AddAsync(telefone);
            await _context.SaveChangesAsync();
            return telefone;
        }

        public async Task<Telefone?> UpdateAsync(Telefone telefone) {
            var existente = await _context.Telefone.FindAsync(telefone.Id, telefone.IdContato);
            if (existente == null)
                return null;

            _context.Entry(existente).CurrentValues.SetValues(telefone);
            await _context.SaveChangesAsync();
            return existente;
        }

        public async Task<bool> DeleteAsync(int id, int idContato) {
            var telefone = await _context.Telefone.FindAsync(id, idContato);
            if (telefone == null)
                return false;

            _context.Telefone.Remove(telefone);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}

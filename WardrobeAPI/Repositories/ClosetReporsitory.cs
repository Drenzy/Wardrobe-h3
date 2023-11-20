using WardrobeAPI.Entities;

namespace WardrobeAPI.Repositories
{

    public interface IClosetReporsitory
    {
        Task<List<Closet>> GetAllAsync();
        Task<Closet> CreateAsync(Closet newCLoset);
        Task<Closet> FindByIdAsync(int closetId);
        Task<Closet> UpdateByIdAsync(int closetId, Closet updateCloset);
        Task<Closet> DeleteByIdAsync(int closetId);
    }
    public class ClosetReporsitory : IClosetReporsitory
    {
        private readonly DatabaseContext _context;

        public ClosetReporsitory(DatabaseContext context)
        {
            _context = context;
        }
        public async Task<Closet> CreateAsync(Closet closet)
        {
            _context.Closet.Add(closet);
            await _context.SaveChangesAsync();
            return closet;
        }

        public async Task<List<Closet>> GetAllAsync()
        {
            return await _context.Closet.ToListAsync();
        }

        public async Task<Closet?> FindByIdAsync(int closetId)
        {
            return await _context.Closet.Include(x => x.Apparels).FirstOrDefaultAsync(x => x.Id == closetId);
        }

        public async Task<Closet?> UpdateByIdAsync(int closetId, Closet updateCloset)
        {
            var closet = await FindByIdAsync(closetId);
            if (closet != null)
            {
                closet.Name = updateCloset.Name;

                await _context.SaveChangesAsync();

                // increase entity has foreingkeys, get the updated data
                // not really nessasary but, will be when closet is added.
                closet = await FindByIdAsync(closetId);
            }
            return closet;
        }

        public async Task<Closet?> DeleteByIdAsync(int closetId)
        {
            var closet = await FindByIdAsync(closetId);
            if (closet != null)
            {
                _context.Closet.Remove(closet);
                await _context.SaveChangesAsync();
            }
            return closet;
        }
    }
}

using WardrobeAPI.Entities;

namespace WardrobeAPI.Repositories
{
    public interface IApparelRepository
    {
        Task<List<Apparel>> GetAllAsync();
        Task<Apparel> CreateAsync(Apparel newApparel);
        Task<Apparel> FindByIdAsync(int apparelId);
        Task<Apparel> UpdateByIdAsync(int apparelId, Apparel updateApparel);
        Task<Apparel> DeleteByIdAsync(int apparelId);
    }
    public class ApparelRepository : IApparelRepository
    {
        private readonly DatabaseContext _context;

        public ApparelRepository(DatabaseContext context)
        {
            _context = context;
        }
        public async Task<List<Apparel>> GetAllAsync()
        {
            return await _context.Apparel.ToListAsync();
        }
        public async Task<Apparel> CreateAsync(Apparel apparel)
        {
            _context.Apparel.Add(apparel);
            await _context.SaveChangesAsync();
            return apparel;
        }

        public async Task<Apparel?> FindByIdAsync(int apparelId)
        {
         return await _context.Apparel.FindAsync(apparelId);


        }

        public async Task<Apparel?> UpdateByIdAsync(int apparelId, Apparel updateApparel)
        {
            var apparel = await FindByIdAsync(apparelId);
            if(apparel != null)
            {
                apparel.Title = updateApparel.Title;
                apparel.Description = updateApparel.Description;
                apparel.Color = updateApparel.Color;

                await _context.SaveChangesAsync();

                // increase entity has foreingkeys, get the updated data
                // not really nessasary but, will be when closet is added.
                apparel = await FindByIdAsync(apparelId);
            }
            return apparel;
        }
        public async Task<Apparel?> DeleteByIdAsync(int apparelId)
        {
            var apparel = await FindByIdAsync(apparelId);
            if(apparel != null)
            {
                _context.Apparel.Remove(apparel);
                await _context.SaveChangesAsync();
            }
            return apparel;
        }
    }
}

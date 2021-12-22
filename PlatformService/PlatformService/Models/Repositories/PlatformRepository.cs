using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PlatformService.Data;

namespace PlatformService.Models.Repositories
{
    public class PlatformRepository : IPlatformRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public PlatformRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async ValueTask<bool> SaveAsync()
        {
            return await _applicationDbContext.SaveChangesAsync() > 0;
        }

        public IQueryable<Platform> All()
        {
            return _applicationDbContext.Platforms;
        }

        public async Task<Platform> FindAsync(int id)
        {
            return await _applicationDbContext.Platforms.FirstOrDefaultAsync(p => p.Id == id);
        }

        public async ValueTask<bool> CreateAsync(Platform platform)
        {
            await _applicationDbContext.Platforms.AddAsync(platform);
            return true;
        }
    }
}

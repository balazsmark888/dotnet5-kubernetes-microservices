using System.Linq;
using System.Threading.Tasks;

namespace PlatformService.Models.Repositories
{
    public interface IPlatformRepository
    {
        ValueTask<bool> SaveAsync();
        IQueryable<Platform> All();
        Task<Platform> FindAsync(int id);
        ValueTask<bool> CreateAsync(Platform platform);
    }
}

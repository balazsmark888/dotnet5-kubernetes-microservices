using System.Threading.Tasks;
using PlatformService.Dtos;

namespace PlatformService.SyncDataServices
{
    public interface ICommandDataClient
    {
        ValueTask<bool> SendPlatformToCommandServiceAsync(PlatformReadDto platformReadDto);
    }
}

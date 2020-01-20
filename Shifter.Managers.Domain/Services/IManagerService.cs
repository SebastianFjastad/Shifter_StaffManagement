using Shifter.Managers.Domain.Models;

namespace Shifter.Managers.Domain.Services
{
    public interface IManagerService
    {
        Manager LoadManager(string username, string password);
    }
}

using Shifter.Security.Domain.Models;

namespace Shifter.Security.Domain.Services
{
    public interface IUserService
    {
        SecurityAccount LoadUser(string username);
        SecurityAccount LoadUserById(string userReferenceId);
    }
}

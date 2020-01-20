using System.Linq;
using Shifter.Security.Domain.Data;
using Shifter.Security.Domain.Models;

namespace Shifter.Security.Domain.Services
{
    public class UserService : IUserService
    {
        #region Implementation of IUserService

        public SecurityAccount LoadUser(string username)
        {
            using (var ctx = new Context())
            {
                return ctx.SecurityAccounts.FirstOrDefault(u => u.Username == username);
            }
        }

        public SecurityAccount LoadUserById(string userReferenceId)
        {
            using (var ctx = new Context())
            {
                return ctx.SecurityAccounts.FirstOrDefault(u => u.Id == userReferenceId);
            }
        }

        #endregion
    }
}

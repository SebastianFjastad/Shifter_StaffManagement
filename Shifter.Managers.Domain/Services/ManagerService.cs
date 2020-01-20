using Framework.Domain;
using Shifter.Managers.Domain.Models;
using System.Linq;

namespace Shifter.Managers.Domain.Services
{
    public class ManagerService : IManagerService
    {
        #region Constructors

        public ManagerService(IRepository repository)
        {
            this.repository = repository;
        }

        #endregion

        #region Locals

        private readonly IRepository repository;

        #endregion

        public Manager LoadManager(string username, string password)
        {
            return repository.FindBy<Manager>(m => m.UserAccount.Username == username && m.UserAccount.Password == password).FirstOrDefault();
        }
    }
}

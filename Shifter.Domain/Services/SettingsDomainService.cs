using System.Linq;
using Framework.Domain;
using Framework.Notifications;
using Shifter.Domain.Model.Entities;

namespace Shifter.Domain.Services
{
    public class SettingsDomainService : ISettingsDomainService
    {
        #region Constructors

        public SettingsDomainService(IRepository repository)
        {
            this.repository = repository;
        }

        #endregion

        #region Locals

        private readonly IRepository repository;

        #endregion

        public Settings LoadSettings(int restaurantId)
        {
            return repository.FindBy<Settings>(m => m.RestaurantId == restaurantId).FirstOrDefault();
        }


        public NotificationCollection SaveSettings(Settings settings)
        {
            return repository.Save(settings);
        }
    }
}

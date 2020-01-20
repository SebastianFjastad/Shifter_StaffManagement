using Framework.Domain;
using Framework.Notifications;
using Shifter.Managers.Domain.Models;
using System.Linq;

namespace Shifter.Managers.Domain.Services
{
    public class SettingsService : ISettingsService
    {
        #region Constructors

        public SettingsService(IRepository repository)
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

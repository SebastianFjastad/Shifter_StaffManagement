using System.Collections.Generic;
using Framework.Domain;
using Framework.Notifications;
using Shifter.Domain.Model.Entities;

namespace Shifter.Domain.Services
{
    public class ShiftTemplateDomainService : IShiftTemplateDomainService
    {
        #region Constructors

        public ShiftTemplateDomainService(IRepository repository)
        {
            this.repository = repository;
        }

        #endregion

        #region Locals

        private readonly IRepository repository;

        #endregion

        #region Implementation of IShiftTemplateService

        public IEnumerable<ShiftTemplate> LoadTemplates(int restaurantId)
        {
            return repository.FindBy<ShiftTemplate>(s => s.RestaurantId == restaurantId);
        }

        public NotificationCollection SaveTemplates(IEnumerable<ShiftTemplate> templates)
        {
            return repository.Save(templates);
        }

        #endregion
    }
}

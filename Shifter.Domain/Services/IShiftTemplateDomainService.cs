using System.Collections.Generic;
using Framework.Notifications;
using Shifter.Domain.Model.Entities;

namespace Shifter.Domain.Services
{
    public interface IShiftTemplateDomainService
    {
        IEnumerable<ShiftTemplate> LoadTemplates(int restaurantId);

        NotificationCollection SaveTemplates(IEnumerable<ShiftTemplate> templates);
    }
}

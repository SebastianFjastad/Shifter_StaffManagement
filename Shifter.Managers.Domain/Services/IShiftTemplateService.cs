using System.Collections.Generic;
using Framework.Notifications;
using Shifter.Managers.Domain.Models;

namespace Shifter.Managers.Domain.Services
{
    public interface IShiftTemplateService
    {
        IEnumerable<ShiftTemplate> LoadTemplates(int restaurantId);

        NotificationCollection SaveTemplates(IEnumerable<ShiftTemplate> templates);
    }
}

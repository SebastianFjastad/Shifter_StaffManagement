using System.Collections.Generic;
using Framework.Notifications;
using Shifter.Managers.Domain.Models;

namespace Shifter.Managers.Domain.Services
{
    public interface IWaiterService
    {
        IEnumerable<Waiter> LoadWaiters(int restaurantId);

        Waiter LoadWaiter(int waiterId);

        NotificationCollection SaveWaiter(Waiter waiter);

        NotificationCollection DeleteWaiter(int waiterId);

        NotificationCollection ResetPassword(int waiterId);
    }
}

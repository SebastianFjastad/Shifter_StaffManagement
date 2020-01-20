using Framework.Notifications;
using Shifter.Waiters.Domain.Models;
using System.Collections.Generic;

namespace Shifter.Waiters.Domain.Services
{
    public interface IWaiterService
    {
        IEnumerable<Waiter> LoadWaiters(int restaurantId);

        Waiter LoadWaiter(int waiterId);

        Waiter LoadWaiter(string username, string password);

        NotificationCollection SaveWaiter(Waiter waiter);

        NotificationCollection ResetPassword(int waiterId);

        Waiter FindWaiter(string username);
    }
}

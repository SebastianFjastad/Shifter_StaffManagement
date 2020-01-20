using System;
using System.Transactions;
using System.Web.Security;
using Framework.Extensions;
using Framework.Notifications;
using Framework.Notifications.Extensions;
using Shifter.Managers.Domain;
using Shifter.Managers.Web.Models;
using Shifter.Managers.Web.ViewModels.Waiters;

namespace Shifter.Managers.Web.Helpers
{
    public static class WaiterPersistenceHelper
    {
        //TODO this needs to be better
        public static NotificationCollection SaveWaiter(WaiterViewModel viewModel, IMembershipService membershipService, IShifterSystem shifterSystem)
        {
            var notifications = NotificationCollection.CreateEmpty();

            if (viewModel.Waiter.IsNew())
            {
                notifications += UpdateWaiter(viewModel, membershipService, shifterSystem);
            }
            else
            {
                notifications += SaveNewWaiter(viewModel, membershipService, shifterSystem);
            }

            return notifications;
        }

        #region private methods

        private static NotificationCollection SaveNewWaiter(WaiterViewModel viewModel, IMembershipService membershipService, IShifterSystem shifterSystem)
        {
            var notifications = NotificationCollection.CreateEmpty();

            try
            {
                using (var scope = new TransactionScope())
                {
                    var createStatus = membershipService.CreateUser(viewModel.Waiter.EmailAddress, viewModel.Password, viewModel.Waiter.EmailAddress);

                    if (createStatus == MembershipCreateStatus.Success)
                    {
                        notifications += CreateWaiterProfile(viewModel, shifterSystem, membershipService);
                    }
                    else
                    {
                        notifications.AddError(createStatus.ToString());
                    }

                    scope.Complete();
                }
            }
            catch (TransactionAbortedException ex)
            {
                notifications.AddError(ex.Message);
            }
            catch (ApplicationException ex)
            {
                notifications.AddError(ex.Message);
            }

            return notifications;
        }

        private static NotificationCollection CreateWaiterProfile(WaiterViewModel viewModel, IShifterSystem shifterSystem, IMembershipService membershipService)
        {
            var notifications = NotificationCollection.CreateEmpty();

            var userAccount = shifterSystem.UserService.LoadUser(viewModel.Waiter.EmailAddress);

            if (userAccount.IsNotNull())
            {
                notifications += shifterSystem.WaiterService.SaveWaiter(viewModel.Waiter);

                if (notifications.HasErrors())
                {
                    membershipService.DeleteUser(userAccount.Username);
                }
            }

            return notifications;
        }


        private static NotificationCollection UpdateWaiter(WaiterViewModel viewModel, IMembershipService membershipService, IShifterSystem shifterSystem)
        {
            var notifications = NotificationCollection.CreateEmpty();

            notifications += UpdatePassword(viewModel, membershipService);

            notifications += shifterSystem.WaiterService.SaveWaiter(viewModel.Waiter);

            return notifications;
        }

        private static NotificationCollection UpdatePassword(WaiterViewModel viewModel, IMembershipService membershipService)
        {
            var notifications = NotificationCollection.CreateEmpty();

            try
            {
                var account = membershipService.LoadUser(viewModel.Waiter.EmailAddress);
                var currentPassword = account.GetPassword();

                if (currentPassword != viewModel.Password)
                {
                    account.ChangePassword(currentPassword, viewModel.Password);
                }
            }
            catch (Exception exception)
            {
                notifications.AddError(exception.Message);
            }

            return notifications;
        }

        #endregion
    }
}
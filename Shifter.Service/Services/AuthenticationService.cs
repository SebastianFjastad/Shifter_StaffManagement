using System.Collections.Generic;
using Framework;
using Framework.Notifications.Extensions;
using Shifter.Domain.Model.Entities;
using Shifter.Domain.Services;
using Shifter.Service.Api.Dtos;
using Shifter.Service.Api.Requests;
using Shifter.Service.Api.Responses;
using Shifter.Service.Api.Services;
using System;

namespace Shifter.Service.Services
{
    public class AuthenticationService : ShifterServiceBase, IAuthenticationService
    {
        private readonly IAuthenticationDomainService authenticationDomainService;
        private readonly IManagerDomainService managerDomainService;
        private readonly IRestaurantDomainService restaurantDomainService;

        public AuthenticationService(IAuthenticationDomainService authenticationDomainService, IManagerDomainService managerDomainService, IRestaurantDomainService restaurantDomainService)
        {
            Guard.ArgumentNotNull(authenticationDomainService, "authenticationDomainService");

            this.authenticationDomainService = authenticationDomainService;
            this.managerDomainService = managerDomainService;
            this.restaurantDomainService = restaurantDomainService;
        }

        public GenericServiceResponse Register(RegistrationRequest request)
        {
            return TryExecute(() =>
            {
                Guard.ArgumentNotNull(request, "request");

                var response = new GenericServiceResponse();

                var manager = new Manager()
                {
                    UserAccount = new UserAccount()
                    {
                        FirstName = request.ManagerFirstName,
                        LastName = request.ManagerLastName,
                        EmailAddress = request.ManagerEmailAddress,
                        Username = request.ManagerUsername,
                        Password = request.ManagerPassword
                    },
                    Restaurants = new List<Restaurant>()
                };
                var restaurant = new Restaurant()
                {
                    Name = request.CompanyName,
                    Address = request.CompanyAddress
                };

                //TODO transactionality
                response.NotificationCollection += restaurantDomainService.SaveRestaurant(restaurant);

                manager.Restaurants.Add(restaurant);
                response.NotificationCollection += managerDomainService.SaveManager(manager);

                return response;
            });
        }

        public AuthenticationResponse Authenticate(AuthenticationRequest request)
        {
            return TryExecute(() =>
            {
                Guard.ArgumentNotNull(request, "request");

                var response = new AuthenticationResponse();

                var userAccount = authenticationDomainService.FindUserAccount(request.Username, request.Password);

                if (userAccount.IsNotNull())
                {
                    response.UserAccountId = userAccount.Id;
                    response.Profiles = LoadProfiles(userAccount.Id);
                }
                else
                {
                    response.NotificationCollection.AddError("Account not found.");
                }

                return response;
            });
        }

        public GenericServiceResponse UpdateUserAccount(UpdateUserAccountRequest request)
        {
            var result = new GenericServiceResponse();

            var userAccount = authenticationDomainService.LoadUserAccount(request.UserAccountId);

            if (userAccount.IsNotNull())
            {
                userAccount.FirstName = request.FirstName;
                userAccount.LastName = request.LastName;
                userAccount.EmailAddress = request.EmailAddress;
                userAccount.ContactNumber = request.ContactNumber;

                result.NotificationCollection += authenticationDomainService.SaveUserAccount(userAccount);
            }
            else
            {
                result.NotificationCollection.AddError("User account could not be found.");
            }

            return result;

        }

        public FindUserAccountResponse FindUser(FindUserAccountRequest request)
        {
            return TryExecute(() =>
            {
                Guard.ArgumentNotNull(request, "request");

                var userAccount = authenticationDomainService.FindUserAccount(request.Username);

                var result = new FindUserAccountResponse();

                result.UserAccountId = userAccount.Id;
                result.EmailAddress = userAccount.EmailAddress;

                return result;
            });
        }

        public PasswordResetResponse ResetPassword(GenericEntityRequest request)
        {
            return TryExecute(() =>
            {
                Guard.ArgumentNotNull(request, "request");

                var notificationCollection = authenticationDomainService.ResetPassword(request.EntityId);

                return new PasswordResetResponse
                {
                    NotificationCollection = notificationCollection
                };
            });
        }

        public GenericServiceResponse SaveNewPassword(SaveNewPasswordRequest request)
        {
            return TryExecute(() =>
            {
                Guard.ArgumentNotNull(request, "request");

                var notificationCollection = authenticationDomainService.SaveNewPassword(request.UserAccountId, request.Password);

                return new GenericServiceResponse
                {
                    NotificationCollection = notificationCollection
                };
            });
        }

        private IEnumerable<ProfileSummary> LoadProfiles(int userAccountId)
        {
            var result = new List<ProfileSummary>();

            //TODO this needs to be made generic
            var manager = authenticationDomainService.LoadProfileByUserAccount<Manager>(userAccountId);
            var staff = authenticationDomainService.LoadProfileByUserAccount<Staff>(userAccountId);

            if (manager.IsNotNull())
            {
                result.Add(new ManagerProfileSummary(manager.Id));
            }

            if (staff.IsNotNull())
            {
                result.Add(new StaffProfileSummary(staff.Id));
            }

            return result;
        }
    }
}

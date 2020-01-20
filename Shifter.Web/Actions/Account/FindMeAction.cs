using System;
using Framework.Encryption;
using Shifter.Service.Api.Requests;

namespace Shifter.Web.Actions.Account
{
    public class FindMeAction<T> where T : class
    {
        #region Locals

        private readonly IServiceRegistry serviceRegistry;

        #endregion

        #region Constructors

        public FindMeAction(IServiceRegistry serviceRegistry)
        {
            this.serviceRegistry = serviceRegistry;
        }

        #endregion

        #region Properties

        public Func<object, T> OnSuccess { get; set; }

        public Func<string, T> OnFail { get; set; }

        #endregion

        #region Methods

        public T Invoke(string username)
        {
            var response = serviceRegistry.AuthenticationService.FindUser(new FindUserAccountRequest { Username = username });

            if (response.IsNotNull() && response.UserAccountId.HasValue)
            {
                var result = new { UserAccountId = response.UserAccountId.ToString().Encrypt(), EmailAddress = response.EmailAddress };

                return OnSuccess.Invoke(result);
            }

            return OnFail.Invoke("Username does not exist.");
        }

        #endregion
    }
}
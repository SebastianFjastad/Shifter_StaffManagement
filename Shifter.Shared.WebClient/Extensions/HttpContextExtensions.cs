using System;
using System.Web;
using System.Web.Security;
using Framework.Serialization;
using Shifter.Service.Api;
using Shifter.Shared.WebClient.Models;

namespace Shifter.Shared.WebClient.Extensions
{
    public static class HttpContextExtensions
    {
        public static FormsAuthenticationTicket GetFormsAuthTicket(this HttpContextBase context)
        {
            try
            {
                var cookie = context.Request.Cookies[FormsAuthentication.FormsCookieName];
                if (cookie.IsNotNull() && cookie.Value.IsNotNullOrEmpty())
                {
                    var ticket = FormsAuthentication.Decrypt(cookie.Value);

                    return ticket;
                }
            }
            catch (Exception)
            {
                //don't do anything
            }

            return null;
        }

        public static UserData GetUserData(this HttpContextBase context)
        {
            var cookieValue = context.GetFormsAuthTicket();

            var userData = new UserData();

            if (cookieValue.IsNotNull() && cookieValue.UserData.IsNotNullOrEmpty())
            {
                if (cookieValue.UserData.IsNotNullOrEmpty())
                {
                    userData = JsonSerialization.JsonDeserialize<UserData>(cookieValue.UserData);
                    EnsureClientKeyIsSet(context);
                }
            }

            return userData;
        }

        public static void UpdateCookie(this HttpContextBase context, UserData userData)
        {
            var cookieValue = context.GetFormsAuthTicket();

            var json = JsonSerialization.JsonSerialize(userData);

            var ticket = new FormsAuthenticationTicket(cookieValue.Version, cookieValue.Name, cookieValue.IssueDate, cookieValue.Expiration, true, json);

            var cookie = context.Request.Cookies[FormsAuthentication.FormsCookieName];

            if (cookie.IsNotNull())
            {
                cookie.Value = FormsAuthentication.Encrypt(ticket);
                cookie.Path = FormsAuthentication.FormsCookiePath;
                context.Response.Cookies.Add(cookie);
            }
        }

        private static void EnsureClientKeyIsSet(HttpContextBase context)
        {
            if (context.Session[Constants.SessionKeys.ClientKey].IsNull())
            {
                context.Session[Constants.SessionKeys.ClientKey] = Guid.NewGuid();
            }
        }
    }
}
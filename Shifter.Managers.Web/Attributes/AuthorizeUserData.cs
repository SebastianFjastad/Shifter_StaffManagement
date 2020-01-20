using Shifter.Service.Api.Dtos;
using Shifter.Shared.WebClient.Extensions;
using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Shifter.Managers.Web.Attributes
{
    public class AuthorizeUserData : AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            var userData = httpContext.GetUserData();

            if (userData.IsNull() || userData.RestaurtantId == 0 || userData.ProfileId == 0 || userData.ProfileType != ProfileType.Manager)
            {
                FormsAuthentication.SignOut();
                return false;
            }
            return base.AuthorizeCore(httpContext);
        }
    }
}
using System;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Framework.Notifications.Extensions;
using Newtonsoft.Json;
using Shifter.Shared.WebClient.Extensions;
using Shifter.Web.Helpers;
using Shifter.Web.ViewModels;

namespace Shifter.Web.Attributes
{
    public class AuthorizeReCapture : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var response = filterContext.HttpContext.Request["g-recaptcha-response"];
            //secret that was generated in key value pair
            const string secret = "6Le1ggETAAAAAE1zV9RihBZjEzBvEijNrKe7y2uU";

            var client = new WebClient();
            var reply = client.DownloadString(string.Format("https://www.google.com/recaptcha/api/siteverify?secret={0}&response={1}", secret, response));

            var captchaResponse = JsonConvert.DeserializeObject<CaptchaResponse>(reply);

            //when response is false check for the error message
            if (!captchaResponse.Success && captchaResponse.ErrorCodes.Count > 0)
            {
                var error = captchaResponse.ErrorCodes[0].ToLower();
                var message = string.Empty;
                switch (error)
                {
                    case ("missing-input-secret"):
                        message = "The secret parameter is missing.";
                        break;
                    case ("invalid-input-secret"):
                        message = "The secret parameter is invalid or malformed.";
                        break;
                    case ("missing-input-response"):
                        message = "The response parameter is missing.";
                        break;
                    case ("invalid-input-response"):
                        message = "The response parameter is invalid or malformed.";
                        break;
                    default:
                        message = "Error occurred. Please try again";
                        break;
                }

                //TODO log error

                filterContext.Controller.ViewData.ModelState.AddModelError("InvalidCaptcha", "Either you are a robot or you made a typo... please try again.");
            }

            base.OnActionExecuting(filterContext);
        }
    }
}
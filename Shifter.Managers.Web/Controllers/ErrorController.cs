using System.Web.Mvc;
using System.Web.Security;

namespace Shifter.Managers.Web.Controllers
{
    public class ErrorController : Controller
    {
        public ActionResult PageNotFound()
        {
            return View();
        }

        public ActionResult GeneralError()
        {
            return View();
        }

        public ActionResult Timeout()
        {
            FormsAuthentication.SignOut();
            return View();
        }
    }
}

using System;
using System.Web;
using System.Web.Mvc;
using Shifter.Service.Api.Dtos;

namespace Shifter.GroupAdmins.Web.Controllers
{
    public abstract class ShifterBaseController : Controller
    {
        #region Constructor

        protected ShifterBaseController(IServiceRegistry serviceRegistry)
        {
            ServiceRegistry = serviceRegistry;
        }

        #endregion

        #region Properties

        protected readonly IServiceRegistry ServiceRegistry;

        #endregion 

        public int ResolveGroupAdminId
        {
            get
            {
                var adminId = HttpContext.Session["AdminId"] as int?;

                if (adminId.IsNull())
                {
                    throw new HttpException("500");
                }

                return adminId.Value;
            }
        }

        public void SetGroupAdminId(int id)
        {
            HttpContext.Session["AdminId"] = id;
        }
    }
}
using Shifter.Managers.Web.Actions;
using Shifter.Managers.Web.Attributes;
using Shifter.Managers.Web.ViewModels;
using System.Web.Mvc;

namespace Shifter.Managers.Web.Controllers
{
    [AuthorizeUserData]
    public class ShiftTemplateController : ShifterBaseController
    {
        /// <summary>
        /// Constructor to set the base system
        /// </summary>
        public ShiftTemplateController(IServiceRegistry serviceRegistry)
            : base(serviceRegistry)
        {
        }

        /// <summary>
        /// Loads the shift template
        /// </summary>
        [HttpGet]
        public ActionResult LoadShiftTemplate()
        {
            var action = new LoadShiftTemplateAction<ActionResult>(ServiceRegistry)
            {
                OnComplete = (model) => View("ShiftTemplate", model)
            };

            return action.Invoke(ResolveRestaurantId());
        }

        /// <summary>
        /// Saves the shift template
        /// </summary>
        [HttpPost]
        public ActionResult SaveShiftTemplate(ShiftTemplateViewModel viewModel)
        {
            var action = new SaveShiftTemplateAction<ActionResult>(ServiceRegistry)
            {
                OnComplete = (model) => View("ShiftTemplate", model)
            };

            return action.Invoke(viewModel, ResolveRestaurantId());
        }
    }
}

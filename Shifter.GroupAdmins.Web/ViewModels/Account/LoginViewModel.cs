
using System.ComponentModel.DataAnnotations;

namespace Shifter.GroupAdmins.Web.ViewModels.Account
{
    public class LoginViewModel
    {
        #region Properties

        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }

        public bool RememberMe { get; set; }

        #endregion
    }
}
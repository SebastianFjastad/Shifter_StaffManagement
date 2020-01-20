
using System.ComponentModel.DataAnnotations;

namespace Shifter.Web.ViewModels.Account
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
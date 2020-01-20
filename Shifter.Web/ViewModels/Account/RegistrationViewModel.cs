using System.ComponentModel.DataAnnotations;

namespace Shifter.Web.ViewModels.Account
{
    public class RegistrationViewModel : ShifterModelBase
    {
        public string CompanyAddress { get; set; }

        [Required(ErrorMessage = "Company Name is required")]
        public string CompanyName { get; set; }

        [Required(ErrorMessage = "Email Address is required")]
        public string ManagerEmailAddress { get; set; }

        [Required(ErrorMessage = "First Name is required")]
        public string ManagerFirstName { get; set; }

        public string ManagerLastName { get; set; }

        [Required(ErrorMessage = "Username is required")]
        public string ManagerUsername { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string ManagerPassword { get; set; }

        [Required(ErrorMessage = "Confirm Password is required")]
        public string ManagerPasswordConfirm { get; set; }
    }
}

using Shifter.Service.Api.Dtos;

namespace Shifter.Web.ViewModels.Account
{
    public class PersonalDetailsViewModel : ShifterModelBase
    {
        #region Properties

        public string NewPassword { get; set; }
        public string ConfirmPassword { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string EmailAddress { get; set; }

        public string Username { get; set; }

        public string ContactNumber { get; set; }

        #endregion

        #region Factory methods

        public static PersonalDetailsViewModel Create(StaffDto staff, string username)
        {
            var model = new PersonalDetailsViewModel();

            model.FirstName = staff.FirstName;
            model.LastName = staff.LastName;
            model.EmailAddress = staff.EmailAddress;
            model.ContactNumber = staff.ContactNumber;
            model.Username = username;

            return model;
        }

        #endregion
    }
}
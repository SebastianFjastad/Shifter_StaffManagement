using System;
using Shifter.Service.Api.Dtos;

namespace Shifter.Shared.WebClient.ViewModels
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

        public static PersonalDetailsViewModel Create(string username, string firstName, string lastName, string emailAddress, string contactNumber)
        {
            var model = new PersonalDetailsViewModel();

            model.Username = username;

            model.FirstName = firstName;
            model.LastName = lastName;
            model.EmailAddress = emailAddress;
            model.ContactNumber = contactNumber;

            return model;
        }

        #endregion
    }
}
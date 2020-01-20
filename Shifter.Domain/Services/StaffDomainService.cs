using System;
using System.Collections.Generic;
using System.Linq;
using Framework;
using Framework.Domain;
using Framework.Email;
using Framework.Encryption;
using Framework.Notifications;
using Framework.Notifications.Extensions;
using Shifter.Domain.Model.Entities;

namespace Shifter.Domain.Services
{
    public class StaffDomainService : IStaffDomainService
    {
        #region Constructors

        public StaffDomainService(IRepository repository, IPasswordEncryptor passwordEncryptor, IPasswordGenerator passwordGenerator)
        {
            Guard.ArgumentNotNull(repository, "repository");
            Guard.ArgumentNotNull(passwordEncryptor, "passwordEncryptor");
            Guard.ArgumentNotNull(passwordGenerator, "passwordGenerator");

            this.repository = repository;
            this.passwordEncryptor = passwordEncryptor;
            this.passwordGenerator = passwordGenerator;
        }

        #endregion

        #region Locals

        private readonly IRepository repository;
        private readonly IPasswordEncryptor passwordEncryptor;
        private readonly IPasswordGenerator passwordGenerator;

        #endregion

        #region Implementation of IStaffService

        public int LoadTotalStaff(int restaurantId)
        {
            var totalStaff = repository.FindBy<Staff>(s => s.Restaurants.Any(r => r.Id == restaurantId)).Count();

            return totalStaff;
        }

        public IEnumerable<Staff> LoadStaff(int restaurantId, IEnumerable<int> staffTypeIds)
        {
            if (staffTypeIds.IsNotNull() && staffTypeIds.Any())
            {
                var result = repository.FindBy<Staff>(w => w.Restaurants.Any(r => r.Id == restaurantId) && w.IsActive);
                return result.Where(s => staffTypeIds.Any(id => s.StaffType.Id == id)).ToList();
            }

            return repository.FindBy<Staff>(w => w.Restaurants.Any(r => r.Id == restaurantId) && w.IsActive);
        }

        public IEnumerable<StaffType> LoadStaffTypes()
        {
            return repository.FindAll<StaffType>();
        }

        public Staff LoadStaffMember(int staffId)
        {
            return repository.FindById<Staff>(staffId);
        }

        public Staff LoadStaff(string username, string password)
        {
            var staff = repository.FindBy<Staff>(m => m.UserAccount.Username == username && m.IsActive).FirstOrDefault();

            if (staff.IsNotNull())
            {
                var passwordMatch = staff.UserAccount.DoPasswordsMatch(password, passwordEncryptor);

                if (!passwordMatch)
                {
                    //Dont return the staff if the password doesn't match
                    return null;
                }
            }

            return staff;
        }

        public NotificationCollection SaveStaffUnavailability(IEnumerable<StaffUnavailabilityRecord> unavailability)
        {
            return repository.Save(unavailability);
        }

        public NotificationCollection DeleteStaffUnavailability(IEnumerable<int> unavailabilityIds)
        {
            var result = NotificationCollection.CreateEmpty();

            foreach (var id in unavailabilityIds)
            {
                result += repository.Delete<StaffUnavailabilityRecord>(id);
            }

            return result;
        }

        public NotificationCollection SaveStaff(Staff staff, UserAccount userAccount, bool emailAddressIsActive = true)
        {
            var newPassword = passwordGenerator.NewPassword();
            var staffToSave = staff.IsTransient() ? CreateAccount(staff, userAccount, newPassword) : UpdateAccount(staff);

            //Because personal details is on the user account for the domain model and on the aggregate for the dto these need to be updated explicitly 
            staffToSave.UserAccount.UpdatePersonalDetails(userAccount);

            var result = repository.Save(staffToSave);

            if (!result.HasErrors())
            {
                result += new Notification("Staff saved successfully.", NotificationSeverity.Information);

                if (!staffToSave.WelcomeEmailSent && emailAddressIsActive)
                {
                    result += SaveWelcomeEmail(staffToSave, newPassword);

                    staffToSave.UserAccount.Password = newPassword;
                    staffToSave.UserAccount.EncrypPassword(passwordEncryptor);
                    staffToSave.WelcomeEmailSent = true;

                    result += repository.Save(staffToSave);
                }
            }

            return result;
        }

        public NotificationCollection ResetPassword(int staffId)
        {
            var staff = repository.FindById<Staff>(staffId);

            var newPassword = passwordGenerator.NewPassword();
            staff.UserAccount.Password = newPassword;
            staff.UserAccount.EncrypPassword(passwordEncryptor);

            var result = repository.Save(staff);

            if (!result.HasErrors())
            {
                var message = repository.FindBy<EmailTemplate>(e => e.TemplateName == Constants.EmailTemplates.PasswordReset).FirstOrDefault();

                Guard.InstanceNotNull(message, "message");

                message.Body = message.Body.Replace(Constants.EmailTemplatePlaceHolders.Password, newPassword);
                message.Body = message.Body.Replace(Constants.EmailTemplatePlaceHolders.Name, staff.UserAccount.FirstName);
                message.Body = message.Body.Replace(Constants.EmailTemplatePlaceHolders.Username, staff.UserAccount.Username);

                result += repository.Save(EmailNotification.Create(message.Subject, message.Body, SharedConfig.FromSupportEmailAddress, staff.UserAccount.EmailAddress));
            }
            if (!result.HasErrors())
            {
                result += new Notification("Password reset successful.", NotificationSeverity.Information);
            }

            return result;
        }

        public NotificationCollection DeleteStaff(int staffId)
        {
            var result = NotificationCollection.CreateEmpty();

            var staff = repository.FindById<Staff>(staffId);

            if (staff.IsNotNull())
            {
                staff.IsActive = false;
                staff.Restaurants = new List<Restaurant>();

                var waiterShifts = repository.FindBy<Shift>(s => s.Staff.Id == staff.Id && s.IsCancelled == false && s.StartTime >= DateTime.Now);

                if (waiterShifts.Any())
                {
                    result.AddError("The staff member cannot be deleted because there are still shifts assigned to them.");
                }
                else
                {
                    result += repository.Save(staff);
                }
            }

            if (!result.HasErrors())
            {
                result.AddMessage(new Notification("Staff member deleted.", NotificationSeverity.Information));
            }

            return result;
        }

        public string GenerateUsername(string firstName, string lastName, string restaurantName)
        {
            Guard.ArgumentNotNull(firstName, "firstName");
            Guard.ArgumentNotNull(firstName, "lastName");
            Guard.ArgumentNotNull(firstName, "restaurantName");

            firstName = firstName.RemoveWhiteSpaceAndSpecialChar();
            lastName = lastName.RemoveWhiteSpaceAndSpecialChar();
            restaurantName = restaurantName.RemoveWhiteSpaceAndSpecialChar();

            var username = string.Format("{0}.{1}_{2}", firstName, lastName, restaurantName);
            var existingUsers = repository.FindBy<UserAccount>(u => u.Username == username).ToList();

            if (existingUsers.IsNotNull() && existingUsers.Any())
            {
                username = string.Format("{0}.{1}_{2}{3}", firstName, lastName, restaurantName, existingUsers.Count() + 1);
            }

            return username;
        }

        #endregion

        #region Private Methods

        private NotificationCollection SaveWelcomeEmail(Staff staff, string newPassword)
        {
            var message = repository.FindBy<EmailTemplate>(e => e.TemplateName == Constants.EmailTemplates.ShifterRegistration).FirstOrDefault();

            message.Body = message.Body.Replace(Constants.EmailTemplatePlaceHolders.Password, newPassword);
            message.Body = message.Body.Replace(Constants.EmailTemplatePlaceHolders.Name, staff.UserAccount.FirstName);
            message.Body = message.Body.Replace(Constants.EmailTemplatePlaceHolders.Username, staff.UserAccount.Username);

            return repository.Save(EmailNotification.Create(message.Subject, message.Body, SharedConfig.FromSupportEmailAddress, staff.UserAccount.EmailAddress));
        }

        private Staff CreateAccount(Staff staff, UserAccount userAccount, string newPassword)
        {
            //Reactivate the old account if exists
            var inactiveAccount = repository.FindBy<Staff>(w => w.UserAccount.EmailAddress == userAccount.EmailAddress && w.IsActive == false).FirstOrDefault();
            if (inactiveAccount.IsNotNull())
            {
                staff.Id = inactiveAccount.Id;
            }

            var username = userAccount.Username.IsNullOrEmpty() ? userAccount.EmailAddress : userAccount.Username;

            //Create security account
            staff.IsActive = true;
            staff.UserAccount = UserAccount.Create(username, newPassword, passwordEncryptor);

            return staff;
        }

        private Staff UpdateAccount(Staff staff)
        {
            //Update info that must not be overwritten
            var existingAccount = repository.FindById<Staff>(staff.Id);

            staff.IsActive = existingAccount.IsActive;
            staff.UserAccount = existingAccount.UserAccount;
            staff.WelcomeEmailSent = existingAccount.WelcomeEmailSent;
            staff.UnavailabilityRecords = existingAccount.UnavailabilityRecords; //this should not be necessary as it is mapped as cascade none..

            return staff;
        }

        #endregion
    }
}

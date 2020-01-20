using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Shifter.Web.ViewModels.ContactUs
{
    public class ContactUsViewModel
    {
        #region Properties

        public string FullName { get; set; }

        public string Email { get; set; }

        public string ContactNumber { get; set; }

        public string SkypeName { get; set; }

        public string Message { get; set; }

        #endregion

        #region Methods

        public string GetFormattedMessage()
        {
            var message = new StringBuilder();
            message.AppendFormat("Name: {0}", FullName);
            message.AppendLine("<br /><br />");

            message.AppendFormat("Contact number: {0}", ContactNumber);
            message.AppendLine("<br /><br />");

            message.AppendFormat("Skype name: {0}", SkypeName);
            message.AppendLine("<br /><br />");

            message.AppendLine("Message:");
            message.AppendLine("<br />");
            message.Append(Message);

            return message.ToString();
        }

        #endregion
    }
}
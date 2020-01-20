using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Framework;
using Shifter.Service.Api.Dtos;
using Shifter.Service.Api.Requests;
using Shifter.Service.Api.Services;

namespace Shifter.Shared.WebClient.Actions
{
    public class SaveLastSeenDateAction<T> where T : class
    {
        #region locals

        private readonly IStaffService staffService;
        #endregion
        public SaveLastSeenDateAction(IStaffService staffService)
        {
            Guard.ArgumentNotNull(staffService, "staffService");
            this.staffService = staffService;
        }

        public Func<T> OnComplete { get; set; }

        public T Invoke(DateTime lastSeenTime, int userAccountId)
        {
            var request = new SaveLastSeenDateRequest()
            {
                StaffId = userAccountId,
                LastSeenDate = lastSeenTime
            };

            staffService.SaveLastSeenDate(request);

            return OnComplete.Invoke();
        }
    }
}

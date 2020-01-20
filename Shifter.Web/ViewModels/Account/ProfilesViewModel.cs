using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Shifter.Service.Api.Dtos;
using Shifter.Service.Api.Responses;

namespace Shifter.Web.ViewModels.Account
{
    public class ProfilesViewModel
    {
        public ProfilesViewModel(IEnumerable<ProfileSummary> profiles)
        {
            Profiles = profiles;
        }

        public IEnumerable<ProfileSummary> Profiles { get; set; }
    }
}
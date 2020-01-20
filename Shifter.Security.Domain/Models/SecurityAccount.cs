
using System;

namespace Shifter.Security.Domain.Models
{
    /// <summary>
    /// Represents a user in the domain
    /// </summary>
    public class SecurityAccount
    {
        public string Id { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }
    }
}
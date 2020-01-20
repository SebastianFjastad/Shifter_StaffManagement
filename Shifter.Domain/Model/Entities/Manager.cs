using System.Collections.Generic;
using Framework.Domain;

namespace Shifter.Domain.Model.Entities
{
    public class Manager : Entity, IProfile
    {
        public UserAccount UserAccount { get; set; }

        public virtual ICollection<Restaurant> Restaurants { get; set; }
    }
}

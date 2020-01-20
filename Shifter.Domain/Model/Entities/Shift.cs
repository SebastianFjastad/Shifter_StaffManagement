using System;
using Framework.Domain;

namespace Shifter.Domain.Model.Entities
{
    public class Shift : Entity
    {
        #region properties

        public virtual Staff Staff { get; set; }
               
        public virtual Restaurant Restaurant { get; set; }
               
        public virtual DateTime StartTime { get; set; }

        public virtual DateTime EndTime { get; set; }
               
        public virtual bool IsAvailable { get; set; }
               
        public virtual bool IsCancelled { get; set; }

        #endregion
    }
}

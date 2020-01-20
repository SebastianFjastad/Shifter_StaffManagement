
namespace Shifter.Managers.Domain.Models
{
    public abstract class Entity
    {
        #region Properties

        public virtual int Id { get; set; }

        #endregion

        #region Methods

        public virtual bool IsTransient()
        {
            return Id <= 0;
        }

        #endregion
    }
}

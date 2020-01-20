
namespace Framework.Domain
{
    public abstract class Entity : IEntity
    {
        #region Properties

        public virtual int Id { get; set; }

        #endregion

        #region Methods

        /// <summary>
        /// Indicates if the instance is new or whether it has already been persisted by checking if the id > 0
        /// </summary>
        public virtual bool IsTransient()
        {
            return Id <= 0;
        }

        #endregion
    }
}

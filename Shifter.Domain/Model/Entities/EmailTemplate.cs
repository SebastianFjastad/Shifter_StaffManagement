using Framework.Domain;

namespace Shifter.Domain.Model.Entities
{
    public class EmailTemplate : Entity
    {
        #region Properties

        public virtual string Body { get; set; }

        public virtual string Subject { get; set; }

        public virtual string TemplateName { get; set; }

        #endregion
    }
}

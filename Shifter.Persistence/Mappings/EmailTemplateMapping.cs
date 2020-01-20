using FluentNHibernate.Mapping;
using Shifter.Domain.Model.Entities;

namespace Shifter.Persistence.Mappings
{
    public class EmailTemplateMapping : ClassMap<EmailTemplate>
    {
        public EmailTemplateMapping()
        {
            Not.LazyLoad();

            Id(c => c.Id, "EmailTemplateId");
            
            Map(c => c.Body);
            Map(c => c.Subject);

            Map(c => c.TemplateName);

            Table("EmailTemplate");
        }
    }
}
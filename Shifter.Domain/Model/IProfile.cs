using Framework.Domain;
using Shifter.Domain.Model.Entities;

namespace Shifter.Domain.Model
{
    public interface IProfile : IEntity
    {
        UserAccount UserAccount { get; set; }
    }
}
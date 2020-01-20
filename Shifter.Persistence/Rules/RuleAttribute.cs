using System;

namespace Shifter.Persistence.Rules
{
    [AttributeUsage(AttributeTargets.Class)]
    public sealed class RuleAttribute : Attribute
    {
        #region Constructors

        public RuleAttribute(Type entityType)
        {
            EntityType = entityType;
        }

        public RuleAttribute()
        {
        }

        #endregion

        #region Public Properties

        public Type EntityType { get; private set; }

        #endregion
    }
}
﻿using System;

namespace Framework.Domain.Rules.Rules
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public sealed class RuleContextAttribute : Attribute
    {
        #region Constructors

        public RuleContextAttribute(ValidationContextKeys key)
        {
            Key = key;
        }

        #endregion

        #region Properties

        public ValidationContextKeys Key { get; private set; }

        #endregion
    }
}

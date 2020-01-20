using Framework.Domain;
using Framework.Notifications;
using Framework.Rules;
using NHibernate.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Shifter.Persistence.Data
{
    public class Repository : IRepository
    {
        #region Constructors

        public Repository(IDatabaseFactory databaseFactory)
        {
            this.databaseFactory = databaseFactory;
        }

        #endregion

        #region Locals

        private IDatabaseFactory databaseFactory;

        #endregion

        #region Public Methods

        public NotificationCollection Save(object entity)
        {
            var notifications = Validate(entity, ValidationContextKeys.Save);

            if (!notifications.HasErrors())
            {
                using (var session = databaseFactory.Session())
                {
                    using (var transaction = session.BeginTransaction())
                    {
                        session.SaveOrUpdate(entity);
                        transaction.Commit();
                    }

                }
            }

            return notifications;
        }

        public NotificationCollection Save(IEnumerable<object> entities)
        {
            var notifications = NotificationCollection.CreateEmpty();

            foreach (var entity in entities)
            {
                notifications += Validate(entity, ValidationContextKeys.Save);
            }

            if (!notifications.HasErrors())
            {
                using (var session = databaseFactory.Session())
                {
                    session.SetBatchSize(entities.Count());
                    using (var transaction = session.BeginTransaction())
                    {
                        foreach (var entity in entities)
                        {
                            session.SaveOrUpdate(entity);
                        }
                        
                        transaction.Commit();
                    }

                }
            }

            return notifications;
        }

        public IEnumerable<T> FindBy<T>(Expression<Func<T, bool>> predicate)
        {
            using (var session = databaseFactory.Session())
            {
                IList<T> results = session.Query<T>().Where(predicate).ToList();
                return results;
            }
        }

        public IEnumerable<T> FindAll<T>()
        {
            using (var session = databaseFactory.Session())
            {
                IList<T> results = session.Query<T>().ToList();
                return results;
            }
        }

        public T FindById<T>(int id)
        {
            using (var session = databaseFactory.Session())
            {
                return session.Load<T>(id);
            }
        }

        public NotificationCollection Delete<T>(int id)
        {
            var notifications = NotificationCollection.CreateEmpty();

            using (var session = databaseFactory.Session())
            {
                var entity = session.Load<T>(id);
                notifications = Validate(entity, ValidationContextKeys.Delete);

                if (!notifications.HasErrors())
                {
                    using (var transaction = session.BeginTransaction())
                    {
                        session.Delete(entity);
                        transaction.Commit();
                    }
                }
            }

            return notifications;
        }
        
        #endregion

        #region Private methods

        private NotificationCollection Validate(object entity, ValidationContextKeys validationContextKey)
        {
            var notifications = NotificationCollection.CreateEmpty();

            //TODO there must be a better way to find the rules than using "MappingAssembly"
            var types = Assembly.Load(databaseFactory.MappingAssembly).GetTypes().Where(t => typeof(IRule).IsAssignableFrom(t) && !t.IsAbstract);

            foreach (var type in types)
            {
                var ruleType = type.GetCustomAttributes(typeof(RuleAttribute), false).First() as RuleAttribute;
                var context = type.GetCustomAttributes(typeof(RuleContextAttribute), false).First() as RuleContextAttribute;

                if (ruleType.EntityType == entity.GetType() && context.Key == validationContextKey)
                {
                    var rule = Activator.CreateInstance(type, true) as IRule;
                    notifications += rule.Validate(entity, this);
                }
            }

            return notifications;
        }

        #endregion

        public void Dispose()
        {
            if (databaseFactory.IsNotNull())
            {
                databaseFactory.Dispose();
                databaseFactory = null;
            }
        }
    }
}

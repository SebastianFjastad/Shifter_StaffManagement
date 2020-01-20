using System;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;

namespace Shifter.Persistence.Data
{
    public class DatabaseFactory : IDatabaseFactory
    {
        #region Constructor

        public DatabaseFactory(string mappingAssembly)
        {
            this.MappingAssembly = mappingAssembly;
        }

        #endregion

        #region Locals

        public string MappingAssembly { get; private set; }

        private ISessionFactory sessionFactory;

        #endregion

        #region Methods

        private void InitializeSessionFactory()
        {
            sessionFactory =
                Fluently.Configure()
                    .Database(MsSqlConfiguration.MsSql2008.ConnectionString(c => c.FromConnectionStringWithKey("ShifterConnection"))
                    .Driver("NHibernate.Driver.Sql2008ClientDriver"))
                    .Mappings(m => m.FluentMappings.AddFromAssembly(GetType().Assembly))
                    .BuildSessionFactory();
        }

        /// <summary>
        /// Gets the nhibernate session
        /// </summary>
        public ISession Session()
        {
            if (sessionFactory.IsNull())
            {
                InitializeSessionFactory();
            }

            return sessionFactory.OpenSession();
        }

        #endregion

        public void Dispose()
        {
            if (sessionFactory.IsNotNull())
            {
                sessionFactory.Close();
                sessionFactory = null;
            }
        }
    }
}

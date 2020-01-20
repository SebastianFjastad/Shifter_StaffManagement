using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using FluentNHibernate.Data;
using Framework.Extensions;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;

namespace Shifter.Manager.Persistence
{
    public static class DatabaseFactory //: IDatabaseFactory
    {
        private static ISessionFactory sessionFactory;

        private static void InitializeSessionFactory()
        {
            const string dbServer = "localhost";
            const string dbUsername = "root";
            const string dbName = "nhibernate";
            const string dbPassword = "";
            const string mappingAssembly = "Shifter.Manager.Persistence.Mappings";

            sessionFactory = Fluently.Configure().Database(MsSqlConfiguration.MsSql2008
                              .ConnectionString(cs => cs.Server(dbServer).Database(dbName).Username(dbUsername).Password(dbPassword)))
                              .Mappings(x => x.FluentMappings.AddFromAssembly(Assembly.Load(mappingAssembly))).ExposeConfiguration(BuildSchema).BuildSessionFactory();
        }
        
        private static void BuildSchema(Configuration config)
        {
            new SchemaExport(config).Create(true, true);
        }

        public static ISession Session()
        {
            if (sessionFactory.IsNull())
            {
                InitializeSessionFactory();
            }

            return sessionFactory.OpenSession();
        }
    }
}

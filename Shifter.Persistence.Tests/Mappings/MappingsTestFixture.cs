using System.Collections.Generic;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using FluentNHibernate.Testing;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;
using NUnit.Framework;
using Shifter.Domain.Model.Entities;
using Shifter.Persistence.Mappings;

namespace Shifter.Persistence.Tests.Mappings
{
    [TestFixture]
    public class MappingsTestFixture : FixtureBase
    {
        [Test]
        public void EnsureMappingsAreCorrect()
        {
            new PersistenceSpecification<Staff>(Session)
                .CheckProperty(c => c.Id, 1)
                .CheckProperty(c => c.UserAccount.FirstName, "John")
                .CheckProperty(c => c.UserAccount.LastName, "Doe")
                .CheckProperty(c => c.CanSwapShifts, true)
                .CheckProperty(c => c.CanWorkDoubles, false)
                .CheckProperty(c => c.MaxNumberOfShiftsPerWeek, 1)
                .CheckProperty(c => c.UserAccount.ContactNumber, "011")
                .CheckProperty(c => c.UserAccount.EmailAddress, "abc")
                .VerifyTheMappings();

            new PersistenceSpecification<Manager>(Session)
                .CheckProperty(c => c.Id, 1)
                .CheckProperty(c => c.UserAccount.FirstName, "John")
                .CheckProperty(c => c.UserAccount.LastName, "Doe")
                .CheckProperty(c => c.UserAccount.ContactNumber, "011")
                .CheckProperty(c => c.UserAccount.EmailAddress, "abc")
                .CheckList(c => c.Restaurants, new List<Restaurant>() {new Restaurant() {Id = 1}})
                .VerifyTheMappings();
        }
    }

    public class FixtureBase
    {
        protected ISession Session { get; private set; }
        protected static Configuration Config { get; private set; }

        private static ISessionFactory factory;

        [SetUp]
        public void SetupContext()
        {
            factory = Fluently.Configure().Database(SQLiteConfiguration.Standard.InMemory)
                .Mappings(m => m.FluentMappings.AddFromAssembly(typeof(ManagerMapping).Assembly))
                .ExposeConfiguration(x => Config = x)
                .BuildSessionFactory();

            Session = factory.OpenSession();

            var export = new SchemaExport(Config);
            export.Execute(true, true, false, Session.Connection, null);
        }

        [TearDown]
        public void Reset()
        {
            Session.Close();
            Session.Dispose();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Framework.Email;
using Framework.Encryption;
using Shifter.Domain.Model.Entities;
using Shifter.Domain.Services;
using Shifter.Persistence.Data;

namespace DbConnectionTester
{
    class Program
    {
        static void Main(string[] args)
        {
            //var service = new ManagerDomainService(new Repository(new DatabaseFactory("Shifter.Domain")), new EmailManager(""), new PasswordEncryptor(new PBKDF2()), new PasswordGenerator());
            var repo = new Repository(new DatabaseFactory("Shifter.Domain"));
            var manager = repo.FindAll<Manager>();
            Console.WriteLine(manager.First().UserAccount.FirstName);
            Console.ReadLine();
        }
    }
}

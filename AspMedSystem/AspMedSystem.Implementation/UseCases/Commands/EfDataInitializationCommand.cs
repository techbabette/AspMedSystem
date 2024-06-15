using AspMedSystem.Application.Exceptions;
using AspMedSystem.Application.UseCases.Commands;
using AspMedSystem.DataAccess;
using AspMedSystem.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspMedSystem.Implementation.UseCases.Commands
{
    public class EfDataInitializationCommand : EfUseCase, IDataInitializationCommand
    {
        public string Name => "Data Initialization";

        public EfDataInitializationCommand(MedSystemContext context) : base(context)
        {
            
        }

        private EfDataInitializationCommand() { }

        public void Execute(bool data)
        {
            if (Context.Users.Any())
            {
                throw new ConflictException("Data already initialized");
            }

            Group Registered = new Group()
            {
                DefaultRegister = true,
                Name = "Registered",
                GroupPermissions = new List<GroupPermission>()
                {

                }
            };

            Group Doctor = new Group()
            {
                DefaultRegister = false,
                Name = "Doctor",
                GroupPermissions = new List<GroupPermission>()
                {

                }
            };

            var AdminPermissions = new List<GroupPermission>();

            var adminAllowed = UseCaseInfo.AllUseCases.Select(useCase => new GroupPermission
            {
                Effect = Effect.Allow,
                Permission = useCase
            });

            AdminPermissions.AddRange(adminAllowed);

            Group Admin = new Group()
            {
                DefaultRegister = false,
                Name = "Admin",
                GroupPermissions = AdminPermissions
            };

            User adminUser = new User()
            {
                Group = Admin,
                Email = "admin@gmail.com",
                Password = BCrypt.Net.BCrypt.HashPassword("admin123"),
                FirstName = "Admin",
                LastName = "Adminkovic",
            };

            Context.Groups.Add(Registered);
            Context.Groups.Add(Doctor);
            Context.Groups.Add(Admin);

            Context.SaveChanges();
        }
    }
}

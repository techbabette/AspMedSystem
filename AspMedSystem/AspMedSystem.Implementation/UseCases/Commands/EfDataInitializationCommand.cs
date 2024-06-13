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

        internal EfDataInitializationCommand() { }

        public void Execute(bool data)
        {
            if (Context.Groups.Any())
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

            Group Scheduler = new Group()
            {
                DefaultRegister = false,
                Name = "Scheduler",
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

            Group Admin = new Group()
            {
                DefaultRegister = false,
                Name = "Admin",
                GroupPermissions = new List<GroupPermission>()
                {

                }
            };

            Context.Groups.Add(Registered);
            Context.Groups.Add(Scheduler);
            Context.Groups.Add(Doctor);
            Context.Groups.Add(Admin);

            Context.SaveChanges();
        }
    }
}

using AspMedSystem.Application.Exceptions;
using AspMedSystem.Application.UseCases.Commands;
using AspMedSystem.DataAccess;
using AspMedSystem.Domain;
using Microsoft.EntityFrameworkCore.Storage.Json;
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

            var registeredAllowed = UseCaseInfo.RegisteredDefaultUseCases.Select(x => new GroupPermission
            {
                Effect = Effect.Allow,
                Permission = x
            }).ToList();

            Group Registered = new Group()
            {
                DefaultRegister = true,
                Name = "Registered",
                GroupPermissions = registeredAllowed
            };

            var schedulerAllowed = UseCaseInfo.SchedulerDefaultUseCases.Select(x => new GroupPermission
            {
                Effect = Effect.Allow,
                Permission = x
            }).ToList();

            Group Scheduler = new Group()
            {
                DefaultRegister = false,
                Name = "Scheduler",
                GroupPermissions = schedulerAllowed
            };

            var doctorAllowed = UseCaseInfo.DoctorDefaultUseCases.Select(x => new GroupPermission
            {
                Effect = Effect.Allow,
                Permission = x
            }).ToList();

            Group Doctor = new Group()
            {
                DefaultRegister = false,
                Name = "Doctor",
                GroupPermissions = doctorAllowed
            };

            var adminAllowed = UseCaseInfo.AllUseCases.Select(useCase => new GroupPermission
            {
                Effect = Effect.Allow,
                Permission = useCase
            }).ToList();

            Group Admin = new Group()
            {
                DefaultRegister = false,
                Name = "Admin",
                GroupPermissions = adminAllowed
            };

            User registeredUser = new User()
            {
                Group = Registered,
                Email = "registered@gmail.com",
                Password = BCrypt.Net.BCrypt.HashPassword("Admin123"),
                FirstName = "User",
                LastName = "Userkovic",
                BirthDate = DateTime.Now.AddYears(-20)
            };

            User schedulerUser = new User()
            {
                Group = Scheduler,
                Email = "scheduler@gmail.com",
                Password = BCrypt.Net.BCrypt.HashPassword("Admin123"),
                FirstName = "Scheduler",
                LastName = "Schedulerikovic",
                BirthDate = DateTime.Now.AddYears(-20)
            };

            User doctorUser = new User()
            {
                Group = Doctor,
                Email = "doctor@gmail.com",
                Password = BCrypt.Net.BCrypt.HashPassword("Admin123"),
                FirstName = "Doctor",
                LastName = "Doctorovic",
                BirthDate = DateTime.Now.AddYears(-20)
            };

            User theDoctorUser = new User()
            {
                Group = Doctor,
                Email = "thedoctor@gmail.com",
                Password = BCrypt.Net.BCrypt.HashPassword("Admin123"),
                FirstName = "Doctor",
                LastName = "Doctorovic",
                BirthDate = DateTime.Now.AddYears(-20)
            };

            User adminUser = new User()
            {
                Group = Admin,
                Email = "admin@gmail.com",
                Password = BCrypt.Net.BCrypt.HashPassword("Admin123"),
                FirstName = "Admin",
                LastName = "Adminkovic",
                BirthDate = DateTime.Now.AddYears(-20)
            };

            ExaminationTerm term1 = new ExaminationTerm { Date = DateTime.Now.AddDays(-20), Examiner = theDoctorUser };
            ExaminationTerm term2 = new ExaminationTerm { Date = DateTime.Now.AddDays(20), Examiner = doctorUser };

            var examination1 = new Examination { ExaminationTerm = term1, Examinee = registeredUser, Perfomed = true };
            var examination2 = new Examination { ExaminationTerm = term2, Examinee = registeredUser, Perfomed = false };

            theDoctorUser.ExaminationTerms.Add(term1);
            doctorUser.ExaminationTerms.Add(term2);

            var report1 = new Report { Examination = examination1, Text = "They're okay, don't worry, they could use some supplements though" };

            Treatment treatment1 = new Treatment
            {
                Name = "Vitamin D3",
                Prescribable = true,
            };

            Treatment treatment2 = new Treatment
            {
                Name = "Vitamin B12",
                Prescribable = true,
            };

            UserTreatment userTreatment1 = new UserTreatment { 
                Report = report1,
                Treatment = treatment1,
                User = registeredUser,
                Note = "500 IU A Day",
                StartDate = DateTime.Now.AddDays(-19),
                EndDate = DateTime.Now,
            };

            UserTreatment userTreatment2 = new UserTreatment
            {
                Report = report1,
                Treatment = treatment2,
                User = registeredUser,
                Note = "Take the dose the instructions in the box suggest",
                StartDate = DateTime.Now.AddDays(30),
                EndDate = DateTime.Now.AddDays(60),
            };

            Context.Users.Add(registeredUser);
            Context.Users.Add(schedulerUser);
            Context.Users.Add(doctorUser);
            Context.Users.Add(theDoctorUser);
            Context.Users.Add(adminUser);

            Context.Examinations.Add(examination2);
            Context.UserTreatments.Add(userTreatment1);
            Context.UserTreatments.Add(userTreatment2);
            Context.SaveChanges();
        }
    }
}

using AspMedSystem.API.Core;
using AspMedSystem.Application;
using AspMedSystem.Application.UseCases.Commands;
using AspMedSystem.Application.UseCases.Commands.Auth;
using AspMedSystem.Application.UseCases.Commands.Groups;
using AspMedSystem.Application.UseCases.Queries.Groups;
using AspMedSystem.Implementation;
using AspMedSystem.Implementation.Logging.UseCases;
using AspMedSystem.Implementation.UseCases.Commands;
using AspMedSystem.Implementation.UseCases.Commands.Auth;
using AspMedSystem.Implementation.UseCases.Commands.Groups;
using AspMedSystem.Implementation.UseCases.Queries.Groups;
using AspMedSystem.Implementation.Validators;

namespace AspMedSystem.API.Extensions
{
    public static class ContainerExtensions
    {
        public static void AddUseCases(this IServiceCollection services)
        {
            services.AddTransient<IUseCaseLogger, DbUseCaseLogger>();
            services.AddTransient<UseCaseHandler>();
            services.AddTransient<JwtTokenCreator>();

            services.AddTransient<IDataInitializationCommand, EfDataInitializationCommand>();

            services.AddTransient<AuthRegisterValidator>();
            services.AddTransient<IAuthRegisterCommand,  EfAuthRegisterCommand>();

            services.AddTransient<GroupCreateValidator>();
            services.AddTransient<IGroupCreateCommand, EfGroupCreateCommand>();

            services.AddTransient<IGroupSearchQuery, EfGroupSearchQuery>();
            services.AddTransient<IGroupSearchSingleQuery, EfGroupSearchSingleQuery>();

            services.AddTransient<GroupUpdateValidator>();
            services.AddTransient<IGroupUpdateCommand, EfGroupUpdateCommand>();
        }
    }
}

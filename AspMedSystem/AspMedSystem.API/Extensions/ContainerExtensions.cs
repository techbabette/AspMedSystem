using AspMedSystem.Application;
using AspMedSystem.Application.UseCases.Commands;
using AspMedSystem.Implementation;
using AspMedSystem.Implementation.Logging.UseCases;
using AspMedSystem.Implementation.UseCases.Commands;

namespace AspMedSystem.API.Extensions
{
    public static class ContainerExtensions
    {
        public static void AddUseCases(this IServiceCollection services)
        {
            services.AddTransient<IUseCaseLogger, DbUseCaseLogger>();
            services.AddTransient<UseCaseHandler>();

            services.AddTransient<IDataInitializationCommand, EfDataInitializationCommand>();
        }
    }
}

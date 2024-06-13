using AspMedSystem.Application;
using AspMedSystem.Implementation;
using AspMedSystem.Implementation.Logging.UseCases;

namespace AspMedSystem.API.Extensions
{
    public static class ContainerExtensions
    {
        public static void AddUseCases(this IServiceCollection services)
        {
            services.AddTransient<IUseCaseLogger, DbUseCaseLogger>();
            services.AddTransient<UseCaseHandler>();
        }
    }
}

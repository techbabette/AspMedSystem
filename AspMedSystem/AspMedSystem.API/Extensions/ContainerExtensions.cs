using AspMedSystem.API.Core;
using AspMedSystem.Application;
using AspMedSystem.Application.UseCases.Commands;
using AspMedSystem.Application.UseCases.Commands.Auth;
using AspMedSystem.Application.UseCases.Commands.Examinations;
using AspMedSystem.Application.UseCases.Commands.ExaminationTerms;
using AspMedSystem.Application.UseCases.Commands.Groups;
using AspMedSystem.Application.UseCases.Commands.Reports;
using AspMedSystem.Application.UseCases.Commands.Users;
using AspMedSystem.Application.UseCases.Queries.Examinations;
using AspMedSystem.Application.UseCases.Queries.ExaminationTerms;
using AspMedSystem.Application.UseCases.Queries.Groups;
using AspMedSystem.Application.UseCases.Queries.Reports;
using AspMedSystem.Application.UseCases.Queries.Users;
using AspMedSystem.Implementation;
using AspMedSystem.Implementation.Logging.UseCases;
using AspMedSystem.Implementation.UseCases.Commands;
using AspMedSystem.Implementation.UseCases.Commands.Auth;
using AspMedSystem.Implementation.UseCases.Commands.Examinations;
using AspMedSystem.Implementation.UseCases.Commands.ExaminationTerms;
using AspMedSystem.Implementation.UseCases.Commands.Groups;
using AspMedSystem.Implementation.UseCases.Commands.Reports;
using AspMedSystem.Implementation.UseCases.Commands.Users;
using AspMedSystem.Implementation.UseCases.Queries.Examinations;
using AspMedSystem.Implementation.UseCases.Queries.ExaminationTerms;
using AspMedSystem.Implementation.UseCases.Queries.Groups;
using AspMedSystem.Implementation.UseCases.Queries.Reports;
using AspMedSystem.Implementation.UseCases.Queries.Users;
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
            services.AddTransient<IGroupDeleteCommand, EfGroupDeleteCommand>();

            services.AddTransient<IUserSearchSingleInformationQuery, EfUserSearchSingleInformationQuery>();
            services.AddTransient<IUserSearchSinglePermissionsQuery, EfUserSearchSinglePermissionsQuery>();
            services.AddTransient<IUserSearchSelfInformationQuery, EfUserSearchSelfInformationQuery>();
            services.AddTransient<IUserSearchSelfPermissionsQuery, EfUserSearchSelfPermissionsQuery>();
            services.AddTransient<IUserSearchQuery, EfUserSearchQuery>();
            services.AddTransient<UserUpdateInformationValidator>();
            services.AddTransient<IUserUpdateInformationSelfCommand, EfUserUpdateInformationSelfCommand>();
            services.AddTransient<IUserUpdateInformationOthersCommand, EfUserUpdateInformationOthersCommand>();
            services.AddTransient<UserUpdatePermissionsValidator>();
            services.AddTransient<IUserUpdatePermissionsCommand,  EfUserUpdatePermissionsCommand>();
            services.AddTransient<IUserUpdateGroupCommand, EfUserUpdateGroupCommand>();
            services.AddTransient<IUserDeleteCommand, EfUserDeleteCommand>();

            services.AddTransient<ExaminationTermCreateValidator>();
            services.AddTransient<IExaminationTermCreateCommand, EfExaminationTermCreateCommand>();
            services.AddTransient<ExaminationTermUpdateValidator>();
            services.AddTransient<IExaminationTermUpdateCommand, EfExaminationTermUpdateCommand>();
            services.AddTransient<IExaminationTermDeleteCommand, EfExaminationTermDeleteCommand>();
            services.AddTransient<IExaminationTermSearchQuery, EfExaminationTermSearchQuery>();

            services.AddTransient<IExaminationSearchQuery, EfExaminationSearchQuery>();
            services.AddTransient<IExaminationCreateCommand, EfExaminationCreateCommand>();
            services.AddTransient<IExaminationPerformedCommand, EfExaminationPerformedCommand>();
            services.AddTransient<IExaminationDeleteCommand, EfExaminationDeleteCommand>();

            services.AddTransient<IReportSearchQuery, EfReportSearchQuery>();
            services.AddTransient<IReportSearchSingleQuery, EfReportSearchSingleQuery>();
            services.AddTransient<ReportCreateValidator>();
            services.AddTransient<IReportCreateCommand, EfReportCreateCommand>();
            services.AddTransient<IReportDeleteCommand, EfReportDeleteCommand>();
        }
    }
}

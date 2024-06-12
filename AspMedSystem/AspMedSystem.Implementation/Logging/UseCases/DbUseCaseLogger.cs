using AspMedSystem.Application;
using AspMedSystem.DataAccess;
using Newtonsoft.Json;
namespace AspMedSystem.Implementation.Logging.UseCases
{
    public class DbUseCaseLogger : IUseCaseLogger
    {
        private readonly MedSystemContext _context;

        public DbUseCaseLogger(MedSystemContext context)
        {
            _context = context;
        }

        public void Log(UseCaseLog log)
        {
            _context.UseCaseLogs.Add(new Domain.UseCaseLog
            {
                Data = JsonConvert.SerializeObject(log.UseCaseData),
                ActorEmail = log.ActorEmail,
                UseCaseName = log.UseCaseName
            });
        }
    }
}

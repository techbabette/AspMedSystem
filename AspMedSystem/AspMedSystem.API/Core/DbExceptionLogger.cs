using AspMedSystem.Application;
using AspMedSystem.DataAccess;
using AspMedSystem.Domain;

namespace AspMedSystem.API.Core
{
    public class DbExceptionLogger : IExceptionLogger
    {
        private readonly MedSystemContext _medSystemContext;

        public DbExceptionLogger(MedSystemContext medSystemContext)
        {
            _medSystemContext = medSystemContext;
        }

        public Guid Log(Exception ex, IApplicationActor actor)
        {
            Guid id = Guid.NewGuid();
            //ID, Message, Time, StrackTrace
            ErrorLog log = new()
            {
                ErrorId = id,
                Message = ex.Message,
                StrackTrace = ex.StackTrace,
                Time = DateTime.UtcNow
            };

            _medSystemContext.ErrorLogs.Add(log);

            _medSystemContext.SaveChanges();

            return id;
        }
    }
}

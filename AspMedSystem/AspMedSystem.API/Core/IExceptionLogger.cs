using AspMedSystem.Application;

namespace AspMedSystem.API.Core
{
    public interface IExceptionLogger
    {
        Guid Log(Exception ex, IApplicationActor actor);
    }
}

using AspMedSystem.Application.UseCases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspMedSystem.Implementation
{
    public static class UseCaseInfo
    {
        public static IEnumerable<string> AllUseCases
        {
            get
            {
                var type = typeof(IUseCase);
                var types = typeof(UseCaseInfo).Assembly.GetTypes()
                                .Where(p => type.IsAssignableFrom(p))
                                .Where(p => p.GetConstructor(Type.EmptyTypes) != null)
                                .Where(p => !p.IsInterface && !p.IsAbstract)
                                .Select(x => Activator.CreateInstance(x));

                List<string> result = new List<string>();

                foreach(IUseCase currentType in types)
                {
                    result.Add(currentType.Name);
                }

                return result;
            }
        }
    }
}

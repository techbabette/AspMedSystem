using AspMedSystem.Application.UseCases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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
                                .Where(p => typeof(IUseCase).IsAssignableFrom(p))
                                .Where(p => p.GetConstructor(BindingFlags.Instance
                                                             | BindingFlags.NonPublic,
                                                             null,
                                                             Type.EmptyTypes,
                                                             null) != null)
                                .Where(p => !p.IsInterface && !p.IsAbstract)
                                .Select(x => Activator.CreateInstance(x, true));

                List<string> result = new List<string>();

                foreach (IUseCase currentType in types)
                {
                    result.Add(currentType.Name);
                }

                return result;
            }
        }
    }
}

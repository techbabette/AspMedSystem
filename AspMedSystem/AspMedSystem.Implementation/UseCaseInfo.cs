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
                    result.Add(currentType.Name.ToLower());
                }

                return result;
            }
        }

        public static string performExaminationPerm => "mark examination as performed";

        public static IEnumerable<string> RegisteredDefaultUseCases
        {
            get
            {
                return new List<string>()
                {
                        "search prescriptions as prescribee",
                        "show prescription as prescribee",
                        "search users",
                        "show your own information",
                        "show your own permissions",
                        "search reports as examinee",
                        "show examination as examinee",
                        "search examiners",
                        "show examiner",
                        "search examination terms",
                        "search examinations as examinee",
                        "search examinations",
                        "show your examinations as examinee",
                        "show any examination",
                        "delete user",
                        "update user group",
                        "updated others' information",
                        "update your own information",
                        "update user permissions",
                        "schedule an examination",
                        "unschedule examination",
                        "register"
                };
            }
        }

        public static IEnumerable<string> SchedulerDefaultUseCases
        {
            get
            {
                return new List<string>()
                {
                        "search prescriptions as prescribee",
                        "search prescriptions",
                        "show prescription as prescribee",
                        "search users",
                        "show your own information",
                        "show your own permissions",
                        "show information about other user",
                        "search reports as examinee",
                        "show examination as examinee",
                        "search examiners",
                        "show examiner",
                        "search examination terms",
                        "search examinations as examinee",
                        "search examinations",
                        "show your examinations as examinee",
                        "show any examination",
                        "delete user",
                        "update user group",
                        "updated others' information",
                        "update your own information",
                        "update user permissions",
                        "publish examination term",
                        "delete examination term",
                        "update examination term",
                        "schedule an examination",
                        "unschedule examination",
                        "register"
                };
            }
        }
        public static IEnumerable<string> DoctorDefaultUseCases
        {
            get
            {
                return new List<string>()
                {
                        "search prescriptions as prescribee",
                        "search prescriptions as prescriber",
                        "search prescriptions",
                        "show prescription as prescribee",
                        "show prescription as prescriber",
                        "show prescription",
                        "search users",
                        "show your own information",
                        "show your own permissions",
                        "show information about other user",
                        "show other users permissions",
                        "search treatments",
                        "show treatment",
                        "search reports as examinee",
                        "search reports as examiner",
                        "search reports",
                        "show examination as examinee",
                        "show examination as examiner",
                        "show report",
                        "search groups",
                        "search single group",
                        "search examiners",
                        "show examiner",
                        "search examination terms",
                        "search examinations as examinee",
                        "search examinations as examiner",
                        "search examinations",
                        "show your examinations as examinee",
                        "show your examinations as examiner",
                        "show any examination",
                        "data initialization",
                        "prescribe user treatment",
                        "stop treatment",
                        "update prescription",
                        "delete user",
                        "update user group",
                        "updated others' information",
                        "update your own information",
                        "update user permissions",
                        "create report",
                        "delete report",
                        "update report",
                        "publish examination term",
                        "delete examination term",
                        "update examination term",
                        "schedule an examination",
                        "unschedule examination",
                        "mark examination as performed",
                        "register"
                };
            }
        }
        public static IEnumerable<string> AdminDefaultUseCases
        {
            get
            {
                return new List<string>
                {
                        "search prescriptions as prescribee",
                        "search prescriptions as prescriber",
                        "search prescriptions",
                        "show prescription as prescribee",
                        "show prescription as prescriber",
                        "show prescription",
                        "search users",
                        "show your own information",
                        "show your own permissions",
                        "show information about other user",
                        "show other users permissions",
                        "search treatments",
                        "show treatment",
                        "search reports as examinee",
                        "search reports as examiner",
                        "search reports",
                        "show examination as examinee",
                        "show examination as examiner",
                        "show report",
                        "search groups",
                        "search single group",
                        "search examiners",
                        "show examiner",
                        "search examination terms",
                        "search examinations as examinee",
                        "search examinations as examiner",
                        "search examinations",
                        "show your examinations as examinee",
                        "show your examinations as examiner",
                        "show any examination",
                        "search audit logs",
                        "show audit log",
                        "data initialization",
                        "prescribe user treatment",
                        "stop treatment",
                        "update prescription",
                        "delete user",
                        "update user group",
                        "updated others' information",
                        "update your own information",
                        "update user permissions",
                        "update treatment",
                        "create treatment",
                        "delete treatment",
                        "create report",
                        "delete report",
                        "update report",
                        "create group",
                        "delete group",
                        "update group",
                        "publish examination term",
                        "delete examination term",
                        "update examination term",
                        "schedule an examination",
                        "unschedule examination",
                        "mark examination as performed",
                        "register"
                };
            }
        }
    }
}

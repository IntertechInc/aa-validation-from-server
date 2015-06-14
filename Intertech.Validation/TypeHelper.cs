using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Intertech.Validation
{
    public class TypeHelper
    {
        /// <summary>
        /// Get the Type for the given object name.
        /// </summary>
        /// <param name="objectName"></param>
        /// <returns></returns>
        public static Type GetObjectType(string objectName, bool isNameFullyQualified, string alternateNamespace, params string[] assemblyNames)
        {
            Type type = null;

            AppDomain.CurrentDomain.ReflectionOnlyAssemblyResolve += CurrentDomain_ReflectionOnlyAssemblyResolve;
            AppDomain.CurrentDomain.ReflectionOnlyAssemblyResolve += CurrentDomain_ReflectionOnlyAssemblyResolve;

            foreach (var asmName in assemblyNames)
            {
                var assembly = Assembly.Load(asmName);
                type = GetTypeFromAssembly(assembly, objectName, isNameFullyQualified, alternateNamespace);
            }

            return type;
        }

        static Assembly CurrentDomain_ReflectionOnlyAssemblyResolve(object sender, ResolveEventArgs args)
        {
            return Assembly.ReflectionOnlyLoad(args.Name);
        }

        private static Type GetTypeFromAssembly(Assembly assembly, string dtoObjectName, bool isNameFullyQualified, string alternateNamespace)
        {
            string typeName;
            Type type = null;

            if (assembly != null)
            {
                var asmName = assembly.FullName.Substring(0, assembly.FullName.IndexOf(','));
                typeName = isNameFullyQualified ? dtoObjectName : string.Format("{0}.{1}", asmName, dtoObjectName);
                type = assembly.GetType(typeName);

                if (type == null)
                {
                    if (!string.IsNullOrWhiteSpace(alternateNamespace))
                    {
                        typeName = string.Format("{0}.{1}", alternateNamespace, dtoObjectName);
                    }
                    else
                    {
                        typeName = dtoObjectName;
                    }
                    type = assembly.GetType(typeName);
                }
            }

            return type;
        }
    }
}

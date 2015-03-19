using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Intertech.Validation.Converters;
using System.Globalization;

namespace Intertech.Validation
{
    public class ValidationHelper
    {
        private static List<IValidationConverter> _converters;
        public List<IValidationConverter> Converters { get { return _converters; } }

        /// <summary>
        /// Constructor that initializes the list of IValidationConverters.
        /// </summary>
        public ValidationHelper()
        {
            if (_converters == null)
            {
                var types = GetAllValidationConverters();
                if (types != null)
                {
                    _converters = new List<IValidationConverter>();

                    foreach (var t in types)
                    {
                        var constructorInfo = t.GetConstructor(System.Type.EmptyTypes);
                        if (constructorInfo != null)
                        {
                            var vcObj = constructorInfo.Invoke(null);
                            if (vcObj != null)
                            {
                                _converters.Add(vcObj as IValidationConverter);
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Get the validations for dtoObjectName and return the json object.
        /// </summary>
        /// <param name="dtoObjectName"></param>
        /// <param name="jsonObjectName"></param>
        /// <param name="assemblyNames">Names of assemblies to check</param>
        /// <returns></returns>
        public object GetValidations(string dtoObjectName, string jsonObjectName, string alternateNamespace, bool useCamelCaseForProperties, params string[] assemblyNames)
        {
            var jsonString = new StringBuilder("{ validations: {");

            GetValidationsForDto(dtoObjectName, jsonObjectName, jsonString, false, alternateNamespace, useCamelCaseForProperties, assemblyNames);

            jsonString.Append("} }");

            return JObject.Parse(jsonString.ToString());
        }

        #region Private Methods

        /// <summary>
        /// Get all validation converters in this assembly.
        /// </summary>
        /// <returns></returns>
        private IEnumerable<Type> GetAllValidationConverters()
        {
            var valAssembly = typeof(IValidationConverter).Assembly;

            var registrations =
                from type in valAssembly.GetExportedTypes()
                where type.Namespace == "Intertech.Validation.Converters"
                where type.GetInterfaces().Any(t => t == typeof(IValidationConverter))
                select type;

            return registrations.AsEnumerable<Type>();
        }

        private void GetValidationsForDto(string dtoObjectName, string jsonObjectName, StringBuilder jsonString, bool isContainedDto, string alternateNamespace, bool useCamelCaseForProperties, params string[] assemblyNames)
        {
            if (isContainedDto)
            {
                jsonString.Append(", ");
            }

            jsonString.Append(jsonObjectName + ": { ");

            var dtoClass = GetDtoType(dtoObjectName, alternateNamespace, assemblyNames);
            if (dtoClass == null)
            {
                var message = string.Format("DTO '{0}' not found.", dtoObjectName);
                throw new Exception(message);
            }

            var properties = dtoClass.GetProperties();
            if (properties != null)
            {
                var isFirstProp = true;

                foreach (var prop in properties)
                {
                    if (prop.CustomAttributes != null && prop.CustomAttributes.Count() > 0)
                    {
                        var attrStr = new StringBuilder();
                        var isFirstAttr = true;

                        foreach (var attr in prop.CustomAttributes)
                        {
                            var converter = _converters.FirstOrDefault(vc => vc.IsAttributeMatch(attr));
                            if (converter != null)
                            {
                                converter.Convert(prop.Name, attr, attrStr, isFirstAttr);
                                isFirstAttr = false;
                            }
                        }

                        if (!isFirstAttr)
                        {
                            var sep = isFirstProp ? string.Empty : ",";
							if (useCamelCaseForProperties)
							{
								jsonString.Append(sep + CamelCaseProperty(prop.Name) + ": { ");
							}
							else
							{
								jsonString.Append(sep + prop.Name + ": { ");
							}
                            jsonString.Append(attrStr.ToString());
                            jsonString.Append("}");

                            isFirstProp = false;
                        }
                    }
                }
            }

            jsonString.Append("}");
        }

        /// <summary>
        /// Get the Type for the given DTO name.
        /// </summary>
        /// <param name="dtoObjectName"></param>
        /// <returns></returns>
        private Type GetDtoType(string dtoObjectName, string alternateNamespace, params string[] assemblyNames)
        {
            Type type = null;

            AppDomain.CurrentDomain.ReflectionOnlyAssemblyResolve -= CurrentDomain_ReflectionOnlyAssemblyResolve;
            AppDomain.CurrentDomain.ReflectionOnlyAssemblyResolve += CurrentDomain_ReflectionOnlyAssemblyResolve;

            foreach (var asmName in assemblyNames)
            {
                var assembly = Assembly.ReflectionOnlyLoad(asmName);
                type = GetTypeFromAssembly(assembly, dtoObjectName, alternateNamespace);
            }

            return type;
        }

        Assembly CurrentDomain_ReflectionOnlyAssemblyResolve(object sender, ResolveEventArgs args)
        {
            return Assembly.ReflectionOnlyLoad(args.Name);
        }

        private Type GetTypeFromAssembly(Assembly assembly, string dtoObjectName, string alternateNamespace)
        {
            string typeName;
            Type type = null;

            if (assembly != null)
            {
                var asmName = assembly.FullName.Substring(0, assembly.FullName.IndexOf(','));
                typeName = string.Format("{0}.{1}", asmName, dtoObjectName);
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

		private string CamelCaseProperty(string input)
		{
			if (string.IsNullOrEmpty(input) || !char.IsUpper(input[0]))
			{
				return input;
			}

			var sb = new StringBuilder();

			for (var i = 0; i < input.Length; ++i)
			{
				var flag = i + 1 < input.Length;
				if (i == 0 || !flag || char.IsUpper(input[i + 1]))
				{
					var ch = char.ToLower(input[i], CultureInfo.InvariantCulture);
					sb.Append(ch);
				}
				else
				{
					sb.Append(input.Substring(i));
					break;
				}
			}

			return sb.ToString();
		}



        #endregion Private Methods
    }
}

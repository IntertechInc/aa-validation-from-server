using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intertech.Validation
{
    /// <summary>
    /// Parameters for the GetValidations method to reduce complexity of the method signature.
    /// The old method signature will be backwards compatible.
    /// </summary>
    public class GetValidationsParms
    {
        public GetValidationsParms(string dtoObjectName, string jsonObjectName)
        {
            DtoObjectName = dtoObjectName;
            JsonObjectName = jsonObjectName;

            JsonString = new StringBuilder("{ validations: {");
        }

        public void CompleteJsonString()
        {
            JsonString.Append("} }");
        }

        public StringBuilder JsonString { get; set; }

        public string DtoObjectName { get; set; }

        public string JsonObjectName { get; set; }

        public string DtoAlternateNamespace { get; set; }

        public List<string> DtoAssemblyNames { get; set; }

        public string ResourceNamespace { get; set; }

        public string ResourceAssemblyName { get; set; }
    }
}

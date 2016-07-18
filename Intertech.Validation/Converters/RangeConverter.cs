using Intertech.Validation.Constants;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Intertech.Validation.Converters
{
    public class RangeConverter : BaseValidationConverter, IValidationConverter
    {
        public bool IsAttributeMatch(CustomAttributeData attr)
        {
            return IsMatch<RangeAttribute>(attr);
        }

        public void Convert(string propertyName, string displayName, CustomAttributeData attr, StringBuilder jsonString, bool isFirstAttr, string resourceNamespace, string resourceAssemblyName)
        {
            PrependComma(jsonString, isFirstAttr);

            var minimum = GetConstructorArgumentValue(attr, 0);
            var maximum = GetConstructorArgumentValue(attr, 1);
            if (!string.IsNullOrWhiteSpace(minimum) && !string.IsNullOrWhiteSpace(maximum))
            {
                jsonString.Append("'min': " + minimum);

                if (!string.IsNullOrWhiteSpace(displayName))
                {
                    var msg = GetErrorMessage(propertyName, attr, resourceNamespace, resourceAssemblyName);
                   
                    msg = string.Format(string.IsNullOrWhiteSpace(msg) ? DataAnnotationConstants.DefaultRangeErrorMsg : msg, displayName, minimum, maximum);

                    jsonString.Append(", 'min-msg': \"" + msg + "\"");

                    jsonString.Append(", 'max': " + maximum);
                    jsonString.Append(", 'max-msg': \"" + msg + "\"");
                }
            }
        }
    }
}

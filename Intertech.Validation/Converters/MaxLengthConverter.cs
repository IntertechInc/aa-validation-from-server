using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Intertech.Validation.Converters
{
    public class MaxLengthConverter : BaseValidationConverter, IValidationConverter
    {
        public bool IsAttributeMatch(CustomAttributeData attr)
        {
            return IsMatch<MaxLengthAttribute>(attr);
        }

        public void Convert(string propertyName, CustomAttributeData attr, StringBuilder jsonString, bool isFirstAttr, string resourceNamespace, string resourceAssemblyName)
        {
            var length = GetConstructorArgumentValue(attr, 0);
            if (!string.IsNullOrWhiteSpace(length))
            {
                SetMaxLengthAAValidation(propertyName, attr, jsonString, isFirstAttr, length, resourceNamespace, resourceAssemblyName);
            }
        }
    }
}

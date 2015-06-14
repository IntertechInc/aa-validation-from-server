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
    public class StringLengthConverter : BaseValidationConverter, IValidationConverter
    {
        public bool IsAttributeMatch(CustomAttributeData attr)
        {
            return IsMatch<StringLengthAttribute>(attr);
        }

        public void Convert(string propertyName, CustomAttributeData attr, StringBuilder jsonString, bool isFirstAttr, string resourceNamespace, string resourceAssemblyName)
        {
            var maxLength = GetConstructorArgumentValue(attr, 0);
            if (!string.IsNullOrWhiteSpace(maxLength))
            {
                SetMaxLengthAAValidation(propertyName, attr, jsonString, isFirstAttr, maxLength, resourceNamespace, resourceAssemblyName);
            }

            var minLength = base.GetNamedArgumentValue(propertyName, attr, DataAnnotationConstants.MinimumLength, false);
            if (!string.IsNullOrWhiteSpace(minLength))
            {
                SetMinLengthAAValidation(propertyName, attr, jsonString, false, minLength, resourceNamespace, resourceAssemblyName);
            }
        }
    }
}

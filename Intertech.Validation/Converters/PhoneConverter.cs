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
    public class PhoneConverter : BaseValidationConverter, IValidationConverter
    {
        public bool IsAttributeMatch(CustomAttributeData attr)
        {
            return IsMatch<PhoneAttribute>(attr);
        }

        public void Convert(string propertyName, string displayName, CustomAttributeData attr, StringBuilder jsonString, bool isFirstAttr, string resourceNamespace, string resourceAssemblyName)
        {
            SetRegularExpressionAAValidation(propertyName, displayName, attr, jsonString, isFirstAttr,
                RegexConstants.Phone, DataAnnotationConstants.DefaultPhoneErrorMsg, resourceNamespace, resourceAssemblyName);
        }
    }
}

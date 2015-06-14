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
    public class UrlConverter : BaseValidationConverter, IValidationConverter
    {
        public bool IsAttributeMatch(CustomAttributeData attr)
        {
            return IsMatch<UrlAttribute>(attr);
        }

        public void Convert(string propertyName, CustomAttributeData attr, StringBuilder jsonString, bool isFirstAttr, string resourceNamespace, string resourceAssemblyName)
        {
            SetRegularExpressionAAValidation(propertyName, attr, jsonString, isFirstAttr,
                RegexConstants.Url, DataAnnotationConstants.DefaultUrlErrorMsg, resourceNamespace, resourceAssemblyName);
        }
    }
}

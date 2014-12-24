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
    public class RegularExpressionConverter : BaseValidationConverter, IValidationConverter
    {
        public bool IsAttributeMatch(CustomAttributeData attr)
        {
            return IsMatch<RegularExpressionAttribute>(attr);
        }

        public void Convert(string propertyName, CustomAttributeData attr, StringBuilder jsonString, bool isFirstAttr)
        {
            var pattern = GetConstructorArgumentValue(attr, 0);
            if (!string.IsNullOrWhiteSpace(pattern))
            {
                SetRegularExpressionAAValidation(propertyName, attr, jsonString, isFirstAttr,
                    pattern, DataAnnotationConstants.DefaultRegexErrorMsg);
            }
        }
    }
}

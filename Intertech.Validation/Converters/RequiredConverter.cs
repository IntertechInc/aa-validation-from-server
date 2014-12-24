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
    public class RequiredConverter : BaseValidationConverter, IValidationConverter
    {
        public bool IsAttributeMatch(CustomAttributeData attr)
        {
            return IsMatch<RequiredAttribute>(attr);
        }

        public void Convert(string propertyName, CustomAttributeData attr, StringBuilder jsonString, bool isFirstAttr)
        {
            PrependComma(jsonString, isFirstAttr);

            jsonString.Append("required: true");

            var displayName = GetNamedArgumentValue(propertyName, attr, DataAnnotationConstants.Display);
            if (!string.IsNullOrWhiteSpace(displayName))
            {
                var msg = GetNamedArgumentValue(propertyName, attr, DataAnnotationConstants.ErrorMessage, false);
                if (string.IsNullOrWhiteSpace(msg))
                {
                    msg = string.Format(DataAnnotationConstants.DefaultRequiredErrorMsg, displayName);
                }
                jsonString.Append(", 'required-msg': \"" + msg + "\"");
            }
        }
    }
}

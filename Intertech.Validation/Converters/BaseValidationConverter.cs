using Intertech.Validation.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Intertech.Validation.Converters
{
    public class BaseValidationConverter
    {
        /// <summary>
        /// Does the given attribute match the type T passed in?
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="attr"></param>
        /// <returns></returns>
        protected bool IsMatch<T>(CustomAttributeData attr)
        {
            if (attr == null) return false;

            return String.Compare(attr.AttributeType.FullName, typeof(T).FullName) == 0;
        }

        /// <summary>
        /// Prepend a comma on the jsonString based on isFirstAttr.
        /// </summary>
        /// <param name="jsonString"></param>
        /// <param name="isFirstAttr"></param>
        protected void PrependComma(StringBuilder jsonString, bool isFirstAttr)
        {
            if (!isFirstAttr)
                jsonString.Append(", ");
        }

        /// <summary>
        /// Get a constructor argument at the given index from the given attribute.
        /// </summary>
        /// <param name="attr"></param>
        /// <param name="constructorIndex"></param>
        /// <returns></returns>
        protected string GetConstructorArgumentValue(CustomAttributeData attr, int constructorIndex)
        {
            string value = null;

            if (attr.ConstructorArguments != null && attr.ConstructorArguments.Count > constructorIndex)
            {
                value = attr.ConstructorArguments[constructorIndex].Value.ToString();
            }

            return value;
        }

        /// <summary>
        /// Get named argument value from the given attribute.
        /// </summary>
        /// <param name="propertyName"></param>
        /// <param name="attr"></param>
        /// <param name="nameOfArgument"></param>
        /// <param name="usePropertyNameIfNull"></param>
        /// <returns></returns>
        protected string GetNamedArgumentValue(string propertyName, CustomAttributeData attr, string nameOfArgument, bool usePropertyNameIfNull = true)
        {
            string value = null;

            if (attr.NamedArguments != null && attr.NamedArguments.Count > 0)
            {
                var namedArg = attr.NamedArguments.FirstOrDefault(na => na.MemberName == nameOfArgument);
                if (namedArg != null && namedArg.TypedValue != null && namedArg.TypedValue.Value != null)
                {
                    value = namedArg.TypedValue.Value.ToString();
                }
            }

            if (string.IsNullOrWhiteSpace(value) && usePropertyNameIfNull)
            {
                value = propertyName;
            }

            return value;
        }

        protected void SetRegularExpressionAAValidation(string propertyName, CustomAttributeData attr, StringBuilder jsonString, bool isFirstAttr,
            string regex, string defaultMsgFormat)
        {
            PrependComma(jsonString, isFirstAttr);

            jsonString.Append("'ng-pattern': \"/" + RegexConstants.GetRegularExpressionForJson(regex) + "/\"");

            var displayName = GetNamedArgumentValue(propertyName, attr, DataAnnotationConstants.Display);
            if (!string.IsNullOrWhiteSpace(displayName))
            {
                var msg = GetNamedArgumentValue(propertyName, attr, DataAnnotationConstants.ErrorMessage, false);
                if (string.IsNullOrWhiteSpace(msg))
                {
                    msg = string.Format(defaultMsgFormat, displayName);
                }
                jsonString.Append(", 'ng-pattern-msg': \"" + msg + "\"");
            }
        }

        protected void SetMaxLengthAAValidation(string propertyName, CustomAttributeData attr, StringBuilder jsonString, bool isFirstAttr,
            string length)
        {
            PrependComma(jsonString, isFirstAttr);

            jsonString.Append("'ng-maxlength': " + length);

            var displayName = GetNamedArgumentValue(propertyName, attr, DataAnnotationConstants.Display);
            if (!string.IsNullOrWhiteSpace(displayName))
            {
                var msg = GetNamedArgumentValue(propertyName, attr, DataAnnotationConstants.ErrorMessage, false);
                if (string.IsNullOrWhiteSpace(msg))
                {
                    msg = string.Format(DataAnnotationConstants.DefaultMaxLengthErrorMsg, displayName, length);
                }
                jsonString.Append(", 'ng-maxlength-msg': \"" + msg + "\"");
            }
        }

        protected void SetMinLengthAAValidation(string propertyName, CustomAttributeData attr, StringBuilder jsonString, bool isFirstAttr,
            string length)
        {
            PrependComma(jsonString, isFirstAttr);

            jsonString.Append("'ng-minlength': " + length);

            var displayName = GetNamedArgumentValue(propertyName, attr, DataAnnotationConstants.Display);
            if (!string.IsNullOrWhiteSpace(displayName))
            {
                var msg = GetNamedArgumentValue(propertyName, attr, DataAnnotationConstants.ErrorMessage, false);
                if (string.IsNullOrWhiteSpace(msg))
                {
                    msg = string.Format(DataAnnotationConstants.DefaultMinLengthErrorMsg, displayName, length);
                }
                jsonString.Append(", 'ng-minlength-msg': \"" + msg + "\"");
            }
        }
    }
}

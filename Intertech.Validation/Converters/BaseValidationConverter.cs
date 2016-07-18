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

        /// <summary>
        /// Get the error message for the given property.
        /// </summary>
        /// <param name="propertyName"></param>
        /// <param name="attr"></param>
        /// <returns></returns>
        protected string GetErrorMessage(string propertyName, CustomAttributeData attr, string resourceNamespace, string resourceAssemblyName)
        {
            // First see if the ErrorMessage property is there.
            var msg = GetNamedArgumentValue(propertyName, attr, DataAnnotationConstants.ErrorMessage, false);
            if (string.IsNullOrWhiteSpace(msg))
            {
                var resourceName = GetNamedArgumentValue(propertyName, attr, DataAnnotationConstants.ErrorMessageResourceName, false);
                if (!string.IsNullOrWhiteSpace(resourceName))
                {
                    var resourceTypeStr = GetNamedArgumentValue(propertyName, attr, DataAnnotationConstants.ErrorMessageResourceType, false);
                    if (!string.IsNullOrWhiteSpace(resourceTypeStr))
                    {
                        // Get the message from the resource.
                        try
                        {
                            var rtype = TypeHelper.GetObjectType(resourceTypeStr, true, resourceNamespace, resourceAssemblyName);
                            var resName = rtype.GetProperty(resourceName, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static);
                            msg = resName.GetValue(null) as string;
                        }
                        catch
                        {
                            msg = null;
                        }
                    }
                }
            }

            if(msg == null)
            {
                //valide the resource default value
                try
                {
                    if(attr.AttributeType.Name.EndsWith("Attribute"))
                    {
                        var rtype = TypeHelper.GetObjectType(resourceNamespace, true, resourceNamespace, resourceAssemblyName);
                        if (rtype != null)
                        {
                            var resName = rtype.GetProperty(attr.AttributeType.Name.Substring(0, attr.AttributeType.Name.Length - 9 /*Attribute*/), BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static);
                            if (resName != null)
                            {
                                msg = resName.GetValue(null) as string;
                            }
                        }
                    }
                }
                catch
                {
                    msg = null;
                }
            }

            return msg;
        }

        protected void SetRegularExpressionAAValidation(string propertyName, string displayName, CustomAttributeData attr, StringBuilder jsonString, bool isFirstAttr,
            string regex, string defaultMsgFormat, string resourceNamespace = null, string resourceAssemblyName = null)
        {
            PrependComma(jsonString, isFirstAttr);

            jsonString.Append("'ng-pattern': \"/" + RegexConstants.GetRegularExpressionForJson(regex) + "/\"");

            if (!string.IsNullOrWhiteSpace(displayName))
            {
                var msg = GetErrorMessage(propertyName, attr, resourceNamespace, resourceAssemblyName);

                msg = string.Format(string.IsNullOrWhiteSpace(msg) ? defaultMsgFormat : msg, displayName);

                jsonString.Append(", 'ng-pattern-msg': \"" + msg + "\"");
            }
        }

        protected void SetMaxLengthAAValidation(string propertyName, string displayName, CustomAttributeData attr, StringBuilder jsonString, bool isFirstAttr,
            string length, string resourceNamespace = null, string resourceAssemblyName = null)
        {
            PrependComma(jsonString, isFirstAttr);

            jsonString.Append("'ng-maxlength': " + length);

            if (!string.IsNullOrWhiteSpace(displayName))
            {
                var msg = GetErrorMessage(propertyName, attr, resourceNamespace, resourceAssemblyName);
                
                msg = string.Format(string.IsNullOrWhiteSpace(msg) ? DataAnnotationConstants.DefaultMaxLengthErrorMsg : msg, displayName, length);


                jsonString.Append(", 'ng-maxlength-msg': \"" + msg + "\"");
            }
        }

        protected void SetMinLengthAAValidation(string propertyName, string displayName, CustomAttributeData attr, StringBuilder jsonString, bool isFirstAttr,
            string length, string resourceNamespace = null, string resourceAssemblyName = null)
        {
            PrependComma(jsonString, isFirstAttr);

            jsonString.Append("'ng-minlength': " + length);

            if (!string.IsNullOrWhiteSpace(displayName))
            {
                var msg = GetErrorMessage(propertyName, attr, resourceNamespace, resourceAssemblyName);

                msg = string.Format(string.IsNullOrWhiteSpace(msg) ? DataAnnotationConstants.DefaultMinLengthErrorMsg : msg, displayName, length);

                jsonString.Append(", 'ng-minlength-msg': \"" + msg + "\"");
            }
        }
    }
}

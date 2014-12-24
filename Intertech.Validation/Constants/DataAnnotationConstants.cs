using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intertech.Validation.Constants
{
    public class DataAnnotationConstants
    {
        public const string Display = "Display";
        public const string MinimumLength = "MinimumLength";
        public const string ErrorMessage = "ErrorMessage";
        public const string DefaultEmailErrorMsg = "{0} is an invalid email address.";
        public const string DefaultPhoneErrorMsg = "{0} is an invalid phone number.";
        public const string DefaultCreditCardErrorMsg = "{0} is an invalid credit card number.";
        public const string DefaultRegexErrorMsg = "{0} is invalid.";
        public const string DefaultUrlErrorMsg = "{0} is an invalid URL.";
        public const string DefaultRequiredErrorMsg = "{0} is required.";
        public const string DefaultMinLengthErrorMsg = "{0} cannot be less than {1} characters.";
        public const string DefaultMaxLengthErrorMsg = "{0} cannot be more than {1} characters.";
        public const string DefaultRangeErrorMsg = "{0} must be between {1} and {2}.";
    }
}

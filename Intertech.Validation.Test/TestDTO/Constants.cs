using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intertech.Validation.Test.TestDTO
{
    public class ErrorMessages
    {
        public const string MinLength = "Length minimum violated";
        public const string MaxLength = "Length maximum violated";
        public const string Required = "Length required, yo";
        public const string Email = "Length should be email";
        public const string Phone = "Length should be phone";
        public const string Url = "Length should be Url";
        public const string Regex = "Length Regex failed";
        public const string CreditCard = "Visa not a CreditCard";
        public const string VisaLength = "Visa has invalid length";
    }
}

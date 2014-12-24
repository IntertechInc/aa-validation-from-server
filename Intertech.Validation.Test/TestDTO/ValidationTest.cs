using Intertech.Validation.Constants;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intertech.Validation.Test.TestDTO
{
    public class ValidationTest
    {
        [Required]
        [MinLength(3)]
        public string Name { get; set; }

        [CreditCard]
        public string CreditCard { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        [MaxLength(40)]
        public string Street { get; set; }

        [Phone]
        public string Phone { get; set; }

        [Range(1, 100)]
        public int FavoriteNumber { get; set; }

        [RegularExpression(RegexConstants.Integer)]
        public string IntegerString { get; set; }

        [StringLength(30, MinimumLength = 2)]
        public string NickName { get; set; }

        [Url]
        public string Website { get; set; }

        [Required(ErrorMessage = ErrorMessages.Required)]
        [MinLength(5, ErrorMessage = ErrorMessages.MinLength)]
        [MaxLength(25, ErrorMessage = ErrorMessages.MaxLength)]
        public string Length { get; set; }

        [CreditCard(ErrorMessage = ErrorMessages.CreditCard)]
        [StringLength(30, MinimumLength = 12, ErrorMessage = ErrorMessages.VisaLength)]
        public string Visa { get; set; }

        [Url(ErrorMessage = ErrorMessages.Url)]
        public string Url { get; set; }

        [EmailAddress(ErrorMessage = ErrorMessages.Email)]
        public string Email2 { get; set; }

        [Phone(ErrorMessage = ErrorMessages.Phone)]
        public string Phone2 { get; set; }

        [RegularExpression(RegexConstants.Decimal, ErrorMessage = ErrorMessages.Regex)]
        public string DecimalString { get; set; }
    }
}

using Intertech.Validation.Constants;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AATestAPI.Models
{
    public class ValidationTest
    {
        [Required]
        public string Name { get; set; }

        [Phone]
        public string Phone { get; set; }

        [Url]
        public string Website { get; set; }

        [CreditCard]
        public string CreditCard { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        [MaxLength(5)]
        public string MaxLengthTest { get; set; }

        [MinLength(2)]
        public string MinLengthTest { get; set; }

        [Range(1, 5)]
        public int RangeTest { get; set; }

        [RegularExpression(RegexConstants.Integer, ErrorMessage = "Must be an integer")]
        public string IntegerString { get; set; }

        [StringLength(10, MinimumLength = 2)]
        public string StringLengthTest { get; set; }
    }
}
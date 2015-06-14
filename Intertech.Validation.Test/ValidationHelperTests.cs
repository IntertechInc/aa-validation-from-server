using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Text;
using Newtonsoft.Json.Linq;
using Intertech.Validation.Constants;
using Intertech.Validation.Test.TestDTO;
using System.IO;
using System.Collections.Generic;

namespace Intertech.Validation.Test
{
    [TestClass]
    public class ValidationHelperTests
    {
        private object _validations;
		private object _validationsCC;
        private object _emptyValidations;
        private GetValidationsParms _parms;

        [TestInitialize]
        public void Init()
        {
            _parms = new GetValidationsParms("TestDTO.ValidationTest", "model")
            {
                DtoAssemblyNames = new List<string> { "Intertech.Validation.Test" },
                ResourceAssemblyName = "Intertech.Validation.Test",
                ResourceNamespace = "Intertech.Validation.Test.TestDTO"
            };

            var vals = new StringBuilder("{ validations: { model: { ");
            vals.Append("Name: { \"ng-minlength\": 3, \"ng-minlength-msg\": \"" + string.Format(DataAnnotationConstants.DefaultMinLengthErrorMsg, "Name", "3") + "\", \"required\": true, \"required-msg\": \"" + TestResource.NameRequiredResource + "\" }");
            vals.Append(", CreditCard: { \"ng-pattern\": \"/" + RegexConstants.GetRegularExpressionForJson(RegexConstants.CreditCard) + "/\", \"ng-pattern-msg\": \"" + string.Format(DataAnnotationConstants.DefaultCreditCardErrorMsg, "CreditCard") + "\" }");
            vals.Append(", Email: { \"ng-pattern\": \"/" + RegexConstants.GetRegularExpressionForJson(RegexConstants.Email) + "/\", \"ng-pattern-msg\": \"" + string.Format(DataAnnotationConstants.DefaultEmailErrorMsg, "Email") + "\" }");
            vals.Append(", Email2: { \"ng-pattern\": \"/" + RegexConstants.GetRegularExpressionForJson(RegexConstants.Email) + "/\", \"ng-pattern-msg\": \"" + ErrorMessages.Email + "\" }");
            vals.Append(", Street: { \"ng-maxlength\": 40, \"ng-maxlength-msg\": \"" + string.Format(DataAnnotationConstants.DefaultMaxLengthErrorMsg, "Street", "40") + "\" }");
            vals.Append(", Phone: { \"ng-pattern\": \"/" + RegexConstants.GetRegularExpressionForJson(RegexConstants.Phone) + "/\", \"ng-pattern-msg\": \"" + string.Format(DataAnnotationConstants.DefaultPhoneErrorMsg, "Phone") + "\" }");
            vals.Append(", Phone2: { \"ng-pattern\": \"/" + RegexConstants.GetRegularExpressionForJson(RegexConstants.Phone) + "/\", \"ng-pattern-msg\": \"" + ErrorMessages.Phone + "\" }");
            vals.Append(", FavoriteNumber: { \"min\": 1, \"min-msg\": \"" + string.Format(DataAnnotationConstants.DefaultRangeErrorMsg, "FavoriteNumber", "1", "100") + "\", \"max\": 100, \"max-msg\": \"" + string.Format(DataAnnotationConstants.DefaultRangeErrorMsg, "FavoriteNumber", "1", "100") + "\" }");
            vals.Append(", IntegerString: { \"ng-pattern\": \"/" + RegexConstants.GetRegularExpressionForJson(RegexConstants.Integer) + "/\", \"ng-pattern-msg\": \"" + string.Format(DataAnnotationConstants.DefaultRegexErrorMsg, "IntegerString") + "\" }");
            vals.Append(", DecimalString: { \"ng-pattern\": \"/" + RegexConstants.GetRegularExpressionForJson(RegexConstants.Decimal) + "/\", \"ng-pattern-msg\": \"" + ErrorMessages.Regex + "\" }");
            vals.Append(", NickName: { \"ng-maxlength\": 30, \"ng-maxlength-msg\": \"" + string.Format(DataAnnotationConstants.DefaultMaxLengthErrorMsg, "NickName", "30") + "\", \"ng-minlength\": 2, \"ng-minlength-msg\": \"" + string.Format(DataAnnotationConstants.DefaultMinLengthErrorMsg, "NickName", "2") + "\" }");
            vals.Append(", Website: { \"ng-pattern\": \"/" + RegexConstants.GetRegularExpressionForJson(RegexConstants.Url) + "/\", \"ng-pattern-msg\": \"" + string.Format(DataAnnotationConstants.DefaultUrlErrorMsg, "Website") + "\" }");
            vals.Append(", Length: { \"ng-minlength\": 5, \"ng-minlength-msg\": \"" + ErrorMessages.MinLength + "\", \"ng-maxlength\": 25, \"ng-maxlength-msg\": \"" + ErrorMessages.MaxLength + "\", \"required\": true, \"required-msg\": \"" + ErrorMessages.Required + "\" }");
            vals.Append(", Visa: { \"ng-pattern\": \"/" + RegexConstants.GetRegularExpressionForJson(RegexConstants.CreditCard) + "/\", \"ng-pattern-msg\": \"" + ErrorMessages.CreditCard + "\", \"ng-maxlength\": 30, \"ng-maxlength-msg\": \"" + ErrorMessages.VisaLength + "\", \"ng-minlength\": 12, \"ng-minlength-msg\": \"" + ErrorMessages.VisaLength + "\" }");
            vals.Append(", Url: { \"ng-pattern\": \"/" + RegexConstants.GetRegularExpressionForJson(RegexConstants.Url) + "/\", \"ng-pattern-msg\": \"" + ErrorMessages.Url + "\" }");
            vals.Append(" }");
            vals.Append("} }");
            _validations = JObject.Parse(vals.ToString());


			var valsCC = new StringBuilder("{ validations: { model: { ");
			valsCC.Append("name: { \"ng-minlength\": 3, \"ng-minlength-msg\": \"" + string.Format(DataAnnotationConstants.DefaultMinLengthErrorMsg, "Name", "3") + "\", \"required\": true, \"required-msg\": \"" + string.Format(DataAnnotationConstants.DefaultRequiredErrorMsg, "Name") + "\" }");
			valsCC.Append(", creditCard: { \"ng-pattern\": \"/" + RegexConstants.GetRegularExpressionForJson(RegexConstants.CreditCard) + "/\", \"ng-pattern-msg\": \"" + string.Format(DataAnnotationConstants.DefaultCreditCardErrorMsg, "CreditCard") + "\" }");
			valsCC.Append(", email: { \"ng-pattern\": \"/" + RegexConstants.GetRegularExpressionForJson(RegexConstants.Email) + "/\", \"ng-pattern-msg\": \"" + string.Format(DataAnnotationConstants.DefaultEmailErrorMsg, "Email") + "\" }");
			valsCC.Append(", email2: { \"ng-pattern\": \"/" + RegexConstants.GetRegularExpressionForJson(RegexConstants.Email) + "/\", \"ng-pattern-msg\": \"" + ErrorMessages.Email + "\" }");
			valsCC.Append(", street: { \"ng-maxlength\": 40, \"ng-maxlength-msg\": \"" + string.Format(DataAnnotationConstants.DefaultMaxLengthErrorMsg, "Street", "40") + "\" }");
			valsCC.Append(", phone: { \"ng-pattern\": \"/" + RegexConstants.GetRegularExpressionForJson(RegexConstants.Phone) + "/\", \"ng-pattern-msg\": \"" + string.Format(DataAnnotationConstants.DefaultPhoneErrorMsg, "Phone") + "\" }");
			valsCC.Append(", phone2: { \"ng-pattern\": \"/" + RegexConstants.GetRegularExpressionForJson(RegexConstants.Phone) + "/\", \"ng-pattern-msg\": \"" + ErrorMessages.Phone + "\" }");
			valsCC.Append(", favoriteNumber: { \"min\": 1, \"min-msg\": \"" + string.Format(DataAnnotationConstants.DefaultRangeErrorMsg, "FavoriteNumber", "1", "100") + "\", \"max\": 100, \"max-msg\": \"" + string.Format(DataAnnotationConstants.DefaultRangeErrorMsg, "FavoriteNumber", "1", "100") + "\" }");
			valsCC.Append(", integerString: { \"ng-pattern\": \"/" + RegexConstants.GetRegularExpressionForJson(RegexConstants.Integer) + "/\", \"ng-pattern-msg\": \"" + string.Format(DataAnnotationConstants.DefaultRegexErrorMsg, "IntegerString") + "\" }");
			valsCC.Append(", decimalString: { \"ng-pattern\": \"/" + RegexConstants.GetRegularExpressionForJson(RegexConstants.Decimal) + "/\", \"ng-pattern-msg\": \"" + ErrorMessages.Regex + "\" }");
			valsCC.Append(", nickName: { \"ng-maxlength\": 30, \"ng-maxlength-msg\": \"" + string.Format(DataAnnotationConstants.DefaultMaxLengthErrorMsg, "NickName", "30") + "\", \"ng-minlength\": 2, \"ng-minlength-msg\": \"" + string.Format(DataAnnotationConstants.DefaultMinLengthErrorMsg, "NickName", "2") + "\" }");
			valsCC.Append(", website: { \"ng-pattern\": \"/" + RegexConstants.GetRegularExpressionForJson(RegexConstants.Url) + "/\", \"ng-pattern-msg\": \"" + string.Format(DataAnnotationConstants.DefaultUrlErrorMsg, "Website") + "\" }");
			valsCC.Append(", length: { \"ng-minlength\": 5, \"ng-minlength-msg\": \"" + ErrorMessages.MinLength + "\", \"ng-maxlength\": 25, \"ng-maxlength-msg\": \"" + ErrorMessages.MaxLength + "\", \"required\": true, \"required-msg\": \"" + ErrorMessages.Required + "\" }");
			valsCC.Append(", visa: { \"ng-pattern\": \"/" + RegexConstants.GetRegularExpressionForJson(RegexConstants.CreditCard) + "/\", \"ng-pattern-msg\": \"" + ErrorMessages.CreditCard + "\", \"ng-maxlength\": 30, \"ng-maxlength-msg\": \"" + ErrorMessages.VisaLength + "\", \"ng-minlength\": 12, \"ng-minlength-msg\": \"" + ErrorMessages.VisaLength + "\" }");
			valsCC.Append(", url: { \"ng-pattern\": \"/" + RegexConstants.GetRegularExpressionForJson(RegexConstants.Url) + "/\", \"ng-pattern-msg\": \"" + ErrorMessages.Url + "\" }");
			valsCC.Append(" }");
			valsCC.Append("} }");
			_validationsCC = JObject.Parse(valsCC.ToString());



            _emptyValidations = JObject.Parse("{ validations: { model: { } } }");
        }

        private void AssertJsonEqual(object expected, object actual)
        {
            Assert.IsTrue(JObject.DeepEquals(expected as JObject, actual as JObject));
        }

        [TestMethod]
        public void ValidationHelper_Constructor_Test()
        {
            // Assemble

            // Act
            var valHelper = new ValidationHelper();

            // Assert
            var converters = valHelper.Converters;
            Assert.IsNotNull(converters);
            Assert.IsTrue(converters.Count == 10);
        }

        [TestMethod]
        public void ValidationHelper_GetValidations_Success_ParmsObject_Test()
        {
            // Assemble
            var valHelper = new ValidationHelper();

            // Act
            var vals = valHelper.GetValidations(_parms);

            // Assert
            Assert.IsNotNull(vals);
            AssertJsonEqual(_validations, vals);
        }

        [TestMethod]
		public void ValidationHelper_GetValidationsCC_Success_Test()
		{
			// Assemble
			var valHelper = new ValidationHelper();

			// Act
			var vals = valHelper.GetValidations("TestDTO.ValidationTest", "model", null, true, "Intertech.Validation.Test");

			// Assert
			Assert.IsNotNull(vals);
			AssertJsonEqual(_validationsCC, vals);
		}
		
		[TestMethod]
        public void ValidationHelper_GetValidations_Empty_Test()
        {
            // Assemble
            var valHelper = new ValidationHelper();

            // Act
            var vals = valHelper.GetValidations("TestDTO.NoValidations", "model", null, false, "Intertech.Validation.Test");

            // Assert
            Assert.IsNotNull(vals);
            AssertJsonEqual(_emptyValidations, vals);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception), "DTO 'blah' not found.")]
        public void ValidationHelper_GetValidations_DTONotFound_Test()
        {
            // Assemble
            var valHelper = new ValidationHelper();

            // Act
            var vals = valHelper.GetValidations("blah", "model", null, false, "Intertech.Validation.Test");

            // Assert
            Assert.IsNull(vals);
        }

        [TestMethod]
        [ExpectedException(typeof(FileNotFoundException))]
        public void ValidationHelper_GetValidations_AssemblyNotFound_Test()
        {
            // Assemble
            var valHelper = new ValidationHelper();

            // Act
            var vals = valHelper.GetValidations("TestDTO.ValidationTest", "model", null, false, "Blah.Validation.Test");

            // Assert
            Assert.IsNull(vals);
        }
    }
}

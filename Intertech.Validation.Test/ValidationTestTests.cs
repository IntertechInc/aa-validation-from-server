using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace Intertech.Validation.Test.TestDTO
{
    [TestClass]
    public class ValidationTestTests
    {
        private ValidationTest _dto;

        [TestInitialize]
        public void Init()
        {
            _dto = new ValidationTest
            {
                Name = "Rich",
                Length = "12345",
                FavoriteNumber = 7
            };
        }

        [TestMethod]
        public void ValidationTest_Name_RequiredFail_Test()
        {
            // Assemble
            _dto.Name = null;
            var results = new List<ValidationResult>();

            // Act
            var actual = Validator.TryValidateObject(_dto, new ValidationContext(_dto), results, true);

            // Assert
            Assert.IsFalse(actual);
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual<string>(TestResource.NameRequiredResource, results[0].ErrorMessage);
        }
    }
}

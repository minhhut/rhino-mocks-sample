using Core.Services;
using NUnit.Framework;

namespace Core.Tests
{
    [TestFixture]
    public class EmailValidationTests
    {
        [Test]
        public void IsValid_EmailNull_ReturnsFalse()
        {
            var emailValidation = new EmailValidation();

            var r = emailValidation.isValid(null);

            Assert.AreEqual(r, false);
        }

        [Test]
        public void IsValid_ValidEmail_ReturnsTrue()
        {
            var emailValidation = new EmailValidation();

            var r = emailValidation.isValid("email@domain.com");

            Assert.AreEqual(r, true);
        }

    }
}

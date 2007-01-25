using System;
using Mix.Core.Attributes;
using Mix.Core.Exceptions;
using NUnit.Framework;

namespace Mix.Core.Tests
{
    [TestFixture]
    public class ValidatorFixture
    {
        #region Private test classes

        private class ValidClass
        {
        }

        private class NotValidClass
        {
            [Required]
            public string Text
            {
                get { return String.Empty; }
            }
        }

        #endregion

        [Test]
        public void ClassValidClass()
        {
            ValidClass obj = new ValidClass();
            Validator validator = new Validator(obj);
            validator.Validate();
        }

        [Test]
        public void ClassNotValidClass()
        {
            NotValidClass obj = new NotValidClass();
            Validator validator = new Validator(obj);
            try
            {
                validator.Validate();
            }
            catch (RequiredValueMissingException e)
            {
                Assert.AreEqual("Text", e.Property);
                return;
            }
            Assert.Fail("A 'RequiredValueMissingException' should have been thrown.");
        }
    }
}

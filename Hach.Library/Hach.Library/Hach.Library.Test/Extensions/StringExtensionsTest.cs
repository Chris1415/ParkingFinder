using Hach.Library.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Hach.Library.Test.Extensions
{
    [TestClass]
    public class StringExtensionsTest
    {
        [TestMethod]
        public void IsNullOrEmptySuccessNull()
        {
            string input = null;

            bool isNullOrEmpty = input.IsNullOrEmpty();

            Assert.AreEqual(true, isNullOrEmpty);
        }

        [TestMethod]
        public void IsNullOrEmptySuccessEmpty()
        {
            string input = string.Empty;

            bool isNullOrEmpty = input.IsNullOrEmpty();

            Assert.AreEqual(true, isNullOrEmpty);
        }

        [TestMethod]
        public void IsNullOrEmptyFailInputNotEmpty()
        {
            const string input = "This is a input";

            bool isNullOrEmpty = input.IsNullOrEmpty();

            Assert.AreEqual(false, isNullOrEmpty);
        }

        [TestMethod]
        public void IsNullOrEmptyFailInputNotEmptySingleCharacter()
        {
            const string input = "T";

            bool isNullOrEmpty = input.IsNullOrEmpty();

            Assert.AreEqual(false, isNullOrEmpty);
        }

        [TestMethod]
        public void IsNullOrEmptyFailInputNotEmptyEmptySpace()
        {
            const string input = " ";

            bool isNullOrEmpty = input.IsNullOrEmpty();

            Assert.AreEqual(false, isNullOrEmpty);
        }
    }
}

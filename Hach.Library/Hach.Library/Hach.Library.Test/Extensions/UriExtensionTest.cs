using System;
using Hach.Library.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Hach.Library.Test.Extensions
{
    [TestClass]
    public class UriExtensionTest
    {
        private const string BaseUrl = "http://www.testurl.dev";

        [TestMethod]
        public void AppendParameterSuccessEmptyQuery()
        {
            Uri url = new Uri(BaseUrl);
            const string key = "Test";
            const string value = "InsertedValue";

            string appendedUrl = url.AppendParameter(key, value);
           
            Assert.AreEqual($"{BaseUrl}?{key}={value}",appendedUrl);
        }

        [TestMethod]
        public void AppendParameterSuccessFilledQueryOtherParameter()
        {
            string extendedBaseUrl = $"{BaseUrl}?ExtendedParam=Value";
            Uri url = new Uri(extendedBaseUrl);
            const string key = "Test";
            const string value = "InsertedValue";

            string appendedUrl = url.AppendParameter(key, value);

            Assert.AreEqual($"{extendedBaseUrl}&{key}={value}", appendedUrl);
        }

        [TestMethod]
        public void AppendParameterSuccessBaseUrlWithSubPage()
        {
            string extendedBaseUrl = $"{BaseUrl}/SubPage";
            Uri url = new Uri(extendedBaseUrl);
            const string key = "Test";
            const string value = "InsertedValue";

            string appendedUrl = url.AppendParameter(key, value);

            Assert.AreEqual($"{extendedBaseUrl}?{key}={value}", appendedUrl);
        }

        [TestMethod]
        public void AppendParameterSuccessBaseUrlWithSubPageAndQuery()
        {
            string extendedBaseUrl = $"{BaseUrl}/SubPage?ExtendedParam=Value";
            Uri url = new Uri(extendedBaseUrl);
            const string key = "Test";
            const string value = "InsertedValue";

            string appendedUrl = url.AppendParameter(key, value);

            Assert.AreEqual($"{extendedBaseUrl}&{key}={value}", appendedUrl);
        }

        [TestMethod]
        public void AppendParameterSuccessBaseUrlWithExistingKey()
        {
            const string key = "Test";
            const string value = "InsertedValue";
            string extendedBaseUrl = $"{BaseUrl}?{key}=PreValue";
            Uri url = new Uri(extendedBaseUrl);

            string appendedUrl = url.AppendParameter(key, value);

            Assert.AreEqual($"{BaseUrl}?{key}={value}", appendedUrl);
        }


        [TestMethod]
        public void AppendParameterEmptyKey()
        {
            string key = string.Empty;
            const string value = "InsertedValue";
            string extendedBaseUrl = $"{BaseUrl}";
            Uri url = new Uri(extendedBaseUrl);

            string appendedUrl = url.AppendParameter(key, value);

            Assert.AreEqual(url.AbsoluteUri, appendedUrl);
        }

        [TestMethod]
        public void AppendParameterSubPageEmptyKey()
        {
            string key = string.Empty;
            const string value = "InsertedValue";
            string extendedBaseUrl = $"{BaseUrl}/SubPage";
            Uri url = new Uri(extendedBaseUrl);

            string appendedUrl = url.AppendParameter(key, value);

            Assert.AreEqual(url.AbsoluteUri, appendedUrl);
        }

        [TestMethod]
        public void AppendParameterEmptyValue()
        {
            const string key = "Test";
            string value = string.Empty;
            string extendedBaseUrl = $"{BaseUrl}";
            Uri url = new Uri(extendedBaseUrl);

            string appendedUrl = url.AppendParameter(key, value);

            Assert.AreEqual(url.AbsoluteUri, appendedUrl);
        }

        [TestMethod]
        public void AppendParameterNullKey()
        {
            const string key = null;
            string value = string.Empty;
            string extendedBaseUrl = $"{BaseUrl}";
            Uri url = new Uri(extendedBaseUrl);

            string appendedUrl = url.AppendParameter(key, value);

            Assert.AreEqual(url.AbsoluteUri, appendedUrl);
        }

        [TestMethod]
        public void AppendParameterNullValue()
        {
            const string key = "Test";
            string value = null;
            string extendedBaseUrl = $"{BaseUrl}";
            Uri url = new Uri(extendedBaseUrl);

            string appendedUrl = url.AppendParameter(key, value);

            Assert.AreEqual(url.AbsoluteUri, appendedUrl);
        }
    }
}

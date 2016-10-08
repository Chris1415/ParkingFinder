using Hach.Library.Services.Mapping.Json.Implementations;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Hach.Library.Test.Services.Mapping.Json.Implementations
{
    [TestClass]
    public class JsonMapperServiceTest
    {
        /// <summary>
        /// Testing Helper Model
        /// </summary>
        public class TestModel
        {
            public string Key { get; set; }

            public string Value { get; set; }
        }

        [TestMethod]
        public void MapStringToClassSuccessSingleProperty()
        {
            JsonMapperService jsonMapperService = new JsonMapperService();
            const string inputString = "{\"Key\":\"TestKey\"}";

            TestModel model = jsonMapperService.MapStringToClass<TestModel>(inputString);

            Assert.IsTrue(model.Key.Equals("TestKey"));
        }

        [TestMethod]
        public void MapStringToClassSuccessMultipleProperties()
        {
            JsonMapperService jsonMapperService = new JsonMapperService();
            const string inputString = "{\"Key\":\"TestKey\", \"Value\":\"TestValue\"}";

            TestModel model = jsonMapperService.MapStringToClass<TestModel>(inputString);

            Assert.IsTrue(model.Key.Equals("TestKey") && model.Value.Equals("TestValue"));
        }
    }
}

using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MongoDB.Driver;
using System.Threading.Tasks;

namespace PepperbotTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public async Task TestMongoDBConnect()
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();
            var client = new MongoClient(config.GetValue<string>("mongoDB"));
            var db = client.GetDatabase("site-links");
            var col = db.GetCollection<object>("links");
            Assert.AreEqual(0, (await col.EstimatedDocumentCountAsync()));
            //Assert.IsTrue((await col.EstimatedDocumentCountAsync()) > 1);
        }
    }
}
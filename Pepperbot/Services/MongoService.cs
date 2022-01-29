using MongoDB.Driver;
using Pepperbot.Data;

namespace Pepperbot.Services
{
    public class MongoService
    {
        private readonly MongoClient client;

        public MongoService(IConfiguration configuration)
        {
            var connStr = configuration["mongoDB"] ?? throw new ArgumentNullException($"MongoDB connection info missing");
            
            client = new MongoClient(connStr);
        }

        public async Task<long> GetLinkCount()
        {
            var db = client.GetDatabase("site-links");
            var col = db.GetCollection<RedirectLink>("links");
            return await col.EstimatedDocumentCountAsync();
        }

        public async Task<List<RedirectLink>> GetDoc(string name)
        {
            var db = client.GetDatabase("site-links");
            var col = db.GetCollection<RedirectLink>("links");
            return (await col.FindAsync(l => l.Name == name)).ToList();
        }
    }
}

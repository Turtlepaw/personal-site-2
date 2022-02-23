using Microsoft.AspNetCore.Mvc;

namespace PepperbotAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PepperbotDataController : ControllerBase
    {

        private readonly ILogger<PepperbotDataController> _logger;

        private readonly MongoClient client;

        public PepperbotDataController(IConfiguration configuration)
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


        public PepperbotDataController(ILogger<PepperbotDataController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }
}
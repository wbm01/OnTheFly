namespace OnTheFly.FlightsService.config
{
    public class MongoDBConfig : IMongoDBConfig
    {
        public string FlightCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}

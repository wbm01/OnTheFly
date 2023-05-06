namespace OnTheFly.AirportServices.Config
{
    public class MongoDBConfig:IMongoDBConfig
    {
        public string AirportCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}

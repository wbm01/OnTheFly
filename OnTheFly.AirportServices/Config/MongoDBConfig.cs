namespace OnTheFly.AirportServices.Config
{
    public class MongoDBConfig:IMongoDBConfig
    {
        public string AirporttCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}

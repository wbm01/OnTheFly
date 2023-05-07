namespace OnTheFly.AirCraftServices.config
{
    public class MongoDBConfig : IMongoDBConfig
    {
        public string AirCraftCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}

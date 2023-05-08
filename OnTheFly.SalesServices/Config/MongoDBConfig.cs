namespace OnTheFly.SalesServices.Config
{
    public class MongoDBConfig : IMongoDBConfig
    {
        public string SalesCollectionName { get ; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}

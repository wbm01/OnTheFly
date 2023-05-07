namespace OnTheFly.AirCraftServices.config
{
    public interface IMongoDBConfig
    {
        string AirCraftCollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}

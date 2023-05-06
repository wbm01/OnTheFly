namespace OnTheFly.AirportServices.Config
{
    public interface IMongoDBConfig
    {
        string AirportCollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}

namespace OnTheFly.FlightsService.config
{
    public interface IMongoDBConfig
    {
        string FlightCollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}

namespace OnTheFly.AirportServices.Config
{
    public interface IMongoDBConfig
    {
        string AirporttCollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}

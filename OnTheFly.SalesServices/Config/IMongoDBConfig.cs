namespace OnTheFly.SalesServices.Config
{
    public interface IMongoDBConfig
    {
        string SalesCollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}

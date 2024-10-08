namespace PrecoDaBanana.API.Models
{
    public class MongoDBSettings
    {
        public string ConnectionString { get; set; } = null!;
        public string DatabaseName { get; set; } = null!;
        public string BananaCollectionName { get; set; } = null!;
        public object Value { get; internal set; }
    }
}

namespace LottoApi.Models
{
    public class MongoDBSettings
    {
        public string ConnectionString { get; set; } = null!;
        public string DatabaseName { get; set; } = null!;
        public List<string> CollectionNames { get; set; } = new List<string>();
    }
}

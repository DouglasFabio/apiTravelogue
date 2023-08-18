namespace ApiTravelogue.Models
{
    public class ViagemDatabaseSettings
    {
        public string ConnectionString { get; set; } = null!;
        public string DatabaseName { get; set; } = null!;
        public string ViagemCollectionName { get; set; } = null!;

        public string EntradaCollectionName { get; set; } = null!;
    }
}

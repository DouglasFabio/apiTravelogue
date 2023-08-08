using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ApiTravelogue.Models
{
    public class Viagem
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonElement("name")]
        public string Name { get; set; } = null!;

        [BsonElement("dateTravel")]
        public DateOnly DateTravel { get; set; }
    }
}

using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ApiTravelogue.Models
{
    public class Entrada
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonElement("visitedLocal")]
        public string VisitedLocal { get; set; } = null!;

        [BsonElement("dateVisit")]
        public DateOnly DateVisit { get; set; }

        [BsonElement("description")]
        public string Description { get; set; } = null!;

        [BsonElement("midiaPath")]
        public string MidiaPath { get; set; } = null!;


        [BsonElement("codTravel")]
        public string CodTravel { get; set; }
    }
}

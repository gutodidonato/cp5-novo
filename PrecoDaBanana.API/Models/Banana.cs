using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace PrecoDaBanana.API.Models
{
    public class Banana
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("Nome")]
        public string Nome { get; set; }

        [BsonElement("Preco")]
        public decimal Preco { get; set; }

        [BsonElement("Origem")]
        public string Origem { get; set; }
    }
}

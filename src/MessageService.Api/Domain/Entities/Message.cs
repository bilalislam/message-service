using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MessageService.Api
{
    public class Message
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public string From { get; set; }
        public string To { get; set; }
        public string Msg { get; set; }
        public string EventOn { get; set; }
    }
}
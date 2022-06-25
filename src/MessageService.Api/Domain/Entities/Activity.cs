using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MessageService.Api
{
    public class Activity
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public string User { get; set; }

        public string Event { get; set; }
        public string EventOn { get; set; }
    }
}
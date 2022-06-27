using MessageService.Api.Domain.Constants;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MessageService.Api
{
    public class Activity
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; private set; }

        public string Email { get; private set; }

        public string Event { get; private set; }
        public DateTime EventOn { get; private set; }

        /// <summary>
        /// impedance mismatch !
        /// </summary>
        /// <param name="id"></param>
        /// <param name="email"></param>
        /// <param name="event"></param>
        /// <param name="eventOn"></param>
        public Activity(string id, string email, string @event, DateTime eventOn)
        {
            Id = id;
            Email = email;
            Event = @event;
            EventOn = eventOn;
        }

        private Activity(string id, string email, string @event)
        {
            Id = id;
            Email = email;
            Event = @event;
        }

        public static Activity Load(ActivityCommand command)
        {
            Guard.That<ValidationException>(string.IsNullOrEmpty(command.Id), nameof(DomainErrorCodes.EDService1010),
                DomainErrorCodes.EDService1010,
                DomainErrorCodes.EDService1010);

            Guard.That<ValidationException>(string.IsNullOrEmpty(command.Email), nameof(DomainErrorCodes.EDService1011),
                DomainErrorCodes.EDService1011,
                DomainErrorCodes.EDService1011);

            Guard.That<ValidationException>(string.IsNullOrEmpty(command.Event), nameof(DomainErrorCodes.EDService1012),
                DomainErrorCodes.EDService1012,
                DomainErrorCodes.EDService1012);

            return new Activity(command.Id, command.Email, command.Event)
                .SetEventDate();
        }

        private Activity SetEventDate()
        {
            EventOn = DateTime.Now;
            return this;
        }
    }
}
using MessageService.Api.Domain.Constants;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MessageService.Api
{
    public class Message
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; private set; }
        public string From { get; private set; }
        public string To { get; private set; }
        public string Msg { get; private set; }
        public DateTime EventOn { get; private set; }

        /// <summary>
        /// impedance mismatch !
        /// </summary>
        /// <param name="id"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <param name="msg"></param>
        /// <param name="eventOn"></param>
        public Message(string id, string from, string to, string msg, DateTime eventOn)
        {
            Id = id;
            From = from;
            To = to;
            Msg = msg;
            EventOn = eventOn;
        }

        public static Message SetTo(string to)
        {
            return new Message(to);
        }

        private Message(string to)
        {
            To = to;
        }

        private Message(string from, string to, string msg)
        {
            Id = ObjectId.GenerateNewId().ToString();
            From = from;
            To = to;
            Msg = msg;
            EventOn = DateTime.Now;
        }

        public static Message Load(SendCommand command)
        {
            Guard.That<ValidationException>(command == null, nameof(DomainErrorCodes.EDService1002),
                DomainErrorCodes.EDService1002,
                DomainErrorCodes.EDService1002);

            Guard.That<ValidationException>(string.IsNullOrEmpty(command.Sender),
                nameof(DomainErrorCodes.EDService1004),
                DomainErrorCodes.EDService1004,
                DomainErrorCodes.EDService1004);

            Guard.That<ValidationException>(string.IsNullOrEmpty(command.Receiver),
                nameof(DomainErrorCodes.EDService1004),
                DomainErrorCodes.EDService1004,
                DomainErrorCodes.EDService1004);

            Guard.That<ValidationException>(command.Sender == command.Receiver,
                nameof(DomainErrorCodes.EDService1007),
                DomainErrorCodes.EDService1007,
                DomainErrorCodes.EDService1007);

            Guard.That<ValidationException>(string.IsNullOrEmpty(command.ReceiverEmail),
                nameof(DomainErrorCodes.EDService1003),
                DomainErrorCodes.EDService1003,
                DomainErrorCodes.EDService1003);

            Guard.That<ValidationException>(string.IsNullOrEmpty(command.Message),
                nameof(DomainErrorCodes.EDService1003),
                DomainErrorCodes.EDService1003,
                DomainErrorCodes.EDService1003);


            return new Message(command.Sender, command.Receiver, command.Message);
        }
    }
}
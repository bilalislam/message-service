using MessageService.Api.Domain.Constants;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MessageService.Api
{
    public class BlockUser
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; private set; }

        public string From { get; private set; }
        public string To { get; private set; }
        public DateTime EventOn { get; private set; }

        /// <summary>
        /// impedance mismatch antipattern !!
        /// </summary>
        /// <param name="id"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <param name="eventOn"></param>
        public BlockUser(string id, string from, string to, DateTime eventOn)
        {
            Id = id;
            From = from;
            To = to;
            EventOn = eventOn;
        }

        private BlockUser(string from, string to)
        {
            Id = ObjectId.GenerateNewId().ToString();
            From = from;
            To = to;
            EventOn = DateTime.Now;
        }

        public static BlockUser Load(BlockUserCommand command)
        {
            Guard.That<ValidationException>(command == null, nameof(DomainErrorCodes.EDService1002),
                DomainErrorCodes.EDService1002,
                DomainErrorCodes.EDService1002);

            Guard.That<ValidationException>(string.IsNullOrEmpty(command.CurrentUser),
                nameof(DomainErrorCodes.EDService1004),
                DomainErrorCodes.EDService1004,
                DomainErrorCodes.EDService1004);

            Guard.That<ValidationException>(string.IsNullOrEmpty(command.BlockedUser),
                nameof(DomainErrorCodes.EDService1004),
                DomainErrorCodes.EDService1004,
                DomainErrorCodes.EDService1004);

            Guard.That<ValidationException>(command.CurrentUser == command.BlockedUser,
                nameof(DomainErrorCodes.EDService1007),
                DomainErrorCodes.EDService1007,
                DomainErrorCodes.EDService1007);


            return new BlockUser(command.CurrentUser, command.BlockedUser);
        }
    }
}
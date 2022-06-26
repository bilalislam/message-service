using MessageService.Api.Domain.Constants;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MessageService.Api
{
    public class User
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId Id { get; private set; }

        public string Name { get; private set; }
        public string Surname { get; private set; }
        public string Email { get; private set; }
        public string Password { get; private set; }
        public Token Token { get; private set; }

        private User(string email, string name, string surname, string password)
        {
            Email = email;
            Name = name;
            Surname = surname;
            Password = password;
        }

        public static User Load(SignUpCommand command)
        {
            Guard.That<ValidationException>(command == null, nameof(DomainErrorCodes.EDService1002),
                DomainErrorCodes.EDService1002, DomainErrorCodes.EDService1002);

            Guard.That<ValidationException>(string.IsNullOrEmpty(command.Email), nameof(DomainErrorCodes.EDService1003),
                DomainErrorCodes.EDService1003,
                DomainErrorCodes.EDService1003);

            Guard.That<ValidationException>(string.IsNullOrEmpty(command.Name), nameof(DomainErrorCodes.EDService1004),
                DomainErrorCodes.EDService1004,
                DomainErrorCodes.EDService1004);

            Guard.That<ValidationException>(string.IsNullOrEmpty(command.Password),
                nameof(DomainErrorCodes.EDService1005), DomainErrorCodes.EDService1005,
                DomainErrorCodes.EDService1005);

            Guard.That<ValidationException>(string.IsNullOrEmpty(command.Surname),
                nameof(DomainErrorCodes.EDService1006),
                DomainErrorCodes.EDService1006,
                DomainErrorCodes.EDService1006);

            return new User(command.Email, command.Name, command.Surname, command.Password);
        }

        public User CreateToken(ITokenProxy tokenProxy)
        {
            Token = tokenProxy.CreateAccessToken(this);
            return this;
        }
    }
}
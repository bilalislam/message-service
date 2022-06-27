using Bogus;
using Bogus.DataSets;
using MessageService.Api;

namespace MessageService.UnitTests.Helper;

public class FakeDataGenerator
{
    private static Faker _faker = new Faker();

    public static SignUpCommand CreateSignUpCommand()
    {
        return new SignUpCommand()
        {
            Email = _faker.Random.String(),
            Name = _faker.Random.String(),
            Password = _faker.Random.String(),
            Surname = _faker.Random.String()
        };
    }

    public static Token CreateToken()
    {
        return new Token()
        {
            AccessToken = _faker.Random.String(),
            Expiration = DateTime.Now,
            RefreshToken = _faker.Random.String(),
            RefreshTokenExpiration = DateTime.Now,
        };
    }

    public static SendCommand CreateSendCommand()
    {
        return new SendCommand()
        {
            Message = _faker.Random.String(),
            Receiver = _faker.Random.String(),
            Sender = _faker.Random.String(),
            ReceiverEmail = _faker.Random.String()
        };
    }

    public static BlockUserCommand CreateBlockUserCommand()
    {
        return new BlockUserCommand()
        {
            BlockedUser = _faker.Random.String(),
            CurrentUser = _faker.Random.String(),
            BlockedUserEmail = _faker.Random.String()
        };
    }

    public static ActivityCommand CreateActivityCommand()
    {
        return new ActivityCommand()
        {
            Id = _faker.Random.String(),
            Email = _faker.Random.String(),
            Event = _faker.Random.String(),
            EventOn = DateTime.Now
        };
    }
}
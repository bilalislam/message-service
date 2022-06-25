using MessageService.Api;

public interface ITokenProxy
{
    Token CreateAccessToken(User user);
}
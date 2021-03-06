using System.Diagnostics.CodeAnalysis;

namespace MessageService.Api.Domain.Constants;

[ExcludeFromCodeCoverage]
public static class DomainErrorCodes
{
    public const string EDService1001 = "This email already exists";
    public const string EDService1002 = "User model could not be empty";
    public const string EDService1003 = "User email could not be empty";
    public const string EDService1004 = "User name could not be empty";
    public const string EDService1005 = "User password could not be empty";
    public const string EDService1006 = "User surname could not be empty";
    public const string EDService1007 = "From and to could not be same";
    public const string EDService1008 = "User does not exists";
    public const string EDService1009 = "Message could not be empty";
    public const string EDService1010 = "Activity id could not be empty";
    public const string EDService1011 = "Activity email could not be empty";
    public const string EDService1012 = "Activity event could not be empty";
}
using FluentAssertions;
using MessageService.Api;
using MessageService.Api.Domain.Constants;
using MessageService.UnitTests.Helper;
using NUnit.Framework;

namespace MessageService.UnitTests;

[TestFixture]
public class BlockUserTests
{
    [Test]
    public void Load_ShouldThrowException_WhenDomainDtoIsNull()
    {
        var exception = Assert.Throws<ValidationException>(() => BlockUser.Load(null));
        exception.Code.Should().Be(nameof(DomainErrorCodes.EDService1002));
    }

    [Test]
    public void Load_ShouldThrowException_WhenDomainDtoIsCurrentUserNull()
    {
        var blockUserCommand = FakeDataGenerator.CreateBlockUserCommand();
        blockUserCommand.CurrentUser = null;
        var exception = Assert.Throws<ValidationException>(() => BlockUser.Load(blockUserCommand));
        exception.Code.Should().Be(nameof(DomainErrorCodes.EDService1004));
    }

    [Test]
    public void Load_ShouldThrowException_WhenDomainDtoIsBlockedUserNull()
    {
        var blockUserCommand = FakeDataGenerator.CreateBlockUserCommand();
        blockUserCommand.BlockedUser = null;
        var exception = Assert.Throws<ValidationException>(() => BlockUser.Load(blockUserCommand));
        exception.Code.Should().Be(nameof(DomainErrorCodes.EDService1004));
    }

    [Test]
    public void Load_ShouldThrowException_WhenDomainDtoIsBlockedUserAndCurrentUserSame()
    {
        var blockUserCommand = FakeDataGenerator.CreateBlockUserCommand();
        blockUserCommand.BlockedUser = blockUserCommand.CurrentUser;
        var exception = Assert.Throws<ValidationException>(() => BlockUser.Load(blockUserCommand));
        exception.Code.Should().Be(nameof(DomainErrorCodes.EDService1007));
    }

    [Test]
    public void Load_ShouldSuccess_WhenIsDomainValid()
    {
        var blockUserCommand = FakeDataGenerator.CreateBlockUserCommand();
        var blockUser = BlockUser.Load(blockUserCommand);
        blockUser.Should().NotBeNull();
    }
}
using FluentAssertions;
using MessageService.Api;
using MessageService.Api.Domain.Constants;
using MessageService.UnitTests.Helper;
using NUnit.Framework;

namespace MessageService.UnitTests;

[TestFixture]
public class MessageTests
{
    [Test]
    public void Load_ShouldThrowException_WhenDomainDtoIsNull()
    {
        var exception = Assert.Throws<ValidationException>(() => Message.Load(null));
        exception.Code.Should().Be(nameof(DomainErrorCodes.EDService1002));
    }

    [Test]
    public void Load_ShouldThrowException_WhenDomainDtoIsSenderNull()
    {
        var fakeSendCommand = FakeDataGenerator.CreateSendCommand();
        fakeSendCommand.Sender = null;
        var exception = Assert.Throws<ValidationException>(() => Message.Load(fakeSendCommand));
        exception.Code.Should().Be(nameof(DomainErrorCodes.EDService1004));
    }

    [Test]
    public void Load_ShouldThrowException_WhenDomainDtoIsReceiverNull()
    {
        var fakeSendCommand = FakeDataGenerator.CreateSendCommand();
        fakeSendCommand.Receiver = null;
        var exception = Assert.Throws<ValidationException>(() => Message.Load(fakeSendCommand));
        exception.Code.Should().Be(nameof(DomainErrorCodes.EDService1004));
    }

    [Test]
    public void Load_ShouldThrowException_WhenDomainDtoIsReceiverAndSenderSame()
    {
        var fakeSendCommand = FakeDataGenerator.CreateSendCommand();
        fakeSendCommand.Receiver = fakeSendCommand.Sender;
        var exception = Assert.Throws<ValidationException>(() => Message.Load(fakeSendCommand));
        exception.Code.Should().Be(nameof(DomainErrorCodes.EDService1007));
    }

    [Test]
    public void Load_ShouldThrowException_WhenDomainDtoIsReceiverEmail()
    {
        var fakeSendCommand = FakeDataGenerator.CreateSendCommand();
        fakeSendCommand.ReceiverEmail = null;
        var exception = Assert.Throws<ValidationException>(() => Message.Load(fakeSendCommand));
        exception.Code.Should().Be(nameof(DomainErrorCodes.EDService1003));
    }

    [Test]
    public void Load_ShouldThrowException_WhenDomainDtoIsMessage()
    {
        var fakeSendCommand = FakeDataGenerator.CreateSendCommand();
        fakeSendCommand.Message = null;
        var exception = Assert.Throws<ValidationException>(() => Message.Load(fakeSendCommand));
        exception.Code.Should().Be(nameof(DomainErrorCodes.EDService1003));
    }
    
    [Test]
    public void Load_ShouldSuccess_WhenIsDomainValid()
    {
        var fakeSendCommand = FakeDataGenerator.CreateSendCommand();
        var message = Message.Load(fakeSendCommand);
        message.Should().NotBeNull();
    }
    
    [Test]
    public void Load_ShouldSuccess_WhenIsCallSetTo()
    {
        var fakeSendCommand = FakeDataGenerator.CreateSendCommand();
        var message = Message.SetTo(fakeSendCommand.Receiver);
        message.To.Should().Be(fakeSendCommand.Receiver);
    }
}
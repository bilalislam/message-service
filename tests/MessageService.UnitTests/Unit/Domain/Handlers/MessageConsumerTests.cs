using MassTransit;
using MessageService.Api;
using MessageService.UnitTests.Helper;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;

namespace MessageService.UnitTests.Unit.Domain.Handlers;

[TestFixture]
public class MessageConsumerTests
{
    private Mock<ILogger<MessageConsumer>> _mockLogger;
    private Mock<IBlockedUserRepository> _mockBlockedUserRepository;
    private Mock<IMessageRepository> _mockMessageRepository;
    private Mock<ConsumeContext<Message>> _mockConsumer;

    [SetUp]
    public void Init()
    {
        _mockLogger = new Mock<ILogger<MessageConsumer>>();
        _mockBlockedUserRepository = new Mock<IBlockedUserRepository>();
        _mockMessageRepository = new Mock<IMessageRepository>();
        _mockConsumer = new Mock<ConsumeContext<Message>>();
    }

    [TearDown]
    public void Dispose()
    {
        _mockLogger.Reset();
        _mockBlockedUserRepository.Reset();
        _mockConsumer.Reset();
        _mockMessageRepository.Reset();
    }

    [Test]
    public async Task Consume_Should_Success_CallAndVerify()
    {
        var message = Message.Load(FakeDataGenerator.CreateSendCommand());
        _mockBlockedUserRepository.Setup(x => 
                x.GetAsync(message.To, message.From, CancellationToken.None))!
            .ReturnsAsync((BlockUser)null!);
        
        _mockMessageRepository.Setup(x => x.AddAsync(message, CancellationToken.None));
        
        var consumer = new MessageConsumer(_mockLogger.Object, _mockMessageRepository.Object,
            _mockBlockedUserRepository.Object);
        
        _mockConsumer.Setup(x => x.Message).Returns(message);
        await consumer.Consume(_mockConsumer.Object);
        
        _mockMessageRepository.Verify(x => x.AddAsync(_mockConsumer.Object.Message, CancellationToken.None),
            Times.Once);
    }
}
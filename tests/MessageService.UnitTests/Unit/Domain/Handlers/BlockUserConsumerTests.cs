using MassTransit;
using MessageService.Api;
using MessageService.UnitTests.Helper;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;

namespace MessageService.UnitTests.Unit.Domain.Handlers;

[TestFixture]
public class BlockUserConsumerTests
{
    private Mock<ILogger<BlockUserConsumer>> _mockLogger;
    private Mock<IBlockedUserRepository> _mockBlockedUserRepository;
    private Mock<ConsumeContext<BlockUser>> _mockConsumer;

    [SetUp]
    public void Init()
    {
        _mockLogger = new Mock<ILogger<BlockUserConsumer>>();
        _mockBlockedUserRepository = new Mock<IBlockedUserRepository>();
        _mockConsumer = new Mock<ConsumeContext<BlockUser>>();
    }

    [TearDown]
    public void Dispose()
    {
        _mockLogger.Reset();
        _mockBlockedUserRepository.Reset();
        _mockConsumer.Reset();
    }

    [Test]
    public async Task Consume_Should_Success_CallAndVerify()
    {
        var blockUser = BlockUser.Load(FakeDataGenerator.CreateBlockUserCommand());
        _mockBlockedUserRepository.Setup(x => x.AddAsync(blockUser, CancellationToken.None));
        var consumer = new BlockUserConsumer(_mockLogger.Object, _mockBlockedUserRepository.Object);
        _mockConsumer.Setup(x => x.Message).Returns(blockUser);
        await consumer.Consume(_mockConsumer.Object);
        _mockBlockedUserRepository.Verify(x => x.AddAsync(_mockConsumer.Object.Message, CancellationToken.None),
            Times.Once);
    }
}
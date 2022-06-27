using MassTransit;
using MessageService.Api;
using MessageService.UnitTests.Helper;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;

[TestFixture]
public class ActivityConsumerTests
{
    private Mock<ILogger<ActivityConsumer>> _mockLogger;
    private Mock<IActivityRepository> _mockActivityRepository;
    private Mock<ConsumeContext<Activity>> _mockConsumer;

    [SetUp]
    public void Init()
    {
        _mockLogger = new Mock<ILogger<ActivityConsumer>>();
        _mockActivityRepository = new Mock<IActivityRepository>();
        _mockConsumer = new Mock<ConsumeContext<Activity>>();
    }

    [TearDown]
    public void Dispose()
    {
        _mockLogger.Reset();
        _mockActivityRepository.Reset();
        _mockConsumer.Reset();
    }

    [Test]
    public async Task Consume_Should_Success_CallAndVerify()
    {
        var activity = Activity.Load(FakeDataGenerator.CreateActivityCommand());
        _mockActivityRepository.Setup(x => x.AddAsync(activity, CancellationToken.None));
        var consumer = new ActivityConsumer(_mockLogger.Object, _mockActivityRepository.Object);
        _mockConsumer.Setup(x => x.Message).Returns(activity);
        await consumer.Consume(_mockConsumer.Object);
        _mockActivityRepository.Verify(x => x.AddAsync(_mockConsumer.Object.Message, CancellationToken.None),
            Times.Once);
    }
}
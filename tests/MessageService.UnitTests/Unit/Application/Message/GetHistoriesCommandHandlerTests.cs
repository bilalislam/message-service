using FluentAssertions;
using MessageService.Api;
using MessageService.UnitTests.Helper;
using Moq;
using NUnit.Framework;

namespace MessageService.UnitTests.Unit.Application.Message;

[TestFixture]
public class GetHistoriesCommandHandlerTests
{
    private Mock<IMessageRepository> _mockMessageRepository;

    [SetUp]
    public void Init()
    {
        _mockMessageRepository = new Mock<IMessageRepository>();
    }

    [TearDown]
    public void Dispose()
    {
        _mockMessageRepository.Reset();
    }

    [Test]
    public async Task Handle_Should_Return_Valid_WhenGetHistories()
    {
        //Arrange 
        _mockMessageRepository.Setup(x =>
                x.GetHistoriesAsync(It.IsAny<string>(), It.IsAny<string>(), CancellationToken.None))
            .ReturnsAsync(new List<Api.Message>()
            {
                Api.Message.Load(FakeDataGenerator.CreateSendCommand())
            });

        var handler = new GetHistoriesCommandHandler(_mockMessageRepository.Object);

        //Act
        var result = await handler.Handle(FakeDataGenerator.CreateHistoriesCommand(), CancellationToken.None);

        //Assert
        result.ValidateState.Should().Be(ValidationState.Valid);
        _mockMessageRepository.Verify(x =>
            x.GetHistoriesAsync(It.IsAny<string>(), It.IsAny<string>(), CancellationToken.None), Times.Once);
    }
}
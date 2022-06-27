using FluentAssertions;
using MessageService.Api;
using MessageService.UnitTests.Helper;
using Moq;
using NUnit.Framework;

namespace MessageService.UnitTests.Unit.Application.Message;

[TestFixture]
public class GetMessagedUsersCommandHandlerTests
{
    private Mock<IMessageRepository> _messageRepository;


    [SetUp]
    public void Init()
    {
        _messageRepository = new Mock<IMessageRepository>();
    }

    [TearDown]
    public void Dispose()
    {
        _messageRepository.Reset();
    }

    [Test]
    public async Task Handle_Should_Return_Valid_WhenMessagedUsersFound()
    {
        //Arrange 
        _messageRepository.Setup(x => x.GetMessagedUsersAsync(It.IsAny<string>(), CancellationToken.None))
            .ReturnsAsync(new List<Api.Message>()
            {
                Api.Message.Load(FakeDataGenerator.CreateSendCommand())
            });

        var handler = new GetMessagedUsersCommandHandler(_messageRepository.Object);

        //Act
        var result = await handler.Handle(FakeDataGenerator.CreateMessageUsersCommand(), CancellationToken.None);

        //Arrange
        result.ValidateState.Should().Be(ValidationState.Valid);
        _messageRepository.Verify(x=>x.GetMessagedUsersAsync(It.IsAny<string>(),CancellationToken.None));
    }
}
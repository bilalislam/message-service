using FluentAssertions;
using MassTransit;
using MessageService.Api;
using MessageService.UnitTests.Helper;
using Moq;
using NUnit.Framework;

namespace MessageService.UnitTests.Unit.Application.Message;

[TestFixture]
public class SendCommandHandlerTests
{
    private Mock<ISendEndpointProvider> _mockSendEndpointProvider;
    private Mock<IUserRepository> _mockUserRepository;
    private Mock<ISendEndpoint> _mockSendEndpoint;


    [SetUp]
    public void Init()
    {
        _mockSendEndpointProvider = new Mock<ISendEndpointProvider>();
        _mockUserRepository = new Mock<IUserRepository>();
        _mockSendEndpoint = new Mock<ISendEndpoint>();
    }

    [TearDown]
    public void Dispose()
    {
        _mockSendEndpointProvider.Reset();
        _mockUserRepository.Reset();
        _mockSendEndpoint.Reset();
    }

    [Test]
    public async Task Handle_Should_Return_UnProcessable_WhenReceiverUserNotFound()
    {
        //Arrange
        _mockUserRepository.Setup(x =>
                x.GetByEmailAndNameAsync(It.IsAny<string>(), It.IsAny<string>(), CancellationToken.None))
            .ReturnsAsync((User)null!);

        var handler = new SendCommandHandler(_mockSendEndpointProvider.Object, _mockUserRepository.Object);

        //Act
        var result = await handler.Handle(FakeDataGenerator.CreateSendCommand(), CancellationToken.None);

        //Assert
        result.ValidateState.Should().Be(ValidationState.UnProcessable);
        result.Success.Should().Be(false);
    }

    [Test]
    public async Task Handle_Should_Return_Valid_WhenReceiverUserFound()
    {
        //Arrange
        _mockUserRepository.Setup(x =>
                x.GetByEmailAndNameAsync(It.IsAny<string>(), It.IsAny<string>(), CancellationToken.None))
            .ReturnsAsync(User.Load(FakeDataGenerator.CreateSignUpCommand()));

        _mockSendEndpointProvider.Setup(x => x.GetSendEndpoint(new Uri("queue:message")))
            .ReturnsAsync(_mockSendEndpoint.Object);

        var handler = new SendCommandHandler(_mockSendEndpointProvider.Object, _mockUserRepository.Object);

        //Act
        var result = await handler.Handle(FakeDataGenerator.CreateSendCommand(), CancellationToken.None);

        //Assert
        result.ValidateState.Should().Be(ValidationState.Valid);
        result.Success.Should().Be(true);
    }
}
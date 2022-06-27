using FluentAssertions;
using MassTransit;
using MassTransit.Transports;
using MessageService.Api;
using MessageService.UnitTests.Helper;
using Moq;
using NUnit.Framework;

namespace MessageService.UnitTests.Unit.Application.Account;

[TestFixture]
public class SigInCommandHandlerTests
{
    private Mock<IUserRepository> _mockUserRepository;
    private Mock<ITokenProxy> _mockTokenProxy;
    private Mock<ISendEndpointProvider> _mockSendEndpointProvider;
    private Mock<ISendEndpoint> _mockSendEndpoint;

    [SetUp]
    public void Init()
    {
        _mockUserRepository = new Mock<IUserRepository>();
        _mockTokenProxy = new Mock<ITokenProxy>();
        _mockSendEndpointProvider = new Mock<ISendEndpointProvider>();
        _mockSendEndpoint = new Mock<ISendEndpoint>();
    }

    [TearDown]
    public void Dispose()
    {
        _mockUserRepository.Reset();
        _mockTokenProxy.Reset();
        _mockSendEndpointProvider.Reset();
        _mockSendEndpoint.Reset();
    }

    [Test]
    public async Task Handle_Should_Return_DoesNotExists()
    {
        //Arrange 
        var command = FakeDataGenerator.CreateSignInCommand();
        _mockUserRepository.Setup(x => x.GetByEmailAsync(command.Email, CancellationToken.None))!
            .ReturnsAsync((User)null!);

        //Act
        var handler = new SignInCommandHandler(_mockUserRepository.Object, _mockTokenProxy.Object,
            _mockSendEndpointProvider.Object);
        var result = await handler.Handle(command, CancellationToken.None);

        //Assert
        result.ValidateState.Should().Be(ValidationState.DoesNotExist);
        result.Success.Should().Be(false);
    }

    [Test]
    public async Task Handle_Should_Return_UnProcessable_When_PasswordMismatch()
    {
        //Arrange 
        var fakeUserCommand = FakeDataGenerator.CreateSignUpCommand();
        fakeUserCommand.Password = "123";
        var user = User.Load(fakeUserCommand);
        var command = FakeDataGenerator.CreateSignInCommand();
        _mockUserRepository.Setup(x => x.GetByEmailAsync(command.Email, CancellationToken.None))!
            .ReturnsAsync(user);
        _mockSendEndpointProvider.Setup(x => x.GetSendEndpoint(new Uri("queue:activity")))
            .ReturnsAsync(_mockSendEndpoint.Object);

        //Act
        var handler = new SignInCommandHandler(_mockUserRepository.Object, _mockTokenProxy.Object,
            _mockSendEndpointProvider.Object);
        var result = await handler.Handle(command, CancellationToken.None);

        //Assert
        result.ValidateState.Should().Be(ValidationState.UnProcessable);
        result.Success.Should().Be(false);
    }

    [Test]
    public async Task Handle_Should_Return_Valid_When_PasswordValid()
    {
        //Arrange 
        var fakeUserCommand = FakeDataGenerator.CreateSignUpCommand();
        fakeUserCommand.Password = "123";
        var user = User.Load(fakeUserCommand);
        var command = FakeDataGenerator.CreateSignInCommand();
        command.Password = "123";
        _mockUserRepository.Setup(x => x.GetByEmailAsync(command.Email, CancellationToken.None))!
            .ReturnsAsync(user);
        _mockSendEndpointProvider.Setup(x => x.GetSendEndpoint(new Uri("queue:activity")))
            .ReturnsAsync(_mockSendEndpoint.Object);
        _mockTokenProxy.Setup(x => x.CreateAccessToken(user))
            .Returns(new Token());

        //Act
        var handler = new SignInCommandHandler(_mockUserRepository.Object, _mockTokenProxy.Object,
            _mockSendEndpointProvider.Object);
        var result = await handler.Handle(command, CancellationToken.None);

        //Assert
        result.ValidateState.Should().Be(ValidationState.Valid);
        result.Success.Should().Be(true);
    }
}
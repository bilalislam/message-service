using FluentAssertions;
using MessageService.Api;
using MessageService.UnitTests.Helper;
using MongoDB.Driver.Linq;
using Moq;
using NUnit.Framework;

namespace MessageService.UnitTests.Unit.Application.Account;

[TestFixture]
public class RefreshTokenCommandHandlerTests
{
    private Mock<IUserRepository> _mockUserRepository;
    private Mock<ITokenProxy> _mockTokenProxy;

    [SetUp]
    public void Init()
    {
        _mockUserRepository = new Mock<IUserRepository>();
        _mockTokenProxy = new Mock<ITokenProxy>();
    }

    [TearDown]
    public void Dispose()
    {
        _mockUserRepository.Reset();
        _mockTokenProxy.Reset();
    }


    [Test]
    public async Task Handle_Should_Return_DoesNotExists_WhenUserNotFound()
    {
        //Arrange 
        var request = FakeDataGenerator.CreateRefreshTokenCommand();
        _mockUserRepository.Setup(x => x.GetByEmailAsync(request.Email, CancellationToken.None))
            .ReturnsAsync((User)null!);

        var handler = new RefreshTokenCommandHandler(_mockUserRepository.Object, _mockTokenProxy.Object);

        //Act
        var result = await handler.Handle(request, CancellationToken.None);

        //Assert
        result.ValidateState.Should().Be(ValidationState.DoesNotExist);
    }

    [Test]
    public async Task Handle_Should_Return_DoesNotExists_TokenInValid()
    {
        //Arrange 
        var user = User.Load(FakeDataGenerator.CreateSignUpCommand());
        _mockTokenProxy.Setup(x => x.CreateAccessToken(user))
            .Returns(FakeDataGenerator.CreateToken);

        user.CreateToken(_mockTokenProxy.Object);

        var request = FakeDataGenerator.CreateRefreshTokenCommand();
        _mockUserRepository.Setup(x => x.GetByEmailAsync(request.Email, CancellationToken.None))
            .ReturnsAsync(user);

        var handler = new RefreshTokenCommandHandler(_mockUserRepository.Object, _mockTokenProxy.Object);

        //Act
        var result = await handler.Handle(request, CancellationToken.None);

        //Assert
        result.ValidateState.Should().Be(ValidationState.DoesNotExist);
    }

    [Test]
    public async Task Handle_Should_Return_Valid()
    {
        //Arrange 
        var user = User.Load(FakeDataGenerator.CreateSignUpCommand());
        var token = FakeDataGenerator.CreateToken();
        token.RefreshTokenExpiration = DateTime.Now.AddMinutes(5);
        
        _mockTokenProxy.Setup(x => x.CreateAccessToken(user))
            .Returns(token);
        
        user.CreateToken(_mockTokenProxy.Object);

        var request = FakeDataGenerator.CreateRefreshTokenCommand();
        request.RefreshToken = token.RefreshToken;
        
        _mockUserRepository.Setup(x => x.GetByEmailAsync(request.Email, CancellationToken.None))
            .ReturnsAsync(user);

        var handler = new RefreshTokenCommandHandler(_mockUserRepository.Object, _mockTokenProxy.Object);

        //Act
        var result = await handler.Handle(request, CancellationToken.None);

        //Assert
        result.ValidateState.Should().Be(ValidationState.Valid);
        result.Success.Should().Be(true);
    }
}
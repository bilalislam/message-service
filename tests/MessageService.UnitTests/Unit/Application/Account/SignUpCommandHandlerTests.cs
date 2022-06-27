using FluentAssertions;
using MessageService.Api;
using MessageService.UnitTests.Helper;
using Moq;
using NUnit.Framework;

namespace MessageService.UnitTests.Unit.Application.Account;

[TestFixture]
public class SignUpCommandHandlerTests
{
    private Mock<IUserRepository> _mockUserRepository;

    [SetUp]
    public void Init()
    {
        _mockUserRepository = new Mock<IUserRepository>();
    }

    [TearDown]
    public void Dispose()
    {
        _mockUserRepository.Reset();
    }

    [Test]
    public async Task Handle_Should_Return_AlreadyExists_WhenUserRegistered()
    {
        //Arrange
        var request = FakeDataGenerator.CreateSignUpCommand();
        _mockUserRepository.Setup(x => x.GetByEmailAsync(request.Email, CancellationToken.None))
            .ReturnsAsync(User.Load(FakeDataGenerator.CreateSignUpCommand()));
        var handler = new SignUpCommandHandler(_mockUserRepository.Object);

        //Act
        var result = await handler.Handle(request, CancellationToken.None);

        //Assert
        result.ValidateState.Should().Be(ValidationState.AlreadyExist);
        result.Success.Should().Be(false);
    }
    
    [Test]
    public async Task Handle_Should_Return_Success_WhenValid()
    {
        //Arrange
        var request = FakeDataGenerator.CreateSignUpCommand();
        _mockUserRepository.Setup(x => x.GetByEmailAsync(request.Email, CancellationToken.None))
            .ReturnsAsync((User)null!);
        var handler = new SignUpCommandHandler(_mockUserRepository.Object);

        //Act
        var result = await handler.Handle(request, CancellationToken.None);

        //Assert
        result.ValidateState.Should().Be(ValidationState.Valid);
        result.Success.Should().Be(true);
        
    }
}
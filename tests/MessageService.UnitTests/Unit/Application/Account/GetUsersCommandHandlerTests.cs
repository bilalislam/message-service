using FluentAssertions;
using MessageService.Api;
using MessageService.Api.Controllers.UseCases.Account.GetUsers;
using MessageService.UnitTests.Helper;
using Moq;
using NUnit.Framework;

namespace MessageService.UnitTests.Unit.Application.Account;

[TestFixture]
public class GetUsersCommandHandlerTests
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
    public async Task Handle_Should_Return_DoesNotExists_WhenUsersNotFound()
    {
        //Arrange
        _mockUserRepository.Setup(x => x.ListAsync(CancellationToken.None))
            .ReturnsAsync(new List<User>());
        var handler = new GetUsersCommandHandler(_mockUserRepository.Object);

        //Act
        var result = await handler.Handle(It.IsAny<GetUsersCommand>(), CancellationToken.None);

        //Assert
        result.ValidateState.Should().Be(ValidationState.DoesNotExist);
        result.Success.Should().Be(false);
    }

    [Test]
    public async Task Handle_Should_Return_Valid_WhenUsersExists()
    {
        //Arrange
        _mockUserRepository.Setup(x => x.ListAsync(CancellationToken.None))
            .ReturnsAsync(new List<User>()
            {
                User.Load(FakeDataGenerator.CreateSignUpCommand())
            });
        var handler = new GetUsersCommandHandler(_mockUserRepository.Object);

        //Act
        var result = await handler.Handle(It.IsAny<GetUsersCommand>(), CancellationToken.None);

        //Assert
        result.ValidateState.Should().Be(ValidationState.Valid);
        result.Success.Should().Be(true);
    }
}
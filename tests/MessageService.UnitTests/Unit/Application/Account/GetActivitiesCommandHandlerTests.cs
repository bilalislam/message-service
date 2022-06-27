using FluentAssertions;
using MessageService.Api;
using MessageService.UnitTests.Helper;
using Moq;
using NUnit.Framework;

namespace MessageService.UnitTests.Unit.Application.Account;

[TestFixture]
public class GetActivitiesCommandHandlerTests
{
    private Mock<IActivityRepository> _mockActivityRepository;

    [SetUp]
    public void Init()
    {
        _mockActivityRepository = new Mock<IActivityRepository>();
    }

    [TearDown]
    public void Dispose()
    {
        _mockActivityRepository.Reset();
    }

    [Test]
    public async Task Handle_Should_Return_DoesNotExists_WhenActivitiesNotFound()
    {
        //Arrange
        var activitiesCommand = FakeDataGenerator.CreateActivitiesCommand();
        _mockActivityRepository.Setup(x => x.FilterAsync(activitiesCommand.Email, CancellationToken.None))
            .ReturnsAsync(new List<Activity>());

        var handler = new GetActivitiesCommandHandler(_mockActivityRepository.Object);

        //Act
        var result = await handler.Handle(activitiesCommand, CancellationToken.None);

        //Assert

        result.ValidateState.Should().Be(ValidationState.DoesNotExist);
        result.Success.Should().Be(false);
    }

    [Test]
    public async Task Handle_Should_Return_DoesNotExists_WhenActivitiesExists()
    {
        //Arrange
        var activitiesCommand = FakeDataGenerator.CreateActivitiesCommand();
        _mockActivityRepository.Setup(x => x.FilterAsync(activitiesCommand.Email, CancellationToken.None))
            .ReturnsAsync(new List<Activity>()
            {
                Activity.Load(FakeDataGenerator.CreateActivityCommand())
            });

        var handler = new GetActivitiesCommandHandler(_mockActivityRepository.Object);

        //Act
        var result = await handler.Handle(activitiesCommand, CancellationToken.None);

        //Assert

        result.ValidateState.Should().Be(ValidationState.Valid);
        result.Success.Should().Be(true);
    }
}
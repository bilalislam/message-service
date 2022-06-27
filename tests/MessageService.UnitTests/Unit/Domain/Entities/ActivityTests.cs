using FluentAssertions;
using MessageService.Api;
using MessageService.Api.Domain.Constants;
using MessageService.UnitTests.Helper;
using NUnit.Framework;

namespace MessageService.UnitTests;

[TestFixture]
public class ActivityTests
{
    [Test]
    public void Load_ShouldThrowException_WhenDomainDtoIsIdNull()
    {
        var activityCommand = FakeDataGenerator.CreateActivityCommand();
        activityCommand.Id = null;
        var exception = Assert.Throws<ValidationException>(() => Activity.Load(activityCommand));
        exception.Code.Should().Be(nameof(DomainErrorCodes.EDService1010));
    }

    [Test]
    public void Load_ShouldThrowException_WhenDomainDtoIsEmailNull()
    {
        var activityCommand = FakeDataGenerator.CreateActivityCommand();
        activityCommand.Email = null;
        var exception = Assert.Throws<ValidationException>(() => Activity.Load(activityCommand));
        exception.Code.Should().Be(nameof(DomainErrorCodes.EDService1011));
    }

    [Test]
    public void Load_ShouldThrowException_WhenDomainDtoIsEventNull()
    {
        var activityCommand = FakeDataGenerator.CreateActivityCommand();
        activityCommand.Event = null;
        var exception = Assert.Throws<ValidationException>(() => Activity.Load(activityCommand));
        exception.Code.Should().Be(nameof(DomainErrorCodes.EDService1012));
    }

    [Test]
    public void Load_ShouldSuccess_WhenIsDomainValid()
    {
        var activityCommand = FakeDataGenerator.CreateActivityCommand();
        var activity = Activity.Load(activityCommand);
        activity.Should().NotBeNull();
        activity.EventOn.Should().Be(activity.EventOn);
    }
}
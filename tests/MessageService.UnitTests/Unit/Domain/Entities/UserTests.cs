using FluentAssertions;
using MessageService.Api;
using MessageService.Api.Domain.Constants;
using MessageService.UnitTests.Helper;
using Moq;
using NUnit.Framework;
using Assert = NUnit.Framework.Assert;

namespace MessageService.UnitTests;

[TestFixture]
public class UserTests
{
    private Mock<ITokenProxy> _mockTokenProxy;

    [SetUp]
    public void Init()
    {
        _mockTokenProxy = new Mock<ITokenProxy>();
    }

    [TearDown]
    public void Dispose()
    {
        _mockTokenProxy.Reset();
    }

    [Test]
    public void Load_ShouldThrowException_WhenDomainDtoIsNull()
    {
        var exception = Assert.Throws<ValidationException>(() => User.Load(null));
        exception.Code.Should().Be(nameof(DomainErrorCodes.EDService1002));
    }

    [Test]
    public void Load_ShouldThrowException_WhenIsEmailNull()
    {
        var fakeUser = FakeDataGenerator.CreateSignUpCommand();
        fakeUser.Email = null;
        var exception = Assert.Throws<ValidationException>(() => User.Load(fakeUser));
        exception.Code.Should().Be(nameof(DomainErrorCodes.EDService1003));
    }

    [Test]
    public void Load_ShouldThrowException_WhenIsNameNull()
    {
        var fakeUser = FakeDataGenerator.CreateSignUpCommand();
        fakeUser.Name = null;
        var exception = Assert.Throws<ValidationException>(() => User.Load(fakeUser));
        exception.Code.Should().Be(nameof(DomainErrorCodes.EDService1004));
    }

    [Test]
    public void Load_ShouldThrowException_WhenIsPasswordNull()
    {
        var fakeUser = FakeDataGenerator.CreateSignUpCommand();
        fakeUser.Password = null;
        var exception = Assert.Throws<ValidationException>(() => User.Load(fakeUser));
        exception.Code.Should().Be(nameof(DomainErrorCodes.EDService1005));
    }

    [Test]
    public void Load_ShouldThrowException_WhenIsSurnameNull()
    {
        var fakeUser = FakeDataGenerator.CreateSignUpCommand();
        fakeUser.Surname = null;
        var exception = Assert.Throws<ValidationException>(() => User.Load(fakeUser));
        exception.Code.Should().Be(nameof(DomainErrorCodes.EDService1006));
    }

    [Test]
    public void Load_ShouldSuccess_WhenIsDomainValid()
    {
        var fakeUser = FakeDataGenerator.CreateSignUpCommand();
        var user = User.Load(fakeUser);
        user.Should().NotBeNull();
    }

    [Test]
    public void Load_ShouldSuccess_WhenIsCreateTokenValid()
    {
        var fakeUser = FakeDataGenerator.CreateSignUpCommand();
        var user = User.Load(fakeUser);
        _mockTokenProxy.Setup(x => x.CreateAccessToken(user))
            .Returns(FakeDataGenerator.CreateToken);
        user.CreateToken(_mockTokenProxy.Object);
        user.Should().NotBeNull();
        user.Token.Should().NotBeNull();
        _mockTokenProxy.Verify(x => x.CreateAccessToken(user), Times.Once);
    }
}
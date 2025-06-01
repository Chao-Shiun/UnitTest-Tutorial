using NSubstitute;
using WebApplication.Repository.Interface;
using WebApplication.Service;

namespace WebApplication.Tests;

[TestFixture]
public class RegisterServiceTests
{
    [Test]
    public void Register_WhenUserDoesNotExist_AddsUser()
    {
        var repo = Substitute.For<IUserRepository>();

        // Stub: 預先定義 repository 的回傳值
        repo.Exists("Alice").Returns(false);

        var service = new RegisterService(repo);

        // Act
        var result = service.Register("Alice");

        // Assert: Mock 的觀念（驗證方法是否被呼叫）
        repo.Received(1).AddUser("Alice");
        Assert.That(true,Is.EqualTo(result));
    }

    [Test]
    public void Register_WhenUserExists_DoesNotAddUser()
    {
        var repo = Substitute.For<IUserRepository>();
        repo.Exists("Alice").Returns(true);

        var service = new RegisterService(repo);

        var result = service.Register("Alice");

        repo.DidNotReceive().AddUser(Arg.Any<string>());
        Assert.That(false,Is.EqualTo(result));
    }
}
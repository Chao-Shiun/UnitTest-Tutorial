using NSubstitute;
using WebApplication.Repository.Interface;
using WebApplication.Service;

namespace WebApplication.Tests;

[TestFixture]
public class UserServiceTests
{
    [Test]
    public void GetUserName_ReturnsCorrectName()
    {
        // Arrange：建立 stub（固定回傳值的假物件）
        var stubRepo = Substitute.For<IUserRepository>();
        stubRepo.GetUserName(1).Returns("Charles");

        var service = new UserService(stubRepo);

        // Act
        var result = service.GetUserName(1);

        // Assert
        Assert.That("Charles", Is.EqualTo(result));
    }
}
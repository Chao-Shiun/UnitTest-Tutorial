using WebApplication.Repository.Interface;

namespace WebApplication.Service;

public class UserService(IUserRepository repo)
{
    public string GetUserName(int userId)
    {
        return repo.GetUserName(userId);
    }
}
using WebApplication.Repository.Interface;

namespace WebApplication.Service;

public class RegisterService(IUserRepository repo)
{
    public bool Register(string userName)
    {
        if (repo.Exists(userName))
            return false;

        repo.AddUser(userName);
        return true;
    }
}
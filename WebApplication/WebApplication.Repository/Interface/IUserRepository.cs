namespace WebApplication.Repository.Interface;

public interface IUserRepository
{
    string GetUserName(int userId);
    bool Exists(string userName);
    void AddUser(string userName);
}
using UnitTestExercise.Repository;

namespace UnitTestExercise.Service.Interface;

public interface IMemberService
{
    bool Register(string idNumber, string password, string name);
    Member? GetMember(string idNumber);
    bool VerifyPassword(string idNumber, string password);
}

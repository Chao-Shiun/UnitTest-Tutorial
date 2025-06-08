namespace UnitTestExercise.Repository.Interface;

public interface IMemberRepository
{
    bool Exists(string idNumber);
    void AddMember(Member member);
    Member? GetMember(string idNumber);
}

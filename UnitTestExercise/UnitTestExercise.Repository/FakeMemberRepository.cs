using UnitTestExercise.Repository.Interface;

namespace UnitTestExercise.Repository;

public class FakeMemberRepository : IMemberRepository
{
    private readonly Dictionary<string, Member> _members = new();

    public bool Exists(string idNumber) => _members.ContainsKey(idNumber);

    public void AddMember(Member member)
    {
        _members[member.IdNumber] = member;
    }

    public Member? GetMember(string idNumber)
    {
        return _members.TryGetValue(idNumber, out var member) ? member : null;
    }
}

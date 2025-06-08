using System.Security.Cryptography;
using System.Text;
using UnitTestExercise.Repository;
using UnitTestExercise.Repository.Interface;
using UnitTestExercise.Service.Interface;

namespace UnitTestExercise.Service;

public class MemberService(IMemberRepository repo) : IMemberService
{
    public bool Register(string idNumber, string password, string name)
    {
        if (string.IsNullOrWhiteSpace(idNumber) || idNumber.Length != 10)
            throw new ArgumentException("Id number must be length 10", nameof(idNumber));

        if (repo.Exists(idNumber))
            return false;

        var hash = Hash(password);
        var member = new Member { IdNumber = idNumber, Name = name, PasswordHash = hash };
        repo.AddMember(member);
        return true;
    }

    public Member? GetMember(string idNumber) => repo.GetMember(idNumber);

    public bool VerifyPassword(string idNumber, string password)
    {
        var member = repo.GetMember(idNumber);
        if (member == null) return false;
        var hash = Hash(password);
        return string.Equals(member.PasswordHash, hash, StringComparison.OrdinalIgnoreCase);
    }

    private static string Hash(string text)
    {
        var bytes = Encoding.UTF8.GetBytes(text);
        var hashed = MD5.HashData(bytes);
        return Convert.ToHexString(hashed);
    }
}

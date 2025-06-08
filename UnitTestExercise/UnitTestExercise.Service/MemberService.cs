using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using UnitTestExercise.Common.Enum;
using UnitTestExercise.Common.Model.Dto;
using UnitTestExercise.Common.Model.Parameter;
using UnitTestExercise.Repository;
using UnitTestExercise.Repository.Interface;
using UnitTestExercise.Service.Interface;

namespace UnitTestExercise.Service;

public class MemberService(IMemberRepository memberRepository, IEmailRepository emailRepository) : IMemberService
{
    public MemberServiceDto Register(MemberParameter parameter)
    {
        if (string.IsNullOrWhiteSpace(parameter.IdNumber) || parameter.IdNumber.Length != 10)
            throw new ArgumentException("Id number must be length 10", nameof(parameter.IdNumber));

        if(string.IsNullOrWhiteSpace(parameter.Name))
            throw new ArgumentException("Name cannot be empty", nameof(parameter.Name));
        
        // 新增電子信箱格式驗證
        if (!string.IsNullOrWhiteSpace(parameter.Email) && !IsValidEmail(parameter.Email))
            throw new ArgumentException("Email format is invalid", nameof(parameter.Email));


        if (memberRepository.Exists(parameter.IdNumber))
            return new MemberServiceDto
            {
                Code = StatusEnum.UserIsExits,
                Message = StatusEnum.UserIsExits.GetDescription()
            };

        var hash = Hash(parameter.Password);
        var member = new GetMemberDto { IdNumber = parameter.IdNumber, Name = parameter.Name, PasswordHash = hash };
        memberRepository.AddMember(member);
        emailRepository.SendRegisterEmail(parameter.Email, parameter.IdNumber);
        return new MemberServiceDto();
    }

    public GetMemberDto? GetMember(string idNumber) => memberRepository.GetMember(idNumber);


    private static string Hash(string text)
    {
        var bytes = Encoding.UTF8.GetBytes(text);
        var hashed = MD5.HashData(bytes);
        return Convert.ToHexString(hashed);
    }

    private static bool IsValidEmail(string email)
    {
        // 簡易正則，符合大部分 email 格式驗證需求
        var pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
        return Regex.IsMatch(email, pattern, RegexOptions.IgnoreCase);
    }
}
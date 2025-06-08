using UnitTestExercise.Common.Model.Dto;
using UnitTestExercise.Common.Model.Parameter;

namespace UnitTestExercise.Service.Interface;

public interface IMemberService
{
    MemberServiceDto Register(MemberParameter parameter);
    GetMemberDto? GetMember(string idNumber);
}

using UnitTestExercise.Common.Enum;

namespace UnitTestExercise.Common.Model.Dto;

public class MemberServiceDto
{
    public StatusEnum Code { get; set; } = StatusEnum.Success;
    public string Message { get; set; } = string.Empty;
}

public class GetMemberDto
{
    public string IdNumber { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
}

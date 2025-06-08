using UnitTestExercise.Common.Model.Dto;

namespace UnitTestExercise.Repository.Interface;

public interface IMemberRepository
{
    /// <summary>
    /// 檢查idNumber是否已存在
    /// </summary>
    /// <param name="idNumber"></param>
    /// <returns></returns>
    bool Exists(string idNumber);
    /// <summary>
    /// 新增會員
    /// </summary>
    /// <param name="member"></param>
    void AddMember(GetMemberDto member);
    /// <summary>
    /// 取得會員資料
    /// </summary>
    /// <param name="idNumber"></param>
    /// <returns></returns>
    GetMemberDto? GetMember(string idNumber);
}

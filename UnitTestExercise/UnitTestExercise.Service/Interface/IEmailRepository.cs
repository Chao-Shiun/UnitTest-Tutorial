namespace UnitTestExercise.Service.Interface;

public interface IEmailRepository
{
    /// <summary>
    /// 發送註冊驗證郵件
    /// </summary>
    /// <param name="email"></param>
    /// <param name="idNumber"></param>
    void SendRegisterEmail(string email, string idNumber);
}
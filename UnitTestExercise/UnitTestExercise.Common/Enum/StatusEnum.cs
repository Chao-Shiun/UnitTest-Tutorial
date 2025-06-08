using System.ComponentModel;

namespace UnitTestExercise.Common.Enum;

public enum StatusEnum
{
    [Description("已成功")]
    Success = 0,
    [Description("使用者已存在")]
    UserIsExits = 1,
}
public static class StatusEnumExtensions
{
    public static string GetDescription(this StatusEnum status)
    {
        var field = status.GetType().GetField(status.ToString());
        if (field != null)
        {
            var attribute = (DescriptionAttribute?)Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute));
            return attribute?.Description ?? status.ToString();
        }
        return status.ToString();
    }
}
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace TheImageComparer.UI.Helpers;
public static class EnumHelpers
{
    public static string? GetDisplayName(this Enum enumValue)
    {
        return enumValue
            .GetType()
            .GetMember(enumValue.ToString())
            .First()
            .GetCustomAttribute<DisplayAttribute>()?
            .GetName();
    }
}

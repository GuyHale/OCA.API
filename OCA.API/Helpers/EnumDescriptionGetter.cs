using System.ComponentModel;
using System.Reflection;

namespace OCA.API.Helpers
{
    public static class EnumDescriptionGetter
    {
        public static string Description(this Enum? value)
        {
            if (value is null)
                return string.Empty;
            FieldInfo? currentField = value.GetType().GetField(value.ToString());
            DescriptionAttribute? descriptionAttr = currentField?.GetCustomAttribute<DescriptionAttribute>(false);
            return descriptionAttr?.Description ?? string.Empty;
        }
    }
}

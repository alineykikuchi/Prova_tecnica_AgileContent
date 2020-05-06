using System;

namespace iTaas.Models.Extension
{
    public static class DataValueExtension
    {
        public static string GetDatavalue(this Enum value)
        {
            var enumType = value.GetType();
            var name = Enum.GetName(enumType, value);
            if (!string.IsNullOrEmpty(name))
            {
                var field = enumType.GetField(Enum.GetName(enumType, value));

                if (field != null)
                {
                    var attributes = field.GetCustomAttributes(typeof(DataValueAttribute), false);

                    return attributes.Length == 0 ? value.ToString() : ((DataValueAttribute)attributes[0]).Value[0];
                }
            }
            return value.ToString();
        }
    }
}

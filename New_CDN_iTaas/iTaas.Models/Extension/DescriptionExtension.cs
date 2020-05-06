using System;

namespace iTaas.Models.Extension
{
    public static class DescriptionExtension
    {
        public static string Description(this Enum value)
        {  
            var enumType = value.GetType();
            var name = Enum.GetName(enumType, value);

            if (!string.IsNullOrEmpty(name))
            {
                var field = enumType.GetField(Enum.GetName(enumType, value));

                if (field != null)
                {
                    var attributes = field.GetCustomAttributes(typeof(System.ComponentModel.DescriptionAttribute), false);
 
                    return attributes.Length == 0 ? value.ToString() : ((System.ComponentModel.DescriptionAttribute)attributes[0]).Description;
                }
            }
            return value.ToString();
        }
    }
}

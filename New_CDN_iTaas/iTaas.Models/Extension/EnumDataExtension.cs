using System;
using System.Collections.Generic;

namespace iTaas.Models.Extension
{
    public static class EnumDataExtension
    {
        public static List<T> GetDataValues<T>(Type enumeration)
        {
            List<T> listEnum = new List<T>();
            var values = Enum.GetValues(enumeration);

            foreach (var value in values)
            {
                T item = (T)Enum.Parse(enumeration, value.ToString());

                listEnum.Add(item);
            }

            return listEnum;
        }
    }
}

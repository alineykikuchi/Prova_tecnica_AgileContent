using System;
using System.Collections.Generic;

namespace iTaas.Models.Extension
{
    [AttributeUsage(AttributeTargets.Field)]
    public class DataValueAttribute : Attribute
    {
        private readonly List<string> value;

        public List<string> Value
        {
            get { return value; }
        }

        public DataValueAttribute(params string[] Valores)
        {
            value = new List<string>();

            foreach (string Valor in Valores)
            {
                value.Add(Valor);
            }

        }
    }
}

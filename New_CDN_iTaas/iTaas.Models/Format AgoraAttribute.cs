using System;

namespace iTaas.Models
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    public class FormatAgoraAttribute : Attribute
    {
        public string Value { get; private set; }
        public FormatAgoraAttribute(string value)
        {
            this.Value = value;
        }
    }
}

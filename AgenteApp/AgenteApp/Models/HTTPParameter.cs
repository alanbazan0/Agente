using System;
namespace AgenteApp.Models
{
    public class HTTPParameter
    {
        public HTTPParameter(string name, string value)
        {
            Name = name;
            Value = value;

        }
        public string Name
        {
            get;
            set;
        }
        public string Value
        {
            get;
            set;
        }
    }
}

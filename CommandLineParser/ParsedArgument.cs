using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommandLineParser
{
    // Named arguments have the form Name=Value
    public class ParsedArgument
    {
        //================================================================================
        public ParsedArgument(String name, String value)
        {
            Name = name;
            Value = value;
        }
        //================================================================================
        public String Name { get; private set; }
        //================================================================================
        public String Value { get; private set; }
        //================================================================================
        public T As<T>()
        {
            return (T)Convert.ChangeType(Value, typeof(T));
        }
        //================================================================================
    }
}

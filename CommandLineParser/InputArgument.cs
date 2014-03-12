using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommandLineParser
{
    public class InputArgument
    {
        //================================================================================
        public InputArgument(String name, String value)
        {
            Name = name;
            Value = value;
        }
        //================================================================================
        public String Name { get; private set; }
        //================================================================================
        public String Value { get; private set; }
        //================================================================================
        public static InputArgument Parse(String argStr)
        {
            argStr = argStr.Trim();
            if (!CanParse(argStr))
                return null;

            var split = argStr.Split('=');
            var name = split[0];
            var value = split[1];

            return new InputArgument(name, value);
        }
        //================================================================================
        public static bool CanParse(String argStr)
        {
            if (!argStr.Contains("="))
                return false;

            var split = argStr.Split('=');
            return split.Length == 2 && split.All(x => !String.IsNullOrWhiteSpace(x));
        }
        //================================================================================
    }
}

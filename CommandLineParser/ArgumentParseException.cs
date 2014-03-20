using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommandLineParser
{
    public class ArgumentParseException : Exception
    {
        //================================================================================
        public ArgumentParseException(String argumentName, String error)
        {
            ArgumentName = argumentName;
            Error = error;
        }
        //================================================================================
        public String ArgumentName { get; private set; }
        //================================================================================
        public String Error { get; private set; }
        //================================================================================
        public String FormattedErrorString()
        {
            return String.Format("Argument: {0}. Error: {1}", ArgumentName, Error);
        }
        //================================================================================
    }
}

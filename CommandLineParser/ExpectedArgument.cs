using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommandLineParser
{
    public abstract class ExpectedArgument
    {
        //================================================================================
        public ExpectedArgument(String name, String defaultValue = null)
            : this(name, null, true, defaultValue)
        {}
        //================================================================================
        public ExpectedArgument(String name, bool ignoreCase, String defaultValue = null)
            : this(name, null, ignoreCase, defaultValue)
        {}
        //================================================================================
        public ExpectedArgument(String name, String description, String defaultValue = null)
            : this(name, description, true, defaultValue)
        {}
        //================================================================================
        public ExpectedArgument(String name, String description, bool ignoreCase, String defaultValue = null)
        {
            Name = name;
            DefaultValue = defaultValue;
            Description = description;
            IgnoreCase = ignoreCase;
        }
        //================================================================================
        public String Name { get; private set; }
        //================================================================================
        public String DefaultValue { get; private set; }
        //================================================================================
        public bool IgnoreCase { get; private set; }
        //================================================================================
        public String Description { get; private set; }
        //================================================================================
        public ParsedArgument ParseFromInputArguments(IEnumerable<InputArgument> inputArgs)
        {
            var thisArg = inputArgs.FirstOrDefault(x => x.Name.Equals(Name, IgnoreCase ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal));
            if (thisArg != null)
                return ParseArgument(thisArg);
            else if (DefaultValue != null)
                return new ParsedArgument(Name, DefaultValue);

            throw new ArgumentParseException(Name, String.Format("Mandatory command line argument {0} was not present. Description: {1}", Name, Description ?? "N/A"));
        }
        //================================================================================
        private ParsedArgument ParseArgument(InputArgument input)
        {
            String errorMessage = null;
            if (!CheckArgument(input.Value, ref errorMessage))
                throw new ArgumentParseException(Name, String.Format("Could not parse argument {0} with value {1}. Argument description: {2}. Error: {3}", input.Name, input.Value, Description ?? "N/A", errorMessage));

            return new ParsedArgument(input.Name, input.Value);
        }
        //================================================================================
        protected abstract bool CheckArgument(String value, ref String errorMessage);
    }

    public class ExpectedStringArgument : ExpectedArgument
    {
        //================================================================================
        public ExpectedStringArgument(String name, String defaultValue = null)
            : base(name, defaultValue)
        { }
        //================================================================================
        public ExpectedStringArgument(String name, bool ignoreCase, String defaultValue = null)
            : base(name, ignoreCase, defaultValue)
        {}
        //================================================================================
        protected override bool CheckArgument(string value, ref string errorMessage)
        {
            if (!String.IsNullOrWhiteSpace(value))
                return true;

            errorMessage = "String value was null or whitespace, but should not be.";
            return false;
        }
        //================================================================================
    }

    public class ExpectedNumericArgument : ExpectedArgument
    {
        //================================================================================
        public ExpectedNumericArgument(String name, double? defaultValue = null)
            : base(name, defaultValue != null ? defaultValue.Value.ToString() : null)
        { }
        //================================================================================
        public ExpectedNumericArgument(String name, bool ignoreCase, double? defaultValue = null)
            : base(name, ignoreCase, defaultValue != null ? defaultValue.Value.ToString() : null)
        {}
        //================================================================================
        protected override bool CheckArgument(string value, ref string errorMessage)
        {
            double parsedVal;
            if (double.TryParse(value, out parsedVal))
                return true;

            errorMessage = "Value was not numeric, but should be.";
            return false;
        }
        //================================================================================
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommandLineParser
{
    public static class FluentParser
    {
        //================================================================================
        public static FluentParserState Setup()
        {
            return new FluentParserState();
        }
        //================================================================================
    }

    public class FluentParserState
    {
        //================================================================================
        internal FluentParserState(IEnumerable<ExpectedArgument> expectedArgs = null)
        {
            ExpectedArguments = expectedArgs != null ? expectedArgs.ToList() : new List<ExpectedArgument>();
        }
        //================================================================================
        internal IList<ExpectedArgument> ExpectedArguments { get; private set; }
        //================================================================================
        public FluentParserState WithString(String name, String defaultValue = null)
        {
            return new FluentParserState(ExpectedArguments.Concat(new[] { new ExpectedStringArgument(name, defaultValue) }));
        }
        //================================================================================
        public FluentParserState WithString(String name, bool ignoreCase, String defaultValue = null)
        {
            return new FluentParserState(ExpectedArguments.Concat(new[] { new ExpectedStringArgument(name, ignoreCase, defaultValue) }));
        }
        //================================================================================
        public FluentParserState WithNumber(String name, double? defaultValue = null)
        {
            return new FluentParserState(ExpectedArguments.Concat(new[] { new ExpectedNumericArgument(name, defaultValue) }));
        }
        //================================================================================
        public FluentParserState WithNumber(String name, bool ignoreCase, double? defaultValue = null)
        {
            return new FluentParserState(ExpectedArguments.Concat(new[] { new ExpectedNumericArgument(name, ignoreCase, defaultValue),  }));
        }
        //================================================================================
        public IDictionary<String, ParsedArgument> Parse(IEnumerable<String> commandLine)
        {
            return Parser.Parse(commandLine, ExpectedArguments);
        }
        //================================================================================
    }
}

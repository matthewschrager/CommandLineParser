using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommandLineParser
{
    public static class Parser
    {
        //================================================================================
        public static IDictionary<String, ParsedArgument> Parse(IEnumerable<String> commandLine, IEnumerable<ExpectedArgument> expectedArguments)
        {
            var inputArgs = commandLine.Select(InputArgument.Parse).Where(x => x != null).ToList();
            var parsedArgs = expectedArguments.Select(x => x.ParseFromInputArguments(inputArgs)).ToList();
            return parsedArgs.ToDictionary(x => x.Name);
        }
        //================================================================================
    }
}

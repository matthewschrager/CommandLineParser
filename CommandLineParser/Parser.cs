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
            var parsedArgs = new List<ParsedArgument>();
            var errors = new List<ArgumentParseException>();
            foreach (var expectedArg in expectedArguments)
            {
                try
                {
                    parsedArgs.Add(expectedArg.ParseFromInputArguments(inputArgs));
                }

                catch (ArgumentParseException e)
                {
                    errors.Add(e);
                }
            }

            if (errors.Any())
            {
                Console.WriteLine("Invalid command line:");
                Console.Write(String.Join("\n", errors.Select(x => x.FormattedErrorString())));
                Console.WriteLine();
                throw new ArgumentException("Invalid command line. See errors above.");
            }

            return parsedArgs.ToDictionary(x => x.Name);
        }
        //================================================================================
    }
}

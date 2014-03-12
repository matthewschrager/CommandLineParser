using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace CommandLineParser.Tests
{
    [TestFixture]
    public class Tests
    {
        //================================================================================
        [Test]
        public void BasicTest()
        {            
            var commandLine = new[] { "testKey=testValue", "testNumber=2" };
            var parsedArgs = FluentParser.Setup().WithString("testKey").WithNumber("testNumber", 2).Parse(commandLine);

            Assert.AreEqual("testValue", parsedArgs["testKey"].Value);
            Assert.AreEqual(2, parsedArgs["testNumber"].As<double>());
        }
        //================================================================================
        [Test]
        public void DefaultValues()
        {
            var commandLine = new List<String>();
            var parsedArgs = FluentParser.Setup().WithString("optionalKey", "defaultValue").Parse(commandLine);
            Assert.AreEqual("defaultValue", parsedArgs["optionalKey"].Value);

            Assert.Throws<ArgumentException>(() => parsedArgs = FluentParser.Setup().WithNumber("testNumber").Parse(commandLine));
        }
        //================================================================================
    }
}

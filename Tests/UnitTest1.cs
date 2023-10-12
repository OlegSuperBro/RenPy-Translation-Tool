using Core.Models;
using System.Text.RegularExpressions;
using System.Xml;

namespace Core.Parsing.Parsers.Tests
{
    [TestClass]
    public class AllFilesParserTests
    {
        const string RELATIVE_TEST_DIR = "TestData/InputFiles";
        readonly string TEST_DIR = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, RELATIVE_TEST_DIR);
        [TestMethod]
        public void ParseAllFiles_ShouldReturnParsedFiles()
        {
            string workingDir = Path.Combine(TEST_DIR, "RegexTestData");
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(Path.Combine(workingDir, "RegexTestData.xml"));

            XmlNodeList patternTests = xmlDoc.SelectNodes("/RegularExpressions/Expression");

            List<Regex> regexes = new List<Regex>();
            Dictionary<int, List<string>> expectedOutputs = new Dictionary<int, List<string>>();
            int index = 0;
            foreach (XmlNode patternTest in patternTests)
            {
                string pattern = patternTest.SelectSingleNode("Pattern").InnerText;
                regexes.Add(new Regex(pattern));

                XmlNodeList Outputs = patternTest.SelectNodes("ExpectedOutputs/Output");

                expectedOutputs[index] = new List<string>();

                foreach (XmlNode OutputNode in Outputs)
                {
                    expectedOutputs[index].Add(OutputNode.InnerText);
                }
                index++;
            }
            string testDirectory = Path.Combine(workingDir, "TestStrings");

            ParsedFile[] parsedFiles = AllFilesParser.ParseAllFiles(testDirectory, regexes);

            Assert.IsNotNull(parsedFiles);
            Assert.IsTrue(parsedFiles.Length > 0);
            for (int i = 0; i < parsedFiles.Length; i++)
            {
                Assert.IsTrue(parsedFiles[i].SequenceEqual(expectedOutputs[i]), $"Parsed not equal to expected. {i} {parsedFiles[i].FileName} {String.Join(", ", parsedFiles[i].Lines.Select(line => line.OriginalLine))} {String.Join(", ", expectedOutputs[i])}");
            }
            Console.WriteLine("All tests completed succesfully");
        }
    }
}
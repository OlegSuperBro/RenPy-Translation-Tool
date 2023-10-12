using Core.Models;
using System.Text.RegularExpressions;

namespace Core.Parsing.Parsers
{
    public class AllFilesParser
    {
        static public ParsedFile[] ParseAllFiles(string path, IEnumerable<Regex> regexes, string? extention = null)
        {
            List<ParsedFile> parsedFiles = new List<ParsedFile>();
            ParsedFile tmpParsedFile;

            foreach (var file in GetFilesInDirectoryAndSubdirectories(path))
            {
                if (extention == null || file.EndsWith(extention))
                {
                    tmpParsedFile = RegexFileParser.ParseFile(file, regexes);
                    if (tmpParsedFile.Lines.Count > 0)
                    {
                        parsedFiles.Add(tmpParsedFile);
                    }
                }
            }
            return parsedFiles.ToArray();
        }
        static public string[] GetFilesInDirectoryAndSubdirectories(string directoryPath)
        {
            return Directory.GetFiles(directoryPath, "*", SearchOption.AllDirectories);
        }
    }
}

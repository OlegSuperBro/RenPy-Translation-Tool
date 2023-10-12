using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Core.Parsing.Parsers
{
    public class RegexFileParser
    {
        static public ParsedFile ParseFile(string path, IEnumerable<Regex> regexes)
        {
            List<ParsedLine> lines = new List<ParsedLine>();

            if (!File.Exists(path))
            {
                throw new FileNotFoundException();
            }

            using (StreamReader reader = new StreamReader(path))
            {
                string? line;
                int index = 0;

                if (!regexes.Any())
                {
                    while ((line = reader.ReadLine()) != null)
                    {
                       lines.Add(new ParsedLine(index, line.TrimStart()));
                    }
                }
                else
                {
                    while ((line = reader.ReadLine()) != null)
                    {
                        if (MatchAnyRegex(line, regexes))
                        {
                            lines.Add(new ParsedLine(index, line.TrimStart()));
                        }
                        index++;
                    }
                }
            }
            return new ParsedFile(path, Path.GetFileName(path), lines);
        }

        static private bool MatchAnyRegex(string line, IEnumerable<Regex> regexes)
        {
            return regexes.Any(regex => regex.Match(line).Success);
        }
    }
}

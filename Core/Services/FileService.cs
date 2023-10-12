using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Models;

namespace Core.Services
{
    public class FileService
    {
        public static void ExportWorkspace(Workspace workspace, string dirPath)
        {
            foreach(ParsedFile file in workspace.Files)
            {
                ExportFile(file, dirPath, file.OriginalPath);
            }
        }

        public static void ExportFile(ParsedFile file, string dirPath, IEnumerable<string> originalLines)
        {
            string[] linesArray = originalLines.ToArray();
            foreach (ParsedLine parsedLine in file.Lines)
            {
                linesArray[parsedLine.LineNumber] = linesArray[parsedLine.LineNumber].Replace(parsedLine.OriginalLine, parsedLine.TranslatedLine);
            }
            using (StreamWriter fileWriter = new StreamWriter(Path.Combine(dirPath, file.FileName)))
            {
                foreach (string line in linesArray)
                {
                    fileWriter.WriteLine(line);
                }
            }
        }
        public static void ExportFile(ParsedFile file, string dirPath, StreamReader originalFile)
        {
            List<string> lines = new List<string>();
            using (StreamReader originalFileReader = originalFile)
            {
                while (!originalFileReader.EndOfStream)
                {
                    lines.Add(originalFileReader.ReadLine());
                }
            }
            ExportFile(file, dirPath, lines);
        }

        public static void ExportFile(ParsedFile file, string dirPath, string originalFilePath)
        {
            using (StreamReader originalFile = new StreamReader(originalFilePath))
            {
                ExportFile(file, dirPath, originalFile);
            }
        }
    }
}

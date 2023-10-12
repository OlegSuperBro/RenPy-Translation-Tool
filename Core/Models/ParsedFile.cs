using System.Xml;
using System.Xml.Serialization;

namespace Core.Models
{
    [Serializable]
    [XmlRoot("ParsedFile")]
    public class ParsedFile /*IXmlSerializable*/
    {
        [XmlElement("OriginalPath")]
        public string? OriginalPath;
        [XmlElement("FileName")]
        public string? FileName;

        [XmlArray("Lines")]
        [XmlArrayItem("Line")]
        public List<ParsedLine> Lines;

        /*public void WriteXml(XmlWriter writer)
        {
            writer.WriteStartElement("ParsedFile");
            writer.WriteElementString("OriginalPath", OriginalPath);
            writer.WriteElementString("FileName", FileName);
            var serializer = new XmlSerializer(typeof(List<ParsedLine>));
            serializer.Serialize(writer, Lines);
            writer.WriteEndElement();
        }

        public void ReadXml(XmlReader reader)
        {
            reader.ReadStartElement("Workspace");
            OriginalPath = reader.ReadElementString("OriginalDirectory");
            FileName = reader.ReadElementString("Description");
            Lines = (List<ParsedLine>)new XmlSerializer(typeof(List<ParsedLine>)).Deserialize(reader);
        }

        public XmlSchema? GetSchema()
        {
            return null;
        }*/

        public ParsedFile()
        {
            Lines = new List<ParsedLine>();
        }

        public ParsedFile(string? originalPath, string? fileName, List<ParsedLine> lines)
        {
            this.OriginalPath = originalPath;
            this.FileName = fileName;
            this.Lines = lines;
        }
        public bool SequenceEqual(ParsedFile other)
        {
            return this == other;
        }

        public bool SequenceEqual(IEnumerable<string> other)
        {
            return this.Lines.Select(line => line.OriginalLine).SequenceEqual(other);
        }
    }
}

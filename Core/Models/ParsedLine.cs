using System.Xml;
using System.Xml.Serialization;

namespace Core.Models
{
    [Serializable]
    [XmlRoot("ParsedLine")]
    public class ParsedLine /*IXmlSerializable*/
    {
        [XmlElement("LineNumber")]
        public int LineNumber;
        [XmlElement("OriginalLine")]
        public string OriginalLine;
        [XmlElement("TranslatedLine")]
        public string? TranslatedLine;
        [XmlElement("CommentLine")]
        public string? CommentLine;

        /*public void WriteXml(XmlWriter writer)
        {
            writer.WriteStartElement("ParsedLine");
            writer.WriteElementString("LineNumber", LineNumber.ToString());
            writer.WriteElementString("OriginalLine", OriginalLine);
            writer.WriteElementString("TranslatedLine", TranslatedLine);
            writer.WriteElementString("CommentLine", CommentLine);
            writer.WriteEndElement();
        }

        public void ReadXml(XmlReader reader)
        {
            reader.ReadStartElement("ParsedLine");
            LineNumber = Convert.ToInt32(reader.ReadElementString("LineNumber"));
            OriginalLine = reader.ReadElementString("OriginalLine");
            TranslatedLine = reader.ReadElementString("TranslatedLine");
            CommentLine = reader.ReadElementString("CommentLine");
        }

        public XmlSchema? GetSchema()
        {
            return null;
        }*/

        public ParsedLine()
        {
            LineNumber = 0;
            OriginalLine = "";
        }

        public ParsedLine(int lineNumber, string originalLine, string? translatedLine = null, string? commentLine = null)
        {
            this.LineNumber = lineNumber;
            this.OriginalLine = originalLine;
            this.TranslatedLine = translatedLine;
            this.CommentLine = commentLine;
        }
    }
}

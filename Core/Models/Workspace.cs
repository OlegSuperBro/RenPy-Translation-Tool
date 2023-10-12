using System.Xml;
using System.Xml.Serialization;

namespace Core.Models
{
    [Serializable]
    [XmlRoot("Workspace")]
    public class Workspace /*IXmlSerializable*/
    {
        [XmlElement("Name")]
        public string Name;
        [XmlElement("OriginalDirecotry")]
        public string OriginalDirectory;
        [XmlElement("Description")]
        public string? Description;
        [XmlArray("Files")]
        [XmlArrayItem("File")]
        public List<ParsedFile>? Files;

        /*public void WriteXml(XmlWriter writer)
        {
            writer.WriteStartElement("Workspace");
            writer.WriteElementString("Name", Name);
            writer.WriteElementString("OriginalDirectory", OriginalDirectory);
            writer.WriteElementString("Description", Description);  
            if (Files != null && Files.Count > 0)
            {
                var serializer = new XmlSerializer(typeof(List<ParsedFile>));
                serializer.Serialize(writer, Files);
            }
            writer.WriteEndElement();
        }

        public void ReadXml(XmlReader reader)
        {
            reader.ReadStartElement("Workspace");
            Name = reader.ReadElementString("Name");
            OriginalDirectory = reader.ReadElementString("OriginalDirectory");
            Description = reader.ReadElementString("Description");
            Files = (List<ParsedFile>?)new XmlSerializer(typeof(List<ParsedFile>)).Deserialize(reader);
        }

        public XmlSchema? GetSchema()
        {
            return null;
        }*/

        public Workspace()
        {
            Name = "";
            OriginalDirectory = "";
        }

        public Workspace(string name, string originalDirectory, string? description = null, List<ParsedFile>? files = null)
        {
            this.Name = name;
            this.Description = description;
            this.OriginalDirectory = originalDirectory;
            this.Files = files;
        }

        public List<string>? GetFileNames()
        {
            if (this.Files == null)
            {
                return null;
            }
            return this.Files.Select(x => x.FileName).ToList();
        }
    }
}

using System.Xml.Serialization;

namespace Core.Parsing.Serialization
{
    public class XmlSerializer<T>
    {
        private readonly string filePath;
        public XmlSerializer(string path)
        {
            this.filePath = path;
        }

        public void Serialize(T obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException(nameof(obj), "Object to serialize cannot be null.");
            }
            try
            {
                using (StreamWriter writer = new StreamWriter(filePath))
                {
                    var serializer = new XmlSerializer(typeof(T));
                    serializer.Serialize(writer, obj);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Serialization error: {ex.Message}");
            }
        }

        public T? Deserialize()
        {
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException("XML file not found.", filePath);
            }

            try
            {
                using (var reader = new StreamReader(filePath))
                {
                    var serializer = new XmlSerializer(typeof(T));
                    return (T?)serializer.Deserialize(reader);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Deserialization error: {ex.Message}");
                return default(T?);
            }
        }
    }
}

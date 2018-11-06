using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace ViewModel.XML
{
    public static class XMLDeserialize
    {
        public static T XmlDeserializer<T>(string path)
        {
            T content = default(T);
            XmlSerializer serializer = new XmlSerializer(typeof(T));
            using (FileStream fileStream = new FileStream(path, FileMode.Open, FileAccess.Read))
            using (XmlReader reader = XmlReader.Create(fileStream))
            {
                content = (T)serializer.Deserialize(reader);
            }
            return content;
        }
    }
}

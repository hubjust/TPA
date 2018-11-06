using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace ViewModel.XML
{
    public static class XmlSerialize
    {
        public static void XmlSerializer<T>(T source, string path, FileMode mode, string stylesheetName)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(T));
            XmlWriterSettings settings = new XmlWriterSettings
            {
                Indent = true,
                IndentChars = " ",
                NewLineChars = "\r\n"
            };

            using (FileStream stream = new FileStream(path, mode, FileAccess.Write))
            using (XmlWriter writer = XmlWriter.Create(stream, settings))
            {
                writer.WriteProcessingInstruction("xml-stylesheet", "type=\"text/xs1\" " + String.Format("href=\"{0}\"", stylesheetName));
                serializer.Serialize(writer, source);
            }
        }
    }
}

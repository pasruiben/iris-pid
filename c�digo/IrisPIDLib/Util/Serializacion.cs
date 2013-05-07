using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml.Serialization;
using System.Xml;
using System.Runtime.Serialization;

namespace IrisPIDLib.Util
{
    public class Serializacion
    {

        public static void Serializar<T>(string fileName, T source)
        {
            using (XmlWriter writer = XmlWriter.Create(fileName, new XmlWriterSettings { Indent = true, OmitXmlDeclaration = true }))
            {
                XmlSerializer s = new XmlSerializer(typeof(T));
                XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
                ns.Add(String.Empty, String.Empty);
                s.Serialize(writer, source, ns);
            }
        }

        public static T DesSerializar<T>(string fileName)
        {
            using (FileStream reader = new FileStream(fileName, FileMode.Open))
            {
                XmlSerializer s = new XmlSerializer(typeof(T));
                return (T)s.Deserialize(reader);
            }
        }

    }
}

using System.IO;
using System.Xml.Serialization;

namespace Services.Utility.FireLocalization
{
    /// <summary>
    /// Fire object serializer for mobile and pc data chace.
    /// </summary>
    public static class LocalFireManager
    {
        public static string Serialize<T>(this T toSerialize)
        {
            XmlSerializer xml = new XmlSerializer(typeof(T));
            StringWriter writer = new StringWriter();
            xml.Serialize(writer, toSerialize);
            return writer.ToString();
        }

        public static T Deserialize<T>(this string id)
        {
            XmlSerializer xml = new XmlSerializer(typeof(T));
            StringReader reader = new StringReader(id);
            return (T)xml.Deserialize(reader);
        }
    }
}

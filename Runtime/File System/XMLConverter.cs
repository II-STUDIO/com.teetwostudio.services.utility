using System.IO;
using System.Xml.Serialization;

namespace Services.Utility.Convertion
{
    /// <summary>
    /// Fire string and object serializer for mobile and pc data chace.
    /// </summary>
    public static class XMLConverter
    {
        /// <summary>
        /// Serialize onject to string with XML serializer.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="toSerialize"></param>
        /// <returns>String data value</returns>
        public static string XMLSerialize<T>(this T toSerialize)
        {
            XmlSerializer xml = new XmlSerializer(typeof(T));
            StringWriter writer = new StringWriter();
            xml.Serialize(writer, toSerialize);
            return writer.ToString();
        }

        /// <summary>
        /// Deserialize strong to object with XML serializer.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id"></param>
        /// <returns>Object that deserialized</returns>
        public static T XMLDeserialize<T>(this string id)
        {
            XmlSerializer xml = new XmlSerializer(typeof(T));
            StringReader reader = new StringReader(id);
            return (T)xml.Deserialize(reader);
        }
    }
}

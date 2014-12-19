using System.IO;
using System.Xml.Serialization;

namespace DimensionalModelETL.Configuration
{
    /// <summary>
    /// Converts settings.xml into a CLR object
    /// </summary>
    public class SettingsLoader
    {
        /// <summary>
        /// Deserialize the XML object
        /// </summary>
        public static Settings LoadSettings(string settingsURI)
        {
            XmlSerializer deserializer = new XmlSerializer(typeof(Settings));

            using (FileStream stream = new FileStream(settingsURI, FileMode.Open))
            {
                return (Settings)deserializer.Deserialize(stream);
            }
        }


        /// <summary>
        /// Converts CLR object into XML
        /// </summary>
        public static void SaveSettings(string settingsURI, Settings settings)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(Settings));

            using (var writer = new StreamWriter(settingsURI))
            {
                serializer.Serialize(writer, settings);
            }
        }
    }
}

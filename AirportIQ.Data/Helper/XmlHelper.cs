using System;
using System.Text;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace AirportIQ.Data.Helpers
{
  /// <summary>
  /// Summary description for XmlHelper
  /// </summary>
  public static class XmlHelper
  {
    public static string Serialize<T>(T value)
    {
      string result = string.Empty;
      if (value == null)
      {
        return null;
      }

      XmlSerializer serializer = new XmlSerializer(typeof(T));

      XmlWriterSettings settings = new XmlWriterSettings();
      settings.Encoding = new UnicodeEncoding(false, false); // no BOM in a .NET string
      settings.Indent = false;
      settings.OmitXmlDeclaration = true;
      settings.CheckCharacters = true;      

      using (StringWriter textWriter = new StringWriter())
      {
        using (XmlWriter xmlWriter = XmlWriter.Create(textWriter, settings))
        {
          serializer.Serialize(xmlWriter, value);
        }
        result = textWriter.ToString();
        result = result.Replace("''", "'"); // unescape 
        result = result.Replace("'", "''"); // escape

      }

      return result;
    }

    public static T Deserialize<T>(string xml)
    {
      if (string.IsNullOrEmpty(xml))
      {
        return default(T);
      }

      XmlSerializer serializer = new XmlSerializer(typeof(T));

      XmlReaderSettings settings = new XmlReaderSettings();

      // No settings need modifying here

      using (StringReader textReader = new StringReader(xml))
      {
        using (XmlReader xmlReader = XmlReader.Create(textReader, settings))
        {
          return (T)serializer.Deserialize(xmlReader);
        }
      }
    }
  }
}
using System;
using System.IO;
using System.Net;
using System.Text;
using System.Xml.Serialization;

namespace Bit.BitLib.ExtensionMethods
{
    internal class Xml
    {
        /// <summary>
        /// Convert an object to XML as String
        /// </summary>
        /// <typeparam name="T">Object Type</typeparam>
        /// <param name="obj">Objetct to convert</param>
        /// <returns>Object as XML</returns>
        public static string AsXml<T>(this T obj)
        {
            String ret = String.Empty;

            try
            {
                XmlSerializerNamespaces xsn = new XmlSerializerNamespaces();
                xsn.Add("", "");

                var returnString = new StringBuilder();
                TextWriter writer = new StringWriter(returnString);
                XmlSerializer serializer = new XmlSerializer(typeof(T));
                serializer.Serialize(writer, obj, xsn);
                return returnString.ToString();
            }
            catch (Exception e)
            {
                obj.ExceptionHandle(m_LogLevel, m_LogHandle, e);
            }

            return ret;
        }

        /// <summary>
        /// Convert an object to XML and save as file
        /// </summary>
        /// <typeparam name="T">Object Type</typeparam>
        /// <param name="obj">Objetct to convert</param>
        /// <param name="filename">Path to the file to write to</param>
        /// <returns>Object as XML</returns>
        public static string WriteToXmlFile<T>(this T obj, FileInfo fileName)
        {
            try
            {
                XmlSerializerNamespaces xsn = new XmlSerializerNamespaces();
                xsn.Add("", "");

                StreamWriter writer = new StreamWriter(fileName.FullName);
                XmlSerializer serializer = new XmlSerializer(typeof(T));
                serializer.Serialize(writer, obj, xsn);
                writer.Close();
            }
            catch (Exception e)
            {
                obj.ExceptionHandle(m_LogLevel, m_LogHandle, e);
            }

            return AsXml(obj);
        }

        /// <summary>
        /// Read XML from a file as Object
        /// </summary>
        /// <typeparam name="T">Object Type</typeparam>
        /// <param name="obj">Objetct to convert</param>
        /// <param name="filename">Path to the file to read</param>
        /// <returns>XML as Object</returns>
        public static T ReadFromXml<T>(this T obj, FileInfo fileName)
        {
            try
            {
                StreamReader reader = new StreamReader(fileName.FullName);
                obj = ReadFromXml<T>(obj, reader);
            }
            catch (Exception e)
            {
                obj.ExceptionHandle(m_LogLevel, m_LogHandle, e);
            }

            return obj;
        }

        public static T ReadFromXml<T>(this T obj, Uri uri)
        {
            try
            {
                WebResponse response = WebRequest.Create(uri).GetResponse();
                StreamReader reader = new StreamReader(response.GetResponseStream());
                obj = ReadFromXml<T>(obj, reader);
            }
            catch (Exception e)
            {
                obj.ExceptionHandle(m_LogLevel, m_LogHandle, e);
            }

            return obj;
        }

        public static T ReadFromXml<T>(this T obj, StreamReader reader)
        {
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(T));
                T xml = (T)serializer.Deserialize(reader);
                reader.Close();
                obj = xml;
            }
            catch (Exception e)
            {
                obj.ExceptionHandle(m_LogLevel, m_LogHandle, e);
            }

            return obj;
        }
    }
}
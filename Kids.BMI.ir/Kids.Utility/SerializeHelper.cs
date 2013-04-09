using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Xml.Serialization;

namespace Kids.Utility
{
    public static class SerializeHelper
    {
        public static string DataContract_ToString<T>(T graph)
        {

            var stream = new MemoryStream();

            var serializer = new DataContractSerializer(typeof(T));

            serializer.WriteObject(stream, graph);

            stream.Position = 0;
            var streamReader = new StreamReader(stream);

            return streamReader.ReadToEnd();

        }

        public static T DataContract_ToObject<T>(string str)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(str);

            MemoryStream stream = new MemoryStream(bytes) { Position = 0 };

            var serializer = new DataContractSerializer(typeof(T));

            return (T)serializer.ReadObject(stream);

        }


        public static string Xml_ToString<T>(T obj)
        {
            XmlSerializer xs;
            StringWriter sw = null;
            try
            {
                xs = new XmlSerializer(typeof(T));
                sw = new StringWriter();
                xs.Serialize(sw, obj);
                sw.Flush();
                return sw.ToString();
            }
            finally
            {
                if (sw != null)
                {
                    sw.Close();
                    sw.Dispose();
                }
            }
        }


        public static T Xml_ToObject<T>(string XML)
        {
            if (string.IsNullOrEmpty(XML))
                return default(T);
            XmlSerializer xs;
            StringReader sr = null;
            try
            {
                xs = new XmlSerializer(typeof(T));
                sr = new StringReader(XML);
                return (T)xs.Deserialize(sr);
            }
            finally
            {
                if (sr != null)
                {
                    sr.Close();
                    sr.Dispose();
                }
            }
        }

        public static byte[] Binary_ToByte<T>(T obj)
        {
            BinaryFormatter bf = new BinaryFormatter();
            MemoryStream m = new MemoryStream();
            bf.Serialize(m, obj);
            return m.ToArray();
        }

        public static T Binary_ToObject<T>(byte[] obj)
        {
            MemoryStream m = new MemoryStream(obj);
            BinaryFormatter bf = new BinaryFormatter();
            return (T)bf.Deserialize(m);
        }



    }


}
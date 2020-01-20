using System.Diagnostics;
using System.IO;
using System.Runtime.Serialization;
using System.Text;
using System.Xml.Serialization;
using Framework;

namespace System
{
    /// <summary>
    /// Additional extensions on <see cref="System.Object"/>
    /// </summary>
    [DebuggerStepThrough]
    public static class ObjectExtensions
    {
        /// <summary>
        /// Indicates whether this instance is not a null value
        /// </summary>
        public static bool IsNotNull(this object value)
        {
            return value != null;
        }

        /// <summary>
        /// Indicates whether this instance is a null value
        /// </summary>
        public static bool IsNull(this object value)
        {
            return value == null;
        }

        /// <summary>
        /// Returns the specified property value
        /// </summary>
        public static object GetValue(this object instance, string propertyName)
        {
            Guard.ArgumentNotNull(instance, "instance");
            Guard.ArgumentNotEmpty(propertyName, "propertyName");

            var propertyInfo = instance.GetType().GetProperty(propertyName);

            return propertyInfo.GetValue(instance, null);
        }

        /// <summary>
        /// Returns the specified property value typed to T
        /// </summary>
        public static T GetValue<T>(this object instance, string propertyName) where T : class
        {
            var value = GetValue(instance, propertyName);
            return value as T ?? default(T);
        }

        /// <summary>
        /// Returns a serialized object in XML string
        /// </summary>
        /// <param name="instance"></param>
        /// <returns></returns>
        [Obsolete("Rather use AsXML extension")]
        public static string Serialize(this object instance)
        {
            if (instance.IsNull())
            {
                return String.Empty;
            }
            var serializer = new DataContractSerializer(instance.GetType());
            var text = string.Empty;
            using (var memoryStream = new MemoryStream())
            {
                serializer.WriteObject(memoryStream, instance);
                byte[] data = new byte[memoryStream.Length];
                Array.Copy(memoryStream.GetBuffer(), data, data.Length);
                text = Encoding.UTF8.GetString(data, 0, data.Length);
            }
            return text;
        }
                
        /// <summary>
        /// Returns a serialized object in XML string
        /// </summary>
        /// <param name="instance"></param>
        /// <returns></returns>
        public static string AsXML(this object instance)
        {
            return SerializeToXml(instance);
        }

        private static string SerializeToXml(object instance)
        {
            if (instance.IsNull())
            {
                return String.Empty;
            }

            var xml = string.Empty;

            using (var ms = new MemoryStream())
            {

                if (Attribute.IsDefined(instance.GetType(), typeof(DataContractAttribute)))
                {
                    new DataContractSerializer(instance.GetType()).WriteObject(ms, instance);
                }
                else
                {
                    new XmlSerializer(instance.GetType()).Serialize(ms, instance);
                }

                var data = new byte[ms.Length];
                Array.Copy(ms.GetBuffer(), data, data.Length);
                xml = Encoding.UTF8.GetString(data, 0, data.Length);
            }

            return xml;
        }


        /// <summary>
        /// Creates a copy of the object
        /// </summary>
        public static T Copy<T>(this T value) where T : class
        {
            var serializer = new DataContractSerializer(typeof(T));

            var instance = default(T);

            using (var stream = new MemoryStream())
            {

                serializer.WriteObject(stream, value);
                stream.Position = 0;

                instance = serializer.ReadObject(stream) as T;
            }

            return instance;
        }
    }
}

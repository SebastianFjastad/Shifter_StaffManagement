using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;

namespace Framework.Serialization
{
    public static class JsonSerialization
    {
        public static string JsonSerialize<T>(T instance) where T : class
        {
            using (var ms = new MemoryStream())
            {
                var ser = new DataContractJsonSerializer(typeof(T));
                ser.WriteObject(ms, instance);

                return Encoding.UTF8.GetString(ms.ToArray());
            }
        }

      
        public static T JsonDeserialize<T>(string value) where T : class
        {
            using (var ms = new MemoryStream(Encoding.UTF8.GetBytes(value)))
            {
                var ser = new DataContractJsonSerializer(typeof(T));
                T obj = (T)ser.ReadObject(ms);
                return obj;
            }
        }
    }
}

using App1.Entities;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace App1.Extensions
{
    public static class StringExtensions
    {
        public static string RemoveExtension(this string value)
        {
            var res = value.Replace(".jpeg", string.Empty);
            res = res.Replace(".jpg", string.Empty);
            return res;
        }

        public static string[] GetFolders(this string path)
        {
            return path.Split('/');
        }

        public static T ToObject<T>(this string xml) where T : SerializableObject
        {
            T res = null;
            var jobject = JsonConvert.DeserializeObject(xml) as JObject;
            if (jobject != null)
            {
                res = jobject.ToObject<T>();
            }

            return res;
        }
    }
}
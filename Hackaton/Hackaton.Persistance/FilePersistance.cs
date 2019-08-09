using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;

namespace Hackaton.Persistance
{
    public class FilePersistance
    {
        public static bool FileExists(string path)
        {
            return File.Exists(path);
        }

        public static bool CanWrite(string path)
        {
            try
            {
                System.Security.AccessControl.DirectorySecurity ds = Directory.GetAccessControl(path);
                return true;
            }
            catch (System.Exception ex)
            {
                return false;
            }
        }

        public static HashSet<T> LoadJson<T>(string path)
        {
            using (StreamReader r = new StreamReader(path))
            {
                var serializer = new JsonSerializer();
                using (var jsonTextReader = new JsonTextReader(r))
                {
                    return serializer.Deserialize<HashSet<T>>(jsonTextReader); //34.344 ms
                }
            }
        }

        public static void WriteJsonToFile<T>(HashSet<T> obj, string path)
        {
            using (StreamWriter file = File.CreateText(path))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(file, obj);
            }
        }


    }
}

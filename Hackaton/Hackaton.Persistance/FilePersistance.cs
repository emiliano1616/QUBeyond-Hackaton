using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;

namespace Hackaton.Persistance
{
    public class FilePersistance
    {

        public static List<T> LoadJson<T>(string path)
        {
            using (StreamReader r = new StreamReader(path))
            {
                var serializer = new JsonSerializer();
                using (var jsonTextReader = new JsonTextReader(r))
                {
                    return serializer.Deserialize<List<T>>(jsonTextReader); //34.344 ms
                }
            }
        }

        public static void WriteJsonToFile<T>(List<T> obj, string path)
        {
            using (StreamWriter file = File.CreateText(path))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(file, obj);
            }
        }


    }
}

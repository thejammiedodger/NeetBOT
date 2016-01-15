using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace NeetBOT.NB_Classes
{
    class JSON
    {
        public static JsonSerializer serializer = new JsonSerializer(); // Everyone use the same damn serializer.

        // Serializes settings to file.
        public static void Serialize(object settingsToSerialize, string resource)
        {
            serializer.NullValueHandling = NullValueHandling.Ignore;
            using (StreamWriter sw = new StreamWriter(resource))
            using (JsonWriter writer = new JsonTextWriter(sw))
            {
                serializer.Serialize(writer, settingsToSerialize);
            }
        }
       
        public static List<string> Deserialise(string resource)
        {
            List<string> tempList = new List<string>();
            Console.WriteLine(resource);
            var assembly = Assembly.GetExecutingAssembly();

            serializer.NullValueHandling = NullValueHandling.Ignore;
            using (Stream stream = assembly.GetManifestResourceStream(resource))
            using (StreamReader sr = new StreamReader(stream))
            using (JsonTextReader jr = new JsonTextReader(sr))
            {
                tempList = serializer.Deserialize<List<string>>(jr);
            }
            return tempList;
        }
    }
    public class LocalResources
    {
        public static string challenges = "NeetBOT.Resources.challenges.txt";
    }
}

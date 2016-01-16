using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
             
        public static void Serialize(object settingsToSerialize, string resource)
        {
            serializer.NullValueHandling = NullValueHandling.Ignore;
            using (StreamWriter sw = new StreamWriter(resource))
            using (JsonWriter writer = new JsonTextWriter(sw))
            {
                serializer.Serialize(writer, settingsToSerialize);
            }
        }
       
        public static ObservableCollection<string> Deserialise(string resource)
        {
            ObservableCollection<string> tempList = new ObservableCollection<string>();          
            serializer.NullValueHandling = NullValueHandling.Ignore;
            
            using (StreamReader sr = new StreamReader(resource))
            using (JsonTextReader jr = new JsonTextReader(sr))
            {
                tempList = serializer.Deserialize<ObservableCollection<string>>(jr);
            }
            return tempList;
        }

        #region Insult Data Serialisation
        public static InsultData DeserialiseInsultData()
        {
            InsultData tempInsultData = new InsultData();            
            
            serializer.NullValueHandling = NullValueHandling.Ignore;
            using (StreamReader sr = new StreamReader(LocalData.insults))
            using (JsonTextReader jr = new JsonTextReader(sr))
            {
                tempInsultData = serializer.Deserialize<InsultData>(jr);
            }
            return tempInsultData;
        }
        public static void SerializeInsultData(InsultData insultdata)
        {                      
            serializer.NullValueHandling = NullValueHandling.Ignore;            
            using (StreamWriter sw = new StreamWriter(LocalData.insults))
            using (JsonWriter writer = new JsonTextWriter(sw))
            {
                serializer.Serialize(writer, insultdata);
            }
        }
        #endregion
    }
    public class LocalResources
    {
        public static string challenges = "NeetBOT.Resources.challenges.txt";
        public static string insults = "NeetBOT.Resources.InsultData";
    }

    public class LocalData
    {
        public static string AppDataFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "NeetBOT"); // Local Appdata location.
        public static string challenges = Path.Combine(AppDataFolder, "ChallengeData");
        public static string insults = Path.Combine(AppDataFolder, "InsultData");
        public static string stats = Path.Combine(AppDataFolder, "NeetBOT_Stats");
    }
}

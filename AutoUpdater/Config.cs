using Hjson;
using Newtonsoft.Json;
using System;

namespace AutoUpdater
{
    public class Config
    {
        private static readonly string s_Path = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "config.hjson");

        public static Config Instance { get; private set; }

        public static void Load()
        {
            if (!System.IO.File.Exists(s_Path))
            {
                Instance = new Config();
                Instance.Save();
                return;
            }

            using (var fs = new System.IO.FileStream(s_Path, System.IO.FileMode.Open, System.IO.FileAccess.Read))
            {
                Instance = JsonConvert.DeserializeObject<Config>(HjsonValue.Load(fs).ToString(Stringify.Plain));
            }
        }

        static Config()
        {
            Load();
        }

        public void Save()
        {
            string json = JsonConvert.SerializeObject(this, Formatting.None);
            System.IO.File.WriteAllText(s_Path, JsonValue.Parse(json).ToString(Stringify.Hjson));
        }

        public Config()
        {
            AutoStart = true;
            StartMinimized = true;
            UpdateInterval = 2;
        }

        [JsonProperty("auto_start")]
        public bool AutoStart { get; set; }

        [JsonProperty("start_minimized")]
        public bool StartMinimized { get; set; }

        [JsonProperty("update_interval")]
        public uint UpdateInterval { get; set; }
    }
}

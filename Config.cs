using System.IO;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using ConsoleApp1.Modules.Core;
using ConsoleApp1.Interfaces.Core;
using ConsoleApp1.Interfaces.Core.Database;

namespace ConsoleApp1
{
    public static class Config
    {
        private class Fields : IConfig
        {
            /* 
             * Implementation of IDatabase for a PostgreSQL connection
             */
            private class PostgreSQL : IDatabase
            {
                public string ip { get; set; }
                public int port { get; set; }
                public string user { get; set; }
                public string password { get; set; }

                public PostgreSQL()
                {
                    ip = "127.0.0.1";
                    port = 5432;
                    user = "";
                    password = "";
                }
            }

            /* 
             * Implementation of IConfig interface for Fields class
             */
            public string discordToken { get; set; }
            public IDatabase postgresql { get; set; }

            /* 
             * Initializes Fields() class with initial values so the
             * config file doesn't come up with null values
             */
            public Fields()
            {
                discordToken = "";
                postgresql = new PostgreSQL();
            }
        }

        public static bool Exists()
        {
            return File.Exists("config.json");
        }

        /* 
         * Initializes a new instance of the Fields class using the
         * IConfig interface with a simpleton pattern
         *
         * The get property allows public read access to the Fields()
         * class while keeping write access private
         *
         * This makes sure the config isn't tampered with during runtime
         * and allows for storage in memory, which in turn, reduces IO load
         */
        private static IConfig config = new Fields();
        public static IConfig get
        {
            get { return config; }
        }

        /* 
         * Loads the config file into memory
         */
        private static void Load()
        {
            using (StreamReader file = new StreamReader("config.json"))
            {
                config = JsonConvert.DeserializeObject<Fields>(file.ReadToEnd());
            }
        }

        /* 
         * Makes a new config using the initialized Fields() class and its
         * predefined values in a JSON structure
         */
        private static void Make()
        {
            using (StreamWriter file = new StreamWriter("config.json"))
            {
                file.Write(JValue.Parse(JsonConvert.SerializeObject(config)).ToString(Formatting.Indented));
            }
        }

        /* 
         * Initializes the Config() class, makes a new file if needed and
         * makes sure it's loaded into memory
         */
        static Config()
        {
            if (!Exists())
            {
                Make();
                Logger.Log("config.json not found! Making a fresh config.", Logger.Level.INFO);
            }

            Load();
            Logger.Log("Loaded config.json", Logger.Level.INFO);
        }
    }
}

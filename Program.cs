using System;
using System.Threading.Tasks;

using DSharpPlus;
using DSharpPlus.CommandsNext;

using ConsoleApp1.Modules.Core;
using ConsoleApp1.Modules.Core.Database;
using Npgsql;

namespace ConsoleApp1
{
    class Program
    {
        public static DiscordClient Discord;
        public static CommandsNextModule CommandsNext;

        public static void Main(string[] args)
        {
            if (Config.get.discordToken != "")
            {
                var config = new DiscordConfiguration
                {
                    Token = Config.get.discordToken,
                    TokenType = TokenType.Bot
                };
                Run(config).ConfigureAwait(false).GetAwaiter().GetResult();
            }
            else
            {
                Logger.Log("Please set your Discord Token in the config.json file", Logger.Level.EXEP);
            }
        }

        /* 
         * Sets up the PostgreSQL Database connection string        
         */
        private static void DatabaseConnection()
        {
            NpgsqlConnectionStringBuilder postgresql = new NpgsqlConnectionStringBuilder();
            if (Config.get.postgresql.user != "" || Config.get.postgresql.password != "")
            {
                postgresql.Username = Config.get.postgresql.user;
                postgresql.Password = Config.get.postgresql.password;
                postgresql.Port = Config.get.postgresql.port;
                postgresql.Host = Config.get.postgresql.ip;

                Connection.Connect(postgresql);
            }
            else
            {
                Logger.Log("Please set your PostgreSQL credentials in the config.json file", Logger.Level.INFO);
                Environment.Exit(1);
            }
        }

        private static async Task Run(DiscordConfiguration config)
        {
            DatabaseConnection();

            Discord = new DiscordClient(config);
            await Discord.ConnectAsync();

            CommandsNext = Discord.UseCommandsNext(new CommandsNextConfiguration
            {
                StringPrefix = "!" // TODO: make customizible
            });

            /* 
             * Registers all the commands using the ICommand interface
             */
            Commands.Load();

            await Task.Delay(-1);
        }
    }
}

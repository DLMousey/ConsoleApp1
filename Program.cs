using System;
using System.Threading.Tasks;

using DSharpPlus;
using DSharpPlus.CommandsNext;

using ConsoleApp1.Modules.Core;

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

        private static async Task Run(DiscordConfiguration config)
        {
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

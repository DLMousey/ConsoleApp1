using System;
using System.Threading.Tasks;

using DSharpPlus;
using DSharpPlus.CommandsNext;

namespace ConsoleApp1
{
    class Program
    {
        public static DiscordClient Discord;
        public static CommandsNextModule CommandsNext;

        public static void Main(string[] args)
        {
        }

        private static async Task Run(DiscordConfiguration config)
        {
            Discord = new DiscordClient(config);
            await Discord.ConnectAsync();

            CommandsNext = Discord.UseCommandsNext(new CommandsNextConfiguration
            {
                StringPrefix = "!" // TODO: make customizible
            });

            await Task.Delay(-1);
        }
    }
}

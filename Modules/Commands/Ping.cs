using System.Threading.Tasks;

using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;

using ConsoleApp1.Interfaces.Core;
using ConsoleApp1.Modules.Core;

namespace ConsoleApp1.Modules.Commands
{
    public class Ping : ICommand
    {

        /* 
         * Implements the Command() function which is called when the
         * CommandsNext Module triggers it
         */
        [Command("ping")]
        public async Task Command(CommandContext ctx, string argument = "")
        {
            await ctx.RespondAsync("pong");
        }

        /* 
         * Registers this class with all its commands for use in the bot
         */
        public void Register()
        {
            Program.CommandsNext.RegisterCommands<Ping>();
            Logger.Log("Set up ping command!", Logger.Level.INFO);
        }
    }
}
using System.Threading.Tasks;

using DSharpPlus.CommandsNext;

using ConsoleApp1.Interfaces.Core;

namespace ConsoleApp1.Modules.Commands
{
    public abstract class BaseCommand : ICommand
    {

        /* 
         * Implements the Command() function which is called when the
         * CommandsNext Module triggers it
         */
        public virtual async Task Command(CommandContext ctx)
        {
            await Task.FromException(new System.NotImplementedException());
        }

        /* 
         * Registers this class with all its commands for use in the bot
         */
        public virtual void Register()
        {
            throw new System.NotImplementedException();
        }
    }
}
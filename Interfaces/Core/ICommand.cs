using System.Threading.Tasks;
using DSharpPlus.CommandsNext;

namespace ConsoleApp1.Interfaces.Core
{
    public interface ICommand
    {
        Task Command(CommandContext ctx);
        void Register();
    }
}
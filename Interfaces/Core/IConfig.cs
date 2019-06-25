using ConsoleApp1.Interfaces.Core.Database;

namespace ConsoleApp1.Interfaces.Core
{
    public interface IConfig
    {
        IDatabase postgresql { get; set; }
        string discordToken { get; set; }
    }
}
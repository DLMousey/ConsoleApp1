namespace ConsoleApp1.Interfaces.Core.Database
{
    public interface IDatabase
    {
        string ip { get; set; }
        int port { get; set; }
        string user { get; set; }
        string password { get; set; }
    }
}

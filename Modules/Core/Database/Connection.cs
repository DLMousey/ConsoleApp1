using Npgsql;

namespace ConsoleApp1.Modules.Core.Database
{
    public static class Connection
    {
        private static NpgsqlConnection connection;

        public static void Connect(NpgsqlConnectionStringBuilder credentials)
        {
            connection = new NpgsqlConnection(credentials.ToString());
            connection.Open();
        }

        public static NpgsqlConnection Get()
        {
            return connection;
        }
    }
}

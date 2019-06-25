using System;
using System.Collections.Generic;
using System.Dynamic;

using Npgsql;

using ConsoleApp1.Interfaces.Core.Database;

namespace ConsoleApp1.Modules.Core.Database
{
    public class Command<T> : IQuery<T>
    {
        public List<T> Query(string query)
        {
            List<T> resultset = new List<T>();

            using (NpgsqlCommand cmd = new NpgsqlCommand(query, Connection.Get()))
            using (Npgsql.NpgsqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    dynamic entry = new ExpandoObject();
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        Type type = reader.GetFieldType(i);
                        entry[reader.GetName(i)] = Convert.ChangeType(reader.GetFieldValue<Object>(i), type);
                    }
                    resultset.Add(entry);
                }
                return resultset;
            }
        }
    }
}

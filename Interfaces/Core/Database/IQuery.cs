using System.Collections.Generic;

namespace ConsoleApp1.Interfaces.Core.Database
{
    public interface IQuery<T>
    {
        List<T> Query(string query);
    }
}

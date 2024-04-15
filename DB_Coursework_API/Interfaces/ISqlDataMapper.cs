using System.Data.SqlClient;

namespace DB_Coursework_API.Interfaces
{
    public interface ISqlDataMapper<T>
    {
        T MapData(SqlDataReader reader);
    }
}

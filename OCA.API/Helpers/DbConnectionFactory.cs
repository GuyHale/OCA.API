using System.Data;
using System.Data.SqlClient;

namespace OCA.API.Helpers
{
    public static class DbConnectionFactory
    {
        public static IDbConnection CreateConnection(string connectionString)
        {
            return new SqlConnection(connectionString);
        }
    }
}

using System.Data.SqlClient;

namespace DB_Coursework_API.Extensions
{
    public static class SqlCommandExtensions
    {
        public static string GetDebugQueryText(this SqlCommand command)
        {
            string query = command.CommandText;

            foreach (SqlParameter p in command.Parameters)
            {
                string valueString;

                // Check if the parameter is of type string, datetime or similar that needs quotation marks around its value
                if (p.Value is string || p.Value is DateTime || p.Value is char)
                {
                    valueString = "'" + p.Value.ToString().Replace("'", "''") + "'"; // Handle single quotes for SQL compatibility
                }
                else if (p.Value is DBNull)
                {
                    valueString = "NULL";
                }
                else
                {
                    valueString = p.Value.ToString().Replace(",", ".");
                    // Replace comma with dot for invariant culture compatibility (important for floating point numbers)
                }

                // Replace the parameter placeholders in the command text
                query = query.Replace(p.ParameterName, valueString);
            }

            return query;
        }
    }
}

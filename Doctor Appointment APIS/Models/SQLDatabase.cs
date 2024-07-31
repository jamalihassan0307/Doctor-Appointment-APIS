using MySql.Data.MySqlClient;
using System;
using System.Data;


namespace Patient_Appointment_APIS.Models
{
    public class SQLDatabase
    {
        public static string ConnectionString = "Server=localhost;Database=DOASQL;Uid=root;Pwd=;";
        public static int ExecNonQuery(string Command)
        {
            MySqlConnection con = new MySqlConnection(ConnectionString);
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = con;
            cmd.CommandText = Command;
            cmd.CommandType = CommandType.Text;
            try
            {
                con.Open();
                if (cmd.ExecuteNonQuery() > 0)
                {
                    con.Close();
                    return 1;
                }
                else
                {
                    con.Close();
                    return 0;
                }

            }
            catch (Exception ex)
            {
                con.Close();
                throw ex;
            }

        }
        public static DataTable GetDataTable(string Query)
        {
            MySqlConnection con = new MySqlConnection(ConnectionString);
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = con;
            cmd.CommandText = Query;
            cmd.CommandType = CommandType.Text;

            MySqlDataAdapter da = new MySqlDataAdapter();

            DataTable tab = new DataTable();

            da.SelectCommand = cmd;
            try
            {
                da.Fill(tab);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return tab;
        }

        public static int ExecUpdate(string tableName, Dictionary<string, object> map, string id)
        {
            try
            {
                string query = $"UPDATE {tableName} SET ";
                List<string> columns = new List<string>();
                List<MySqlParameter> values = new List<MySqlParameter>();

                foreach (var entry in map)
                {
                    columns.Add($"{entry.Key} = @{entry.Key}");
                    values.Add(new MySqlParameter($"@{entry.Key}", entry.Value));
                }

                query += string.Join(", ", columns);
                query += " WHERE id = @id";
                values.Add(new MySqlParameter("@id", id));

                using (MySqlConnection con = new MySqlConnection(ConnectionString))
                {
                    using (MySqlCommand cmd = new MySqlCommand(query, con))
                    {
                        cmd.Parameters.AddRange(values.ToArray());
                        con.Open();
                        int result = cmd.ExecuteNonQuery();
                        con.Close();
                        return result;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to execute update query: {ex.Message}", ex);
            }
        }

    }
}
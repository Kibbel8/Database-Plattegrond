using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;

namespace Database_Plattegrond.DatabaseService
{
    public class TestDatabaseService
    {

        public void DoInsertQuery()
        {
            SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["ApplicatiePlattegrondConnectionString"].ToString());
            using (SqlCommand command = new SqlCommand("", connection))
            {
                connection.Open();
                command.CommandText = "insert into TestTable values(@ID, @Int, @Date)";
                command.Parameters.AddWithValue("@ID", "12445");
                command.Parameters.AddWithValue("@Int", 1);
                command.Parameters.AddWithValue("@Date", DateTime.Now);
                command.ExecuteNonQuery();
                //connection.
                connection.Close();
            }
        }

        public void DoGetQuery()
        {
            SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["ApplicatiePlattegrondConnectionString"].ToString());
            using (SqlCommand command = new SqlCommand("", connection))
            {
                connection.Open();

                command.CommandText = "select * from TestTable"; //where ID=@ID";
                command.Parameters.AddWithValue("@ID", "12445");
                SqlDataReader reader = command.ExecuteReader();
                List<string> strings = new List<string>();
                while (reader.Read())
                {
                    string Id = reader["ID"].ToString();
                    int Int = (int)reader["Int"];
                    strings.Add(Id);
                }
                connection.Close();
            }
        }
    }
}
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
                command.CommandText = "insert into dataset values(@ID, @naam, @beschrijving, @datum_aangemaakt, @link_open_data, @zoektermen, @eigenaar, @applicatie)";
                command.Parameters.AddWithValue("@ID", 1);
                command.Parameters.AddWithValue("@naam", "Kunst");
                command.Parameters.AddWithValue("@beschrijving", "Dataset over kunst");
                command.Parameters.AddWithValue("@datum_aangemaakt", DateTime.Now);
                command.Parameters.AddWithValue("@link_open_data", DBNull.Value);
                command.Parameters.AddWithValue("@zoektermen", "Kunst | Cultureel | Locatie");
                command.Parameters.AddWithValue("@eigenaar", "Bob");
                command.Parameters.AddWithValue("@applicatie", "Kunstnet");
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

                command.CommandText = "select * from dataset"; //where ID=@ID";
                SqlDataReader reader = command.ExecuteReader();
                List<string> strings = new List<string>();
                while (reader.Read())
                {
                    string Id = reader["ID"].ToString();
                    strings.Add(Id);
                }
                connection.Close();
            }
        }
    }
}
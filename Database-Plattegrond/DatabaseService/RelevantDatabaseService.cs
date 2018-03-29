using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Database_Plattegrond.Models;
using System.Data.SqlClient;
using System.Web.Configuration;

namespace Database_Plattegrond.DatabaseService
{
    public class RelevantDatabaseService
    {
        public List<Relevant> GetRelevanteLinksVoorDataset(int datasetID)
        {
            List<Relevant> links = new List<Relevant>();

            SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["ApplicatiePlattegrondConnectionString"].ToString());
            using (SqlCommand command = new SqlCommand("", connection))
            {
                connection.Open();
                command.CommandText = "SELECT * FROM Relevante_Link WHERE Dataset_ID = @Dataset_ID";

                command.Parameters.AddWithValue("@Dataset_ID", datasetID);

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Relevant link = new Relevant
                    {
                        Id = (int)reader["ID"],
                        Naam = reader["Naam"].ToString(),
                        Link = reader["Link"].ToString(),
                        DatasetID = (int)reader["ID"]
                    };
                    links.Add(link);
                }

                connection.Close();
                return links;
            }
        }

        public int InsertRelevanteLink(Relevant link)
        {
            SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["ApplicatiePlattegrondConnectionString"].ToString());
            using (SqlCommand command = new SqlCommand("", connection))
            {
                connection.Open();
                command.CommandText = "INSERT INTO comment (Naam, Link, Dataset_ID) VALUES (@Naam, @Link, @Dataset_ID);";

                command.Parameters.AddWithValue("@Naam", link.Naam);
                command.Parameters.AddWithValue("@Link", link.Link);
                command.Parameters.AddWithValue("@Dataset_ID", link.DatasetID);

                int rowsAffected = command.ExecuteNonQuery();
                connection.Close();

                return rowsAffected;
            }
        }

        public int UpdateRelevanteLink(Relevant link)
        {
            SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["ApplicatiePlattegrondConnectionString"].ToString());
            using (SqlCommand command = new SqlCommand("", connection))
            {
                connection.Open();
                command.CommandText = "UPDATE comment SET Naam = @Naam, Link = @Link WHERE ID = @ID;";

                command.Parameters.AddWithValue("@Naam", link.Naam);
                command.Parameters.AddWithValue("@Link", link.Link);
                command.Parameters.AddWithValue("@ID", link.Id);

                int rowsAffected = command.ExecuteNonQuery();
                connection.Close();

                return rowsAffected;
            }
        }
    }
}
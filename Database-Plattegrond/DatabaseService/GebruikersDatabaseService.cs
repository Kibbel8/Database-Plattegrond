using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Database_Plattegrond.Models;
using System.Data.SqlClient;
using System.Web.Configuration;

namespace Database_Plattegrond.DatabaseService
{
    public class GebruikersDatabaseService
    {
        public List<Gebruiker> GetAllGebruikers()
        {
            List<Gebruiker> gebruikers = new List<Gebruiker>();

            SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["ApplicatiePlattegrondConnectionString"].ToString());
            using (SqlCommand command = new SqlCommand("", connection))
            {
                connection.Open();
                command.CommandText = "SELECT * FROM Gebruiker";

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Gebruiker gebruiker = new Gebruiker
                    {
                        Naam = reader["Naam"].ToString(),
                        Email = reader["Email"].ToString(),
                        ID = (int)reader["ID"]
                    };
                    gebruikers.Add(gebruiker);
                }

                connection.Close();
                return gebruikers;
            }
        }

        public Gebruiker GetGebruikerFromID(int id)
        {
            SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["ApplicatiePlattegrondConnectionString"].ToString());
            using (SqlCommand command = new SqlCommand("", connection))
            {
                connection.Open();
                command.CommandText = "SELECT * FROM Gebruiker WHERE ID = @ID";
                command.Parameters.AddWithValue("@ID", id);
                
                SqlDataReader reader = command.ExecuteReader();

                Gebruiker gebruiker = new Gebruiker
                {
                    Naam = reader["Naam"].ToString(),
                    Email = reader["Email"].ToString(),
                    ID = (int)reader["ID"]
                };

                connection.Close();
                return gebruiker;
            }
        }

        public int InsertGebruiker(Gebruiker gebruiker)
        {
            SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["ApplicatiePlattegrondConnectionString"].ToString());
            using (SqlCommand command = new SqlCommand("", connection))
            {
                connection.Open();
                command.CommandText = "INSERT INTO gebruiker (Naam, Email) VALUES (@Naam, @Email);";
                
                if (gebruiker.Naam == null)
                    command.Parameters.AddWithValue("@Naam", DBNull.Value);
                else
                    command.Parameters.AddWithValue("@Naam", gebruiker.Naam);
                if (gebruiker.Email == null)
                    command.Parameters.AddWithValue("@Email", DBNull.Value);
                else
                    command.Parameters.AddWithValue("@Email", gebruiker.Email);

                int rowsAffected = command.ExecuteNonQuery();
                connection.Close();

                return rowsAffected;
            }
        }

        public int UpdateGebruiker(Gebruiker gebruiker)
        {
            SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["ApplicatiePlattegrondConnectionString"].ToString());
            using (SqlCommand command = new SqlCommand("", connection))
            {
                connection.Open();
                command.CommandText = "UPDATE Gebruiker SET Naam = @Naam, Email = @Email WHERE ID = @ID;";

                command.Parameters.AddWithValue("@Naam", gebruiker.Naam);
                command.Parameters.AddWithValue("@Email", gebruiker.Email);
                command.Parameters.AddWithValue("@ID", gebruiker.ID);

                int rowsAffected = command.ExecuteNonQuery();
                connection.Close();

                return rowsAffected;
            }
        }
    }
}
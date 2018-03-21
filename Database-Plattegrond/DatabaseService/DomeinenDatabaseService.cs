using Database_Plattegrond.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;

namespace Database_Plattegrond.DatabaseService
{
    public class DomeinenDatabaseService
    {
        public List<Domein> GetHoofdDomeinen()
        {
            List<Domein> domeinen = new List<Domein>();

            SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["ApplicatiePlattegrondConnectionString"].ToString());
            using (SqlCommand command = new SqlCommand("", connection))
            {
                connection.Open();
                command.CommandText = "SELECT Naam from Domein WHERE Sub_domein_van IS NULL";

                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Domein domein = new Domein { Naam = reader.GetString(0) };
                        domeinen.Add(domein);
                    }
                }

                connection.Close();
                return domeinen;
            }
        }

        public List<Domein> GetSubDomeinen(Domein domein)
        {
            List<Domein> subDomeinen = new List<Domein>();

            SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["ApplicatiePlattegrondConnectionString"].ToString());
            using (SqlCommand command = new SqlCommand("", connection))
            {
                connection.Open();
                command.CommandText = "SELECT Naam from Domein WHERE Sub_domein_van IS @domein";

                command.Parameters.AddWithValue("@domein", domein.Naam);

                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Domein subDomein = new Domein { Naam = reader.GetString(0) };
                        subDomeinen.Add(domein);
                    }
                }

                connection.Close();
                return subDomeinen;
            }
        }

        public int UpdateDomein(Domein domein)
        {
            SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["ApplicatiePlattegrondConnectionString"].ToString());
            using (SqlCommand command = new SqlCommand("", connection))
            {
                connection.Open();
                command.CommandText = "Update domein SET Naam = @Naam, Is_Subdomein_van = @Is_Subdomein_van WHERE Naam=@Naam";

                command.Parameters.AddWithValue("@Naam", domein.Naam);

                if (domein.SubdomeinVan == null)
                    command.Parameters.AddWithValue("@Is_Subdomein_van", DBNull.Value);
                else
                    command.Parameters.AddWithValue("@Is_Subdomein_van", domein.SubdomeinVan);

                int rowsAffected = command.ExecuteNonQuery();
                connection.Close();

                return rowsAffected;
            }
        }

        public int InsertDomein(Domein domein)
        {
            SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["ApplicatiePlattegrondConnectionString"].ToString());
            using (SqlCommand command = new SqlCommand("", connection))
            {
                connection.Open();
                command.CommandText = "INSERT INTO domein (Naam, Is_Subdomein_van) VALUES (@Naam, @Is_Subdomein_van)";

                command.Parameters.AddWithValue("@Naam", domein.Naam);

                if (domein.SubdomeinVan == null)
                    command.Parameters.AddWithValue("@Is_Subdomein_van", DBNull.Value);
                else
                    command.Parameters.AddWithValue("@Is_Subdomein_van", domein.SubdomeinVan);

                int rowsAffected = command.ExecuteNonQuery();
                connection.Close();

                return rowsAffected;
            }
        }

        public int DeleteDomein(Domein domein)
        {
            throw new NotImplementedException();
        }
    }
}
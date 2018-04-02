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
        public List<Domein> GetAlleDomeinen()
        {
            List<Domein> domeinen = new List<Domein>();

            SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["ApplicatiePlattegrondConnectionString"].ToString());
            using (SqlCommand command = new SqlCommand("", connection))
            {
                connection.Open();
                command.CommandText = "SELECT Naam from Domein"; //WHERE Naam != 'ongesorteerd'";

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Domein domein = new Domein { Naam = reader.GetString(0) };
                    domeinen.Add(domein);
                }

                connection.Close();
                return domeinen;
            }
        }

        public List<Domein> GetHoofdDomeinen()
        {
            List<Domein> domeinen = new List<Domein>();

            SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["ApplicatiePlattegrondConnectionString"].ToString());
            using (SqlCommand command = new SqlCommand("", connection))
            {
                connection.Open();
                command.CommandText = "SELECT Naam, Thumbnail, DATALENGTH(Thumbnail) as DATALENGTH  from Domein WHERE Is_Subdomein_van IS NULL";

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Domein domein = new Domein { Naam = reader.GetString(0) };

                    int dataLength = -1;
                    Int32.TryParse(reader["DATALENGTH"].ToString(), out dataLength);
                    if (dataLength > 0)
                    {
                        byte[] imageBytes = new byte[dataLength];
                        reader.GetBytes(1, 0, imageBytes, 0, dataLength);
                        domein.Image = imageBytes;
                    }

                    domeinen.Add(domein);
                }

                connection.Close();
                return domeinen;
            }
        }

        public List<Domein> GetSubDomeinen(string domeinNaam)
        {
            List<Domein> subDomeinen = new List<Domein>();

            SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["ApplicatiePlattegrondConnectionString"].ToString());
            using (SqlCommand command = new SqlCommand("", connection))
            {
                connection.Open();
                command.CommandText = "SELECT Naam from Domein, Thumbnail, DATALENGTH(Thumbnail) as DATALENGTH WHERE Is_Subdomein_van IS @domein";

                command.Parameters.AddWithValue("@domein", domeinNaam);

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Domein subDomein = new Domein { Naam = reader.GetString(0) };

                    int dataLength = -1;
                    Int32.TryParse(reader["DATALENGTH"].ToString(), out dataLength);
                    if (dataLength > 0)
                    {
                        byte[] imageBytes = new byte[dataLength];
                        reader.GetBytes(1, 0, imageBytes, 0, dataLength);
                        subDomein.Image = imageBytes;
                    }

                    subDomeinen.Add(subDomein);
                }

                connection.Close();
                return subDomeinen;
            }
        }

        public Domein GetDomeinFromNaam(string naam)
        {
            SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["ApplicatiePlattegrondConnectionString"].ToString());
            //SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["jeroen_vd_kolk.datacatalogus"].ToString());
            connection.Open();

            SqlCommand command = new SqlCommand("", connection)
            {
                CommandText = "select Naam, Is_Subdomein_van, Thumbnail, DATALENGTH(Thumbnail) as DATALENGTH from domein where Naam = @Naam;"
            };
            command.Parameters.AddWithValue("@Naam", naam);

            SqlDataReader reader = command.ExecuteReader();

            if (reader.Read())
            {
                Domein domein = new Domein
                {
                    Naam = reader["Naam"].ToString(),
                    SubdomeinVan = reader["Is_Subdomein_van"].ToString()
                };

                int dataLength = -1;
                Int32.TryParse(reader["DATALENGTH"].ToString(), out dataLength);
                if (dataLength > 0)
                {
                    byte[] imageBytes = new byte[dataLength];
                    reader.GetBytes(2, 0, imageBytes, 0, dataLength);
                    domein.Image = imageBytes;
                }

                connection.Close();
                return domein;
            }

            connection.Close();
            return new Domein();
        }

        public List<Domein> GetDomeinenVoorDataset(int datasetID)
        {
            List<Domein> domeinen = new List<Domein>();

            SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["ApplicatiePlattegrondConnectionString"].ToString());
            using (SqlCommand command = new SqlCommand("", connection))
            {
                connection.Open();
                command.CommandText = "select Naam FROM Domein d JOIN Dataset_Domein dd on d.Naam = dd.Domein_Naam where dd.Dataset_ID = @datasetID;";
                command.Parameters.AddWithValue("@datasetID", datasetID);

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Domein domein = new Domein { Naam = reader.GetString(0) };
                    domeinen.Add(domein);
                }

                connection.Close();
                return domeinen;
            }
        }

        public int UpdateDomein(Domein domein)
        {
            SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["ApplicatiePlattegrondConnectionString"].ToString());
            using (SqlCommand command = new SqlCommand("", connection))
            {
                connection.Open();
                command.CommandText = "Update domein SET Naam = @Naam, Is_Subdomein_van = @Is_Subdomein_van, Thumbnail = @Thumbnail WHERE Naam=@Naam";

                command.Parameters.AddWithValue("@Naam", domein.Naam);

                if (domein.SubdomeinVan == null)
                    command.Parameters.AddWithValue("@Is_Subdomein_van", DBNull.Value);
                else
                    command.Parameters.AddWithValue("@Is_Subdomein_van", domein.SubdomeinVan);

                if (domein.Image == null)
                    command.Parameters.AddWithValue("@Thumbnail", DBNull.Value);
                else
                    command.Parameters.AddWithValue("@Thumbnail", domein.Image);

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
                command.CommandText = "INSERT INTO domein (Naam, Is_Subdomein_van, Thumbnail) VALUES (@Naam, @Is_Subdomein_van, @Thumbnail)";

                command.Parameters.AddWithValue("@Naam", domein.Naam);

                if (domein.SubdomeinVan == null)
                    command.Parameters.AddWithValue("@Is_Subdomein_van", DBNull.Value);
                else
                    command.Parameters.AddWithValue("@Is_Subdomein_van", domein.SubdomeinVan);

                if (domein.Image == null)
                    command.CommandText = command.CommandText.Replace("@Thumbnail", "NULL");
                    //command.Parameters.AddWithValue("@Thumbnail", DBNull.Value);
                else
                    command.Parameters.AddWithValue("@Thumbnail", domein.Image);

                int rowsAffected = command.ExecuteNonQuery();
                connection.Close();

                return rowsAffected;
            }
        }

        public int DeleteDomein(string domeinNaam)
        {
            SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["ApplicatiePlattegrondConnectionString"].ToString());
            using (SqlCommand command = new SqlCommand("", connection))
            {
                connection.Open();
                command.CommandText = "DELETE FROM domein WHERE Naam = @Naam";

                command.Parameters.AddWithValue("@Naam", domeinNaam);


                int rowsAffected = command.ExecuteNonQuery();
                connection.Close();

                return rowsAffected;
            }
        }
    }
}
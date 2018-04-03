using Database_Plattegrond.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;

namespace Database_Plattegrond.DatabaseService
{
    public class SoortDatasetDatabaseService
    {
        public List<SoortDataset> GetAlleSoortDataset()
        {
            List<SoortDataset> SoortDatasets = new List<SoortDataset>();

            SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["ApplicatiePlattegrondConnectionString"].ToString());
            using (SqlCommand command = new SqlCommand("", connection))
            {
                connection.Open();
                command.CommandText = "select Type, g.Naam as Naam from Soort_dataset sd left join Gebruiker g on g.ID = sd.Beheerder;";

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    SoortDataset soortDataset = new SoortDataset
                    {
                        Type = reader.GetString(0),
                        Beheerder = new Gebruiker { Naam = reader[1].ToString() }
                    };
                    SoortDatasets.Add(soortDataset);
                }

                connection.Close();
                return SoortDatasets;
            }
        }

        public SoortDataset GetSoortDataset(string type)
        {
            SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["ApplicatiePlattegrondConnectionString"].ToString());
            //SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["jeroen_vd_kolk.datacatalogus"].ToString());
            connection.Open();

            SqlCommand command = new SqlCommand("", connection)
            {
                CommandText = "select Type, g.Naam as Naam from Soort_dataset sd left join Gebruiker g on g.ID = sd.Beheerder WHERE Type = @Type;"
            };
            command.Parameters.AddWithValue("@Type", type);

            SqlDataReader reader = command.ExecuteReader();

            if (reader.Read())
            {
                SoortDataset soortDataset = new SoortDataset
                {
                    Type = reader["Type"].ToString(),
                    Beheerder = new Gebruiker { Naam = reader["Naam"].ToString() }
                };

                connection.Close();
                return soortDataset;
            }

            connection.Close();
            return new SoortDataset();
        }

        public int UpdateSoortDataset(SoortDataset soortDataset)
        {
            SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["ApplicatiePlattegrondConnectionString"].ToString());
            using (SqlCommand command = new SqlCommand("", connection))
            {
                connection.Open();
                command.CommandText = "Update Soort_dataset SET Type = @Type, Beheerder = @Beheerder WHERE Type=@Type";

                command.Parameters.AddWithValue("@Type", soortDataset.Type);

                if (soortDataset.Beheerder.Naam == null)
                    command.Parameters.AddWithValue("@Beheerder", DBNull.Value);
                else
                    //command.Parameters.AddWithValue("@Beheerder", soortDataset.Beheerder.ID);
                    command.Parameters.AddWithValue("@Beheerder", 0);


                int rowsAffected = command.ExecuteNonQuery();
                connection.Close();

                return rowsAffected;
            }
        }

        public int InsertSoortDataset(SoortDataset soortDataset)
        {
            SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["ApplicatiePlattegrondConnectionString"].ToString());
            using (SqlCommand command = new SqlCommand("", connection))
            {
                connection.Open();
                command.CommandText = "INSERT INTO Soort_dataset (Type, Beheerder) VALUES (@Type, @Beheerder)";

                command.Parameters.AddWithValue("@Type", soortDataset.Type);

                if (soortDataset.Beheerder.Naam == null)
                    command.Parameters.AddWithValue("@Beheerder", DBNull.Value);
                else
                    //command.Parameters.AddWithValue("@Beheerder", soortDataset.Beheerder.ID);
                    command.Parameters.AddWithValue("@Beheerder", 0);


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
                command.CommandText = "DELETE FROM Soort_dataset WHERE Type = @Type";

                command.Parameters.AddWithValue("@Type", domeinNaam);
                
                int rowsAffected = command.ExecuteNonQuery();
                connection.Close();

                return rowsAffected;
            }
        }
    }
}
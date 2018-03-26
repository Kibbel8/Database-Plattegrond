using Database_Plattegrond.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;

namespace Database_Plattegrond.DatabaseService
{
    public class ZoekenDatabaseService
    {
        public ZoekViewModel GetAllSearchedDatasets(string zoekterm)
        {
            SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["ApplicatiePlattegrondConnectionString"].ToString());
            connection.Open();
            //Querry nog schrijven
            SqlCommand command = new SqlCommand("", connection)
            {
                CommandText = "SELECT DISTINCT * FROM dataset WHERE Zoektermen LIKE '%@Zoekterm%'"
            };
            command.Parameters.AddWithValue("@Zoekterm", zoekterm);

            SqlDataReader reader = command.ExecuteReader();
            ZoekViewModel zoekVM = new ZoekViewModel { ZoekDatasets = new List<Dataset>() };

            while (reader.Read())
            {
                Dataset result = new Dataset
                {
                    Id = (int)reader["ID"],
                    Naam = reader["naam"].ToString(),
                    Beschrijving = reader["beschrijving"].ToString(),
                    DatumAangemaakt = (DateTime)reader["datum_aangemaakt"],
                    LinkOpenData = reader["link_open_data"].ToString(),
                    Zoektermen = reader["zoektermen"].ToString(),
                    Eigenaar = new Gebruiker { Naam = reader["eigenaar"].ToString() },
                    Applicatie = reader["applicatie"].ToString()
                };

                zoekVM.ZoekDatasets.Add(result);
            }

            connection.Close();
            return zoekVM;
        }
    }
}
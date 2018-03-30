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
            List<string> listZoekElements = zoekterm.Split(' ').ToList();

            SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["ApplicatiePlattegrondConnectionString"].ToString());
            connection.Open();           
            SqlCommand command = new SqlCommand("", connection)
            {
                CommandText = "SELECT * FROM dataset WHERE"
            };
            
            foreach (string zoek in listZoekElements)
            {
                command.CommandText = command.CommandText + " Zoektermen LIKE '%" + zoek + "%' OR Naam LIKE '%" + zoek + "%' OR beschrijving LIKE '%" + zoek + "%'";
            }
            
            SqlDataReader reader = command.ExecuteReader();
            ZoekViewModel zoekVM = new ZoekViewModel { ZoekDatasets = new List<Dataset>() };

            while (reader.Read())
            {

                Dataset result = new Dataset
                {
                    Id = (int)reader["ID"],
                    Naam = reader["naam"].ToString(),
                    Beschrijving = reader["beschrijving"].ToString(),
                    //Applicatie = reader["applicatie"].ToString()
                };

                zoekVM.ZoekDatasets.Add(result);
            }


            connection.Close();
            return zoekVM;
        }
    }
}
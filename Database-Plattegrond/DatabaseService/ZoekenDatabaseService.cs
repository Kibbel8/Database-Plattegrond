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
            int counter = 0;
            SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["ApplicatiePlattegrondConnectionString"].ToString());
            connection.Open();           
            SqlCommand command = new SqlCommand("", connection)
            {
                CommandText = "SELECT DISTINCT *, ("
            };
            
            foreach (string zoek in listZoekElements)
            {
                counter++;                
                command.CommandText = command.CommandText + "(LEN(zoektermen) - LEN(REPLACE(zoektermen, '" + zoek + "', ''))) + (LEN(naam) - LEN(REPlACE(naam, '" + zoek + "',''))) + (LEN(beschrijving) - LEN(REPLACE(beschrijving, '" + zoek + "','')))/COALESCE(NULLIF(LEN('" + zoek + "'), 0), 1)";
                if (counter != listZoekElements.Count)
                    command.CommandText = command.CommandText + " + ";
                else
                {
                    counter = 0;
                    command.CommandText = command.CommandText + ") as found FROM dataset WHERE ";
                    foreach(string zoek2 in listZoekElements)
                    {
                        counter++;
                        command.CommandText = command.CommandText + " Zoektermen LIKE '%" + zoek2 + "%' OR Naam LIKE '%" + zoek2 + "%' OR beschrijving LIKE '%" + zoek2 + "%'";
                        if (counter != listZoekElements.Count)
                            command.CommandText = command.CommandText + " OR";
                        else
                            command.CommandText = command.CommandText + "order by found desc";
                    }
                }
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
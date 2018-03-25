using Database_Plattegrond.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;

namespace Database_Plattegrond.DatabaseService
{
    public class DatasetsDatabaseService
    {

        public Dataset GetDatasetFromId(int id)
        {
            SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["ApplicatiePlattegrondConnectionString"].ToString());
            //SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["jeroen_vd_kolk.datacatalogus"].ToString());
            connection.Open();

            SqlCommand command = new SqlCommand("", connection)
            {
                CommandText = "select d.*,  g.Naam as EIGENAARNAAM, g.Email as EIGENAAREMAIL FROM Dataset d LEFT JOIN Gebruiker g on d.Eigenaar = g.ID WHERE d.ID=@ID"
            };
            command.Parameters.AddWithValue("@ID", id);

            SqlDataReader reader = command.ExecuteReader();

            if (reader.Read())
            {
                Dataset result = new Dataset
                {
                    Id = (int)reader["ID"],
                    Naam = reader["naam"].ToString(),
                    Beschrijving = reader["beschrijving"].ToString(),
                    DatumAangemaakt = (DateTime)reader["datum_aangemaakt"],
                    LinkOpenData = reader["link_open_data"].ToString(),
                    Zoektermen = reader["zoektermen"].ToString(),
                    Eigenaar = new Gebruiker { Naam = reader["EIGENAARNAAM"].ToString(), Email = reader["EIGENAAREMAIL"].ToString() },
                    Applicatie = reader["applicatie"].ToString()
                };

                connection.Close();
                return result;
            }

            connection.Close();
            return new Dataset();
        }

        public DatasetsViewModel GetAllDatasets()
        {
            SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["ApplicatiePlattegrondConnectionString"].ToString());
            connection.Open();

            SqlCommand command = new SqlCommand("", connection)
            {
                CommandText = "SELECT * FROM dataset"
            };

            SqlDataReader reader = command.ExecuteReader();
            DatasetsViewModel datasetVM = new DatasetsViewModel { Datasets = new List<Dataset>() };

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

                datasetVM.Datasets.Add(result);
            }

            connection.Close();
            return datasetVM;
        }

        public List<Dataset> GetDatasetsVoorDomein(string domeinNaam)
        {
            List<Dataset> datasets = new List<Dataset>();

            SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["ApplicatiePlattegrondConnectionString"].ToString());
            using (SqlCommand command = new SqlCommand("", connection))
            {
                connection.Open();
                command.CommandText = "select ID, Naam, Beschrijving, Applicatie FROM Dataset d JOIN Dataset_Domein dd on d.ID = dd.Dataset_ID where dd.Domein_Naam = @DomeinNaam;";

                command.Parameters.AddWithValue("@DomeinNaam", domeinNaam);

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Dataset dataset = new Dataset
                    {
                        Id = reader.GetInt32(0),
                        Naam = reader.GetString(1),
                        Beschrijving = reader.GetString(2),
                        Applicatie = reader.GetString(3)
                    };
                    datasets.Add(dataset);
                }

                connection.Close();
                return datasets;
            }
        }

        public int UpdateDataset(Dataset dataset)
        {
            SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["ApplicatiePlattegrondConnectionString"].ToString());
            using (SqlCommand command = new SqlCommand("", connection))
            {
                connection.Open();
                command.CommandText = "Update dataset SET naam = @naam, beschrijving = @beschrijving, datum_aangemaakt = @datum_aangemaakt, link_open_data = @link_open_data, zoektermen = @zoektermen, eigenaar = @eigenaar, applicatie = @applicatie WHERE ID=@ID";

                command.Parameters.AddWithValue("@ID", dataset.Id);

                if (dataset.Naam == null)
                    command.Parameters.AddWithValue("@naam", DBNull.Value);
                else
                    command.Parameters.AddWithValue("@naam", dataset.Naam);

                command.Parameters.AddWithValue("@beschrijving", dataset.Beschrijving ?? "");

                if (dataset.DatumAangemaakt == null)
                    command.Parameters.AddWithValue("@datum_aangemaakt", DBNull.Value);
                else
                    command.Parameters.AddWithValue("@datum_aangemaakt", dataset.DatumAangemaakt);

                if (dataset.LinkOpenData == null)
                    command.Parameters.AddWithValue("@link_open_data", DBNull.Value);
                else
                    command.Parameters.AddWithValue("@link_open_data", dataset.LinkOpenData);

                if (dataset.Zoektermen == null)
                    command.Parameters.AddWithValue("@zoektermen", DBNull.Value);
                else
                    command.Parameters.AddWithValue("@zoektermen", dataset.Zoektermen);
                if (dataset.Eigenaar == null)
                    command.Parameters.AddWithValue("@eigenaar", DBNull.Value);
                else
                    command.Parameters.AddWithValue("@eigenaar", dataset.Eigenaar);

                if (dataset.Applicatie == null)
                    command.Parameters.AddWithValue("@applicatie", DBNull.Value);
                else
                    command.Parameters.AddWithValue("@applicatie", dataset.Applicatie);

                int rowsAffected = command.ExecuteNonQuery();
                connection.Close();

                return rowsAffected;
            }
        }

        public int InsertDataset(Dataset dataset)
        {
            SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["ApplicatiePlattegrondConnectionString"].ToString());
            using (SqlCommand command = new SqlCommand("", connection))
            {
                connection.Open();
                command.CommandText = "INSERT INTO dataset (naam, beschrijving, datum_aangemaakt, link_open_data, zoektermen, eigenaar, applicatie) VALUES(@naam, @beschrijving, @datum_aangemaakt, @link_open_data, @zoektermen, @eigenaar, @applicatie)";


                if (dataset.Naam == null)
                    command.Parameters.AddWithValue("@naam", DBNull.Value);
                else
                    command.Parameters.AddWithValue("@naam", dataset.Naam);

                command.Parameters.AddWithValue("@beschrijving", dataset.Beschrijving ?? "");

                if (dataset.DatumAangemaakt == null)
                    command.Parameters.AddWithValue("@datum_aangemaakt", DBNull.Value);
                else
                    command.Parameters.AddWithValue("@datum_aangemaakt", dataset.DatumAangemaakt);

                if (dataset.LinkOpenData == null)
                    command.Parameters.AddWithValue("@link_open_data", DBNull.Value);
                else
                    command.Parameters.AddWithValue("@link_open_data", dataset.LinkOpenData);

                if (dataset.Zoektermen == null)
                    command.Parameters.AddWithValue("@zoektermen", DBNull.Value);
                else
                    command.Parameters.AddWithValue("@zoektermen", dataset.Zoektermen);

                if (dataset.Eigenaar == null)
                    command.Parameters.AddWithValue("@eigenaar", DBNull.Value);
                else
                    command.Parameters.AddWithValue("@eigenaar", dataset.Eigenaar);

                if (dataset.Applicatie == null)
                    command.Parameters.AddWithValue("@applicatie", DBNull.Value);
                else
                    command.Parameters.AddWithValue("@applicatie", dataset.Applicatie);

                int rowsAffected = command.ExecuteNonQuery();

                //SELECTS de laatste identity die toegevoegd is, dus hiermee krijg je de id van de toegevoegde dataset
                command.CommandText = "SELECT ident_current('dataset');";

                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    dataset.Id = Convert.ToInt32((decimal)reader[0]);
                }
                else
                {
                    dataset.Id = -1;
                }

                connection.Close();

                return dataset.Id;
            }
        }

        public int DeleteDataset(int datasetID)
        {
            throw new NotImplementedException();
        }

        public int InsertDatasetDomein(int datasetID, string domeinNaam)
        {
            SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["ApplicatiePlattegrondConnectionString"].ToString());
            using (SqlCommand command = new SqlCommand("", connection))
            {
                connection.Open();
                command.CommandText = "INSERT INTO dataset_domein (dataset_id, domein_naam) VALUES (@dataset_id, @domein_naam)";

                command.Parameters.AddWithValue("@dataset_id", datasetID);

                command.Parameters.AddWithValue("@domein_naam", domeinNaam);

                int rowsAffected = command.ExecuteNonQuery();
                connection.Close();

                return rowsAffected;
            }
        }

        public int DeleteDatasetDomein(int datasetID, string domeinNaam)
        {
            SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["ApplicatiePlattegrondConnectionString"].ToString());
            using (SqlCommand command = new SqlCommand("", connection))
            {
                connection.Open();
                command.CommandText = "DELETE FROM dataset_domein WHERE dataset_id = @dataset_id AND domein_naam = @domein_naam";

                command.Parameters.AddWithValue("@dataset_id", datasetID);

                command.Parameters.AddWithValue("@domein_naam", domeinNaam);

                int rowsAffected = command.ExecuteNonQuery();
                connection.Close();

                return rowsAffected;
            }
        }
    }
}
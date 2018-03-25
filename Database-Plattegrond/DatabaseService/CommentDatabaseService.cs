using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Database_Plattegrond.Models;
using System.Data.SqlClient;
using System.Web.Configuration;

namespace Database_Plattegrond.DatabaseService
{
    public class CommentDatabaseService
    {

        public List<Comment> GetCommentsVoorDataset(int datasetID)
        {
            List<Comment> comments = new List<Comment>();

            SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["ApplicatiePlattegrondConnectionString"].ToString());
            using (SqlCommand command = new SqlCommand("", connection))
            {
                connection.Open();
                command.CommandText = "SELECT g.Naam, g.ID, Datum_Geplaatst, Tekst, Status_Comment FROM comment c JOIN Gebruiker g on g.ID = c.Gebruiker WHERE Dataset_ID = @Dataset_ID";

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Comment comment = new Comment
                    {
                        Gebruiker = new Gebruiker { Naam = reader.GetString(0), ID = reader.GetInt32(1) },
                        DatumGeplaatst = reader.GetDateTime(2),
                        Tekst = reader.GetString(3),
                        Status = reader.GetString(4)
                    };
                    comments.Add(comment);
                }

                connection.Close();
                return comments;
            }
        }

        public int InsertComment(Comment comment)
        {
            SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["ApplicatiePlattegrondConnectionString"].ToString());
            using (SqlCommand command = new SqlCommand("", connection))
            {
                connection.Open();
                command.CommandText = "INSERT INTO comment (Gebruiker, Datum_Geplaatst, Dataset_ID, Tekst, Status_Comment) VALUES (@Gebruiker, @Datum_Geplaatst, @Dataset_ID, @Tekst, @Status_Comment);";

                command.Parameters.AddWithValue("@Gebruiker", comment.Gebruiker.Naam);
                command.Parameters.AddWithValue("@Datum_Geplaatst", DateTime.Now);
                command.Parameters.AddWithValue("@Dataset_ID", comment.DatasetID);
                command.Parameters.AddWithValue("@Tekst", comment.Tekst);
                command.Parameters.AddWithValue("@Tekst", comment.Status);

                int rowsAffected = command.ExecuteNonQuery();
                connection.Close();

                return rowsAffected;
            }
        }

        public int UpdateComment(Comment comment)
        {
            SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["ApplicatiePlattegrondConnectionString"].ToString());
            using (SqlCommand command = new SqlCommand("", connection))
            {
                connection.Open();
                command.CommandText = "UPDATE comment SET Tekst = @Tekst, Status_Comment = @Status_Comment WHERE Gebruiker = @Gebruiker AND Datum_Geplaatst = @Datum_Geplaatst;";

                command.Parameters.AddWithValue("@Gebruiker", comment.Gebruiker.ID);
                command.Parameters.AddWithValue("@Datum_Geplaatst", comment.DatumGeplaatst);
                command.Parameters.AddWithValue("@Tekst", comment.Tekst ?? "");
                command.Parameters.AddWithValue("@Status_Comment", comment.Status ?? "");

                int rowsAffected = command.ExecuteNonQuery();
                connection.Close();

                return rowsAffected;
            }
        }
    }
}
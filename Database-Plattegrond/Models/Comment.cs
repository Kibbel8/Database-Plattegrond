using System;

namespace Database_Plattegrond.Models
{
    public class Comment
    {
        public Gebruiker Gebruiker { get; set; }
        public DateTime DatumGeplaatst { get; set; }
        public string Tekst { get; set; }
        public string Status { get; set; }
        public int DatasetID { get; set; }
    }
}
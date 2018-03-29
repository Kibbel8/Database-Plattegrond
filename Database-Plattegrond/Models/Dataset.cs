using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Database_Plattegrond.Models
{
    public class Dataset
    {
        public int Id { get; set; }
        public string Naam { get; set; }
        public string Beschrijving { get; set; }
        public DateTime DatumAangemaakt { get; set; }
        public string LinkOpenData { get; set; }
        public string Zoektermen { get; set; }
        public Gebruiker Eigenaar { get; set; }
        public string Applicatie { get; set; }
        public List<Domein> Domeinen { get; set; }
        public string TypeDataset { get; set; }
    }
}
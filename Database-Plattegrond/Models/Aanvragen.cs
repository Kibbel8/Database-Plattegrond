using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Database_Plattegrond.Models
{
    public class Aanvragen
    {
        public int DatasetID { get; set; }
        public string Naam { get; set; }
        public string Beschrijving { get; set; }
        public Gebruiker Eigenaar { get; set; }
        public string VerwDoeleind { get; set; }
        public string NodigVanaf { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Database_Plattegrond.Models
{
    public class Aanvraag
    {
        public Dataset Dataset { get; set; }
        public Gebruiker Eigenaar { get; set; }
        public string Verwerkingsdoeleind { get; set; }
        public string WanneerNodig { get; set; }
    }
}
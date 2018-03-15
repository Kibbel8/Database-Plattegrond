using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Database_Plattegrond.Models
{
    public class Beheerder
    {
        public string Naam { get; set; }
        public string Email { get; set; }
        public string Functie { get; set; }
        public string Afdeling { get; set; }
        public string Telnr { get; set; }
    }
}
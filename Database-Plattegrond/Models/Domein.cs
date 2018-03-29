using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Database_Plattegrond.Models
{
    public class Domein
    {
        public string Naam { get; set; }
        public string SubdomeinVan { get; set; }
        public bool Selected { get; set; }
    }
}
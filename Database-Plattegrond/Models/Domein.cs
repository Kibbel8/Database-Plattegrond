using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Database_Plattegrond.Models
{
    public class Domein
    {
        public string Id;
        public string Naam;
        public List<Domein> SubdomeinVan;
    }
}
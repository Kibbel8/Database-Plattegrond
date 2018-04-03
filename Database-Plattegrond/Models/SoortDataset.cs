using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Database_Plattegrond.Models
{
    public class SoortDataset
    {
        public string Type { get; set; }
        public Gebruiker Beheerder { get; set; }
    }
}
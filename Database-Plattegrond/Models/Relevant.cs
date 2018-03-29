using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Database_Plattegrond.Models
{
    public class Relevant
    {
        public int Id { get; set; }
        public string Naam { get; set; }
        public string Link { get; set; }
        public int DatasetID { get; set; }
    }
}
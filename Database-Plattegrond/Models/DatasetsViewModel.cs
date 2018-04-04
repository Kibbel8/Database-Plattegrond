using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Database_Plattegrond.Models
{
    public class DatasetsViewModel
    {
        public List<Dataset> Datasets { get; set; }
        public string Domein { get; set; }
        public List<Domein> Domeinen { get; set; }

    }
}
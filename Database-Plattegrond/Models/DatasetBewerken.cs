using System.Collections.Generic;
using System.Web.Mvc;

namespace Database_Plattegrond.Models
{
    public class DatasetBewerken
    {
        public Dataset Dataset { get; set; }
        public List<Domein> Domeinen { get; set; }
        public List<SelectListItem> Gebruikers { get; set; }
        public List<SelectListItem> TypeDatasets { get; set; }
    }
}
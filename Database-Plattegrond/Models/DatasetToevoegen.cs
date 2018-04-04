using System.Collections.Generic;
using System.Web.Mvc;

namespace Database_Plattegrond.Models
{
    public class DatasetToevoegen
    {
        public Dataset Dataset { get; set; }
        public List<SelectListItem> Domeinen { get; set; }
        public List<SelectListItem> Gebruikers { get; set; }
        public List<SelectListItem> TypeDatasets { get; set; }
    }
}
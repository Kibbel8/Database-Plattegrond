using Database_Plattegrond.DatabaseService;
using Database_Plattegrond.Models;
using Database_Plattegrond.Utils;
using System.IO;
using System.Web.Mvc;

namespace Database_Plattegrond.Controllers
{
    public class DomeinenController : Controller
    {
        // GET: Domeinen
        public ActionResult Index()
        {
            ViewBag.Message = "Domeinen";

            DomeinenDatabaseService DDS = new DomeinenDatabaseService();
            DomeinenViewModel DVM = new DomeinenViewModel { Domeinen = DDS.GetHoofdDomeinen() };

            return View(DVM);
        }


        public ActionResult Bewerken(string domeinNaam = "")
        {
            DomeinenDatabaseService DDS = new DomeinenDatabaseService();
            Domein domein = DDS.GetDomeinFromNaam(domeinNaam);
            return View(domein);
        }

        [HttpPost]
        public ActionResult Bewerken(Domein domein)
        {
            DomeinenDatabaseService DDS = new DomeinenDatabaseService();

            if (domein.PostedFile != null)
            {
                ImageUtils imageUtils = new ImageUtils();
                byte[] imageArray = null;
                using (var binaryReader = new BinaryReader(Request.Files[0].InputStream))
                {
                    imageArray = binaryReader.ReadBytes(Request.Files[0].ContentLength);
                }
                domein.Image = imageUtils.ScaleImage(imageArray, 75, 75);
            }

            DDS.UpdateDomein(domein);
            return View(domein);
        }

        public ActionResult Toevoegen()
        {
            ViewBag.Message = "Domein toevoegen";

            return View();
        }

        [HttpPost]
        public ActionResult Toevoegen(Domein domein)
        {
            DomeinenDatabaseService DDS = new DomeinenDatabaseService();

            if (domein.PostedFile != null)
            {
                ImageUtils imageUtils = new ImageUtils();

                byte[] imageArray = null;
                using (var binaryReader = new BinaryReader(Request.Files[0].InputStream))
                {
                    imageArray = binaryReader.ReadBytes(Request.Files[0].ContentLength);
                }
                domein.Image = imageUtils.ScaleImage(imageArray, 75, 75);
            }

            DDS.InsertDomein(domein);

            return RedirectToAction("Bewerken", new { domeinNaam = domein.Naam });
        }

        [HttpPost]
        public ActionResult Verwijderen(string domeinNaam)
        {
            DomeinenDatabaseService domeinDatabaseService = new DomeinenDatabaseService();
            domeinDatabaseService.DeleteDomein(domeinNaam);
            return RedirectToAction("Index");
        }
    }
}
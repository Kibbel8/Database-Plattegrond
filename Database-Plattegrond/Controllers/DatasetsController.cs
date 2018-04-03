using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Database_Plattegrond.Models;
using Database_Plattegrond.DatabaseService;
using System.Web.Routing;
using System.Net.Mail;
using System.IO;

namespace Database_Plattegrond.Controllers
{
    public class DatasetsController : Controller
    {
        // GET: Dataset
        public ActionResult Index(string domein = "")
        {
            ViewBag.Message = "Datasets";
            DatasetsViewModel datasetsViewModel = new DatasetsViewModel()
            {
                Domein = domein
            };

            DatasetsDatabaseService dds = new DatasetsDatabaseService();
            if (domein == "")
            {
                datasetsViewModel.Datasets = dds.GetAllDatasets();
            }
            else
            {
                datasetsViewModel.Datasets = dds.GetDatasetsVoorDomein(domein);
            }

            DomeinenDatabaseService domDS = new DomeinenDatabaseService();
            datasetsViewModel.Domeinen = domDS.GetHoofdDomeinen();

            return View(datasetsViewModel);
        }

        public ActionResult Details(int? id = -1)
        {
            ViewBag.Message = "Dataset pagina";

            DatasetsDatabaseService datasetsDS = new DatasetsDatabaseService();
            Dataset dataset = datasetsDS.GetDatasetFromId(id.Value);

            CommentDatabaseService CDS = new CommentDatabaseService();
            //TODO Sorteer comments op logische wijze
            List<Comment> comments = CDS.GetCommentsVoorDataset(id.Value);

            RelevantDatabaseService RDS = new RelevantDatabaseService();
            List<Relevant> links = RDS.GetRelevanteLinksVoorDataset(id.Value);

            DatasetDetail datasetDetail = new DatasetDetail
            {
                Dataset = dataset,
                Comments = comments,
                Links = links,
                CommentStatussen = new List<SelectListItem> { { new SelectListItem { Text = "Niet Verwerkt", Value = "Niet Verwerkt" } }, { new SelectListItem { Text = "In Behandeling", Value = "In Behandeling" } }, { new SelectListItem { Text = "Verwerkt", Value = "Verwerkt" } } }
            };
            return View(datasetDetail);
        }

        [HttpPost]
        public ActionResult SubmitComment(DatasetDetail datasetDetail)
        {
            if (datasetDetail.NewCommentText != null)
            {
                CommentDatabaseService CDS = new CommentDatabaseService();

                Comment comment = new Comment
                {
                    DatasetID = datasetDetail.Dataset.Id,
                    DatumGeplaatst = DateTime.Now,
                    Status = "Niet verwerkt",
                    Tekst = datasetDetail.NewCommentText,
                    Gebruiker = new Gebruiker { ID = 0 }
                };

                CDS.InsertComment(comment);
            }

            return RedirectToAction("Details", new { id = datasetDetail.Dataset.Id });
        }

        public ActionResult Bewerken(int? id = -1)
        {
            ViewBag.Message = "Dataset Bewerken";

            //Haalt de betreffende dataset op uit DB
            DatasetsDatabaseService datasetsDS = new DatasetsDatabaseService();
            Dataset dataset = datasetsDS.GetDatasetFromId(id.Value);

            //Haalt alle domeinen uit DB
            DomeinenDatabaseService domeinenDS = new DomeinenDatabaseService();
            List<Domein> domeinen = domeinenDS.GetAlleDomeinen();
            List<SelectListItem> domeinenList = new List<SelectListItem>();

            foreach(Domein domein in domeinen)
            {
                SelectListItem item = new SelectListItem { Text = domein.Naam, Value = domein.Naam };
                domeinenList.Add(item);
            }

            //Haalt alle gebruikers uit DB
            GebruikersDatabaseService gebruikersDS = new GebruikersDatabaseService();
            List<Gebruiker> gebruikers = gebruikersDS.GetAllGebruikers();
            List<SelectListItem> gebruikersList = new List<SelectListItem>();
            
            foreach (Gebruiker gebruiker in gebruikers)
            {
                SelectListItem item = new SelectListItem { Text = gebruiker.Naam, Value = gebruiker.ID.ToString() };
                gebruikersList.Add(item);
            }

            DatasetBewerken datasetBewerken = new DatasetBewerken
            {
                Dataset = dataset,
                Domeinen = domeinenList,
                Gebruikers = gebruikersList,
                TypeDatasets = new List<SelectListItem> { { new SelectListItem { Text = "Test 1", Value = "Test 1" } }, { new SelectListItem { Text = "Test 2", Value = "Test 2" } } }
            };

            return View(datasetBewerken);
        }

        [HttpPost]
        public ActionResult Bewerken(DatasetBewerken model)
        {
            ViewBag.Message = "Dataset Bewerken";

            DatasetsDatabaseService dds = new DatasetsDatabaseService();
            int rowsAffected = dds.UpdateDataset(model.Dataset);
            return RedirectToAction("Details", new { id = model.Dataset.Id });
        }

        public ActionResult Toevoegen(int? id = -1)
        {
            ViewBag.Message = "Dataset Pagina toevoegen";

            //Haalt betreffende dataset uit DB
            DatasetsDatabaseService datasetsDS = new DatasetsDatabaseService();
            Dataset dataset = datasetsDS.GetDatasetFromId(id.Value);

            //Haalt alle domeinen uit DB
            DomeinenDatabaseService domeinenDS = new DomeinenDatabaseService();
            List<Domein> domeinen = domeinenDS.GetAlleDomeinen();
            List<SelectListItem> domeinenList = new List<SelectListItem>();

            foreach (Domein domein in domeinen)
            {
                SelectListItem item = new SelectListItem { Text = domein.Naam, Value = domein.Naam };
                domeinenList.Add(item);
            }

            //Haalt alle gebruikers uit DB
            GebruikersDatabaseService GDS = new GebruikersDatabaseService();
            List<Gebruiker> gebruikers = GDS.GetAllGebruikers();
            List<SelectListItem> gebruikersList = new List<SelectListItem>();

            foreach (Gebruiker gebruiker in gebruikers)
            {
                SelectListItem item = new SelectListItem { Text = gebruiker.Naam, Value = gebruiker.ID.ToString() };
                gebruikersList.Add(item);
            }

            DatasetToevoegen datasetToevoegen = new DatasetToevoegen
            {
                Dataset = dataset,
                Domeinen = domeinenList,
                Gebruikers = gebruikersList,
                TypeDatasets = new List<SelectListItem> { { new SelectListItem { Text = "Test 1", Value = "Test 1" } }, { new SelectListItem { Text = "Test 2", Value = "Test 2" } } }
            };

            return View(datasetToevoegen);
        }

        [HttpPost]
        public ActionResult Toevoegen(DatasetBewerken model)
        {
            ViewBag.Message = "Dataset Pagina toevoegen";
            model.Dataset.DatumAangemaakt = DateTime.Now;

            //Voeg dataset toe aan DB
            DatasetsDatabaseService datasetsDS = new DatasetsDatabaseService();
            int id = datasetsDS.InsertDataset(model.Dataset);

            return RedirectToAction("Details", new { id });
        }

        public ActionResult AanvraagFormulier(int? id = -1)
        {
            ViewBag.Message = "Aanvraag Formulier";
            
            DatasetsDatabaseService dds = new DatasetsDatabaseService();
            Dataset dataset = dds.GetDatasetFromId(id.Value);

            Aanvragen aanvragen = new Aanvragen
            {
                DatasetID = dataset.Id,
                Naam = dataset.Naam,
                Beschrijving = dataset.Beschrijving,
                Eigenaar = dataset.Eigenaar
            };

            return View(aanvragen);
        }

        public ActionResult AanvraagSturen(Aanvragen aanvraag)
        {
            var mail = new MailMessage{ Subject = "Aanvraag dataset " + aanvraag.Naam };

            var writer = new StringWriter();
            writer.WriteLine("Beste " + aanvraag.Eigenaar.Naam + "");
            writer.WriteLine();
            writer.WriteLine();
            writer.WriteLine("Hierbij vraag ik bij u de dataset " + aanvraag.Naam + " aan, met als verwerkingsdoeleind:");
            writer.WriteLine();
            writer.WriteLine(aanvraag.VerwDoeleind);
            writer.WriteLine();
            writer.WriteLine("Graag ontvang ik deze per: " + aanvraag.NodigVanaf);
            writer.WriteLine();
            writer.WriteLine("Hoogachtend,");
            writer.WriteLine();
            writer.WriteLine("[INGELOGDE GEBRUIKER]");
            mail.Body = writer.ToString();

            string mailText = "mailto:" + aanvraag.Eigenaar.Email + "?" + string.Join("&", Parameters(mail));
            System.Diagnostics.Process.Start(mailText);

            return RedirectToAction("Details", new { id = aanvraag.DatasetID });
        }

        static IEnumerable<string> Parameters(MailMessage message)
        {
            if (message.To.Any())
                yield return "to=" + Recipients(message.To);

            if (message.CC.Any())
                yield return "cc=" + Recipients(message.CC);

            if (message.Bcc.Any())
                yield return "bcc=" + Recipients(message.Bcc);

            if (!string.IsNullOrWhiteSpace(message.Subject))
                yield return "subject=" + Uri.EscapeDataString(message.Subject);

            if (!string.IsNullOrWhiteSpace(message.Body))
                yield return "body=" + Uri.EscapeDataString(message.Body);
        }

        static string Recipients(MailAddressCollection addresses) =>
            string.Join(",", from r in addresses
                             select Uri.EscapeDataString(r.Address));

        public ActionResult Verwijderen(int? id = -1)
        {
            DatasetsDatabaseService datasetsDatabaseService = new DatasetsDatabaseService();
            
            datasetsDatabaseService.DeleteDataset(id.Value);
            return RedirectToAction("Index");
        }

        public ActionResult NieuweLink(int? id = -1)
        {
            ViewBag.Message = "NieuweLink";

            DatasetsDatabaseService dds = new DatasetsDatabaseService();
            Dataset dataset = dds.GetDatasetFromId(id.Value);

            return View(dataset);
        }

        public ActionResult NieuweLinkVersturen(Dataset dataset)
        {

            dataset.NieuweLink.DatasetID = dataset.Id;
            RelevantDatabaseService rds = new RelevantDatabaseService();
            rds.InsertRelevanteLink(dataset.NieuweLink);

            return RedirectToAction("Details", new { id = dataset.Id });
        }
    }
}
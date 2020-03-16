using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace OIWEB.Controllers
{
    public class PrijavaController : Controller
    {
        ObrazovniInformatorDataContext oi = new ObrazovniInformatorDataContext();
        // GET: Prijava
        public ActionResult Index()
        {
            if (Session["Korisnik"] == null)
            {
                return RedirectToAction("Page404");
            }
            List<PravnoLice> pravnaLica = (from p in oi.PravnoLices
                                           select p).ToList();
            ViewBag.PravnaLica = pravnaLica;
            List<StrucnoUsavrsavanje> strucnaU = (from s in oi.StrucnoUsavrsavanjes
                                                  select s).ToList();
            ViewBag.StrucnaU = strucnaU;
            return View(oi.Prijavas.ToList());
        }

        public ActionResult Details(int id)
        {
            if (Session["Korisnik"] == null)
            {
                return RedirectToAction("Page404");
            }
            PravnoLice pravnaLica = (from pr in oi.PravnoLices
                                           where pr.IDPravnoLice == id
                                           select pr).Single();
            return View(pravnaLica);
        }
        [HttpGet]
        public ActionResult Create(int id)
        {
            ViewBag.IDSeminar = id;
            return View();
        }

        [HttpPost]
        public ActionResult Create(int id,FormCollection fc)
        {
            List<PravnoLice> pravnaLica = (from pr in oi.PravnoLices
                                           select pr).ToList();
            bool provera = false;
            foreach(PravnoLice pravno in pravnaLica)
            {
                if (pravno.PIB != fc["PIB"])
                {
                    provera = true;
                }
                else
                {
                    provera = false;
                }
            }

            if (provera)
            {
                PravnoLice pl = new PravnoLice();
                pl.PreplatnickiBroj = fc["PretplatnickiBroj"];
                pl.NazivPravnogLica = fc["NazivPravnogLica"];
                pl.BrojTekucegRacuna = fc["BrojTekucegRacuna"];
                pl.PIB = fc["PIB"];
                pl.Mesto = fc["Mesto"];
                pl.PostanskiBroj = fc["PostanskiBroj"];
                pl.UlicaIBroj = fc["UlicaIBroj"];
                pl.Email = fc["Email"];
                try
                {
                    oi.PravnoLices.InsertOnSubmit(pl);
                    oi.SubmitChanges();
                }
                catch
                {
                    return View();
                }
            }
            PravnoLice pravnoLice = (from prav in oi.PravnoLices
                                     where prav.PIB.Equals(fc["PIB"])
                                     select prav).Single();

            Prijava p = new Prijava();
            p.DatumPrijave = DateTime.Today.ToString();
            p.NacinPlacanja = fc["NacinPlacanja"];
            p.IDUsavrsavanja = id;
            p.IDPravnoLice = pravnoLice.IDPravnoLice;
            try
            {
                oi.Prijavas.InsertOnSubmit(p);
                oi.SubmitChanges();
                SaljiMejl(p, pravnoLice);

                ViewBag.IDSeminar = id;
                return View();
            }
            catch
            {
                return View();
            }

        }


        public void SaljiMejl(Prijava p, PravnoLice pl)
        {
            MailMessage msg = new MailMessage("mihajlovicandjelija@gmail.com", "dogandzic97@gmail.com");

            msg.Subject = "Narudzbenica";
            msg.Body = "Narudzbenica" +
                " Ime pravnog lica: " + pl.NazivPravnogLica + ";     Broj Tekuceg Racuna: " + pl.BrojTekucegRacuna + ";        Adresa: " + pl.Mesto + " " + pl.UlicaIBroj + ";" +
                "Nacin Placanja: "+p.NacinPlacanja;
            SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
            // smtp.Host = "relay-hosting.secureserver.net";
            //smtp.Port = 25;
            smtp.Credentials = new System.Net.NetworkCredential()
            {
                UserName = "mihajlovicandjelija@gmail.com",
                Password = "Volimtinu1"
            };
            smtp.EnableSsl = true;
            smtp.Send(msg);
        }

        public ActionResult Page404()
        {
            return View();
        }
    }
}
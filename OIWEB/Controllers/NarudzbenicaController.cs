using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;
using System.Net.Mail;

namespace OIWEB.Controllers
{
    public class NarudzbenicaController : Controller
    {
        ObrazovniInformatorDataContext oi = new ObrazovniInformatorDataContext();
        // GET: Narudzbenica
        public ActionResult Index()
        {
            if (Session["Korisnik"] == null)
            {
                return RedirectToAction("Page404");
            }
            List<Narucilac> narucilac = (from n in oi.Narucilacs
                             select n).ToList();

            ViewBag.Narucilac = narucilac;

            List<OstalaIzdanja> ostalaIzdanja = (from o in oi.OstalaIzdanjas
                                                 select o).ToList();

            //ISPISATI I KONTAKT OSOBU
            ViewBag.OstalaIzdanja = ostalaIzdanja;
            return View(oi.Narudzbenicas.ToList());
        }

        [HttpGet]
        public ActionResult Create(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Page404");
            }
            ViewBag.IDIzdanja = id;
            
            return View();
        }

        [HttpPost]
        public ActionResult Create(int id, FormCollection fc)
        {
            List<Narucilac> narucioci = (from na in oi.Narucilacs
                                select na).ToList();
            bool provera = false;
            foreach(Narucilac naru in narucioci)
            {
                if (naru.PIB != fc["PIB"])
                    provera = true;
                else
                    provera = false;
            }
            if (provera)
            {
                Narucilac narucilac = new Narucilac();
                narucilac.PreprlatnickiBroj = fc["PretplatnickiBroj"];
                narucilac.NaziPravnogLica = fc["NaziPravnogLica"];
                narucilac.BrojTekucegRacuna = fc["BrojTekucegRacuna"];
                narucilac.PIB = fc["PIB"];
                narucilac.Mesto = fc["Mesto"];
                narucilac.UlicaIBroj = fc["UlicaIBroj"];
                narucilac.PostanskiBroj = Convert.ToInt32(fc["PostanskiBroj"]);
                try
                {
                    oi.Narucilacs.InsertOnSubmit(narucilac);
                    oi.SubmitChanges();
                }
                catch
                {
                    return View();
                }
            }
            Narucilac pravnoLice = (from nar in oi.Narucilacs
                                    where nar.PIB == fc["PIB"]
                                    select nar).Single();

            List<KontaktOsobe> kontaktOsobe = (from k in oi.KontaktOsobes
                                               select k).ToList();
            bool proveraEmail = false;

            foreach(KontaktOsobe kon in kontaktOsobe)
            {
                if (kon.Email == fc["Email"])
                {
                    proveraEmail = true;
                }
                else
                {
                    proveraEmail = false;
                }
            }

            if (proveraEmail)
            {
                KontaktOsobe ko = new KontaktOsobe();
                ko.Ime = fc["Ime"];
                ko.Prezime = fc["Prezime"];
                ko.Telefon = fc["Telefon"];
                ko.Faks = fc["Faks"];
                ko.Email = fc["Email"];
                ko.IDNarucilac = pravnoLice.IDNarucioca;
                try
                {
                    oi.KontaktOsobes.InsertOnSubmit(ko);
                    oi.SubmitChanges();
                }
                catch
                {
                    return View();
                }
            }

           

            Narudzbenica narudzbenica = new Narudzbenica();
            narudzbenica.IDNarucilac = pravnoLice.IDNarucioca;
            narudzbenica.IDOstalihIzdanja = id;
            narudzbenica.Kolicina = Convert.ToInt32(fc["Kolicina"]);
            narudzbenica.DatumPorucivanja = Convert.ToString(DateTime.Today);
            try
            {
                oi.Narudzbenicas.InsertOnSubmit(narudzbenica);
                oi.SubmitChanges();
                SaljiMejl(narudzbenica, pravnoLice);
                ViewBag.IDIzdanja = id;
                ViewBag.Msg = "Uspeh";
                return View();
            }
            catch(Exception ex)
            {
                ViewBag.Msg = "Neuspeh"+ex.Message;
                ViewBag.IDIzdanja = id;
                return View();
            }
        }

        public void SaljiMejl(Narudzbenica n, Narucilac nar)
        {
            MailMessage msg = new MailMessage("mihajlovicandjelija@gmail.com", "dogandzic97@gmail.com");
         
            msg.Subject = "Narudzbenica";
            msg.Body = "Narudzbenica" +
                " Ime pravnog lica: " + nar.NaziPravnogLica + ";     Broj Tekuceg Racuna: " + nar.BrojTekucegRacuna + ";        Adresa: " + nar.Mesto + "  " + nar.UlicaIBroj + ";" +
                "   Kolicina: " + n.Kolicina;
            SmtpClient smtp = new SmtpClient("smtp.gmail.com",587);
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

        public ActionResult Details(int? id)
        {
            if (Session["Korisnik"] == null || id == null)
            {
                return RedirectToAction("Page404");
            }
            KontaktOsobe kontaktOsoba = (from ko in oi.KontaktOsobes
                                                where ko.IDNarucilac == id
                                                select ko).Single();
            return View(kontaktOsoba);
        }

        public ActionResult Page404()
        {
            return View();
        }
    }

}
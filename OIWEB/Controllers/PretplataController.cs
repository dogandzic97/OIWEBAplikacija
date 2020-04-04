using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace OIWEB.Controllers
{
    public class PretplataController : Controller
    {
        ObrazovniInformatorDataContext oi = new ObrazovniInformatorDataContext();
        // GET: Pretplata
        public ActionResult Index()
        {
            if (Session["Korisnik"] == null)
            {
                return RedirectToAction("Page404");
            }
            List<Casopi> casopis = (from c in oi.Casopis
                                    select c).ToList();
            ViewBag.Casopis = casopis;

            List<Narucilac> narucilac = (from n in oi.Narucilacs
                                         select n).ToList();
            ViewBag.Narucilac = narucilac;

            //ISPISATI I KONTAKT OSOBU

            return View(oi.Pretplatas.ToList());
            
        }

        [HttpGet]
        public ActionResult Create(string id)
        {
            ViewBag.Msg = id;
            return View();
        }

        [HttpPost]
        public ActionResult Create(FormCollection fc)
        {
            List<Narucilac> naroucioci = (from na in oi.Narucilacs
                                          select na).ToList();
            bool provera = true;
            foreach(Narucilac s in naroucioci)
            {
                if (s.PIB == fc["PIB"])
                {
                    provera = false;
                    break;
                }
            }

            if (provera==true)
            {

                Narucilac n = new Narucilac();

                n.PreprlatnickiBroj = fc["PretplatnickiBroj"];
                n.NaziPravnogLica = fc["NaziPravnogLica"];
                n.BrojTekucegRacuna = fc["BrojTekucegRacuna"];
                n.PIB = fc["PIB"];
                n.Mesto = fc["Mesto"];
                n.PostanskiBroj = Convert.ToInt32(fc["PostanskiBroj"]);
                n.UlicaIBroj = fc["UlicaIBroj"];
                try
                {
                    oi.Narucilacs.InsertOnSubmit(n);
                    oi.SubmitChanges();
                }
                catch
                {
                    return View();
                }
            }

            Narucilac narucilac = (from na in oi.Narucilacs
                                   where na.PIB == fc["PIB"]
                                   select na).Single();
            bool proveraMail = true;

            List<KontaktOsobe> kontaktOsobe = (from ko in oi.KontaktOsobes
                                               select ko).ToList();

            foreach(KontaktOsobe kontakt in kontaktOsobe)
            {
                if (kontakt.Email == fc["Email"])
                {
                    proveraMail = false;
                    break;
                }
            
            }
            if (proveraMail==true)
            {
                KontaktOsobe kontakti = new KontaktOsobe();
                kontakti.Ime = fc["Ime"];
                kontakti.Prezime = fc["Prezime"];
                kontakti.Telefon = fc["Telefon"];
                kontakti.Faks = fc["Faks"];
                kontakti.Email = fc["Email"];
                kontakti.IDNarucilac = narucilac.IDNarucioca;
            }
            Pretplata p = new Pretplata();

            p.DatumPocetka = DateTime.Today.ToString();
            p.DatumKraja = "a";
            p.Trajanje = fc["Trajanje"];
            p.NacinPlacanja = fc["NacinPlacanja"];
            p.BrojPrimeraka = Convert.ToInt32(fc["BrojPrimeraka"]);
           
            p.IDNarucioc = narucilac.IDNarucioca;
            p.IDCasopis = 1;

            try
            {
                oi.Pretplatas.InsertOnSubmit(p);
                oi.SubmitChanges();
                SaljiMejl(p, narucilac);
                ViewBag.IDCasopis = fc["IDCasopis"];
                //  string poruka = "Uspeh";
                return RedirectToAction("Create/uspeh");
            }
            catch
            {
                return View();
            }


        }

        public void SaljiMejl(Pretplata n, Narucilac nar)
        {
            MailMessage msg = new MailMessage("mihajlovicandjelija@gmail.com", "dogandzic97@gmail.com");

            msg.Subject = "Narudzbenica";
            msg.Body = "Narudzbenica" +
                " Ime pravnog lica: " + nar.NaziPravnogLica + ";     Broj Tekuceg Racuna: " + nar.BrojTekucegRacuna + ";        Adresa: " + nar.Mesto + "  " + nar.UlicaIBroj + ";" +
                "   Kolicina: " + n.BrojPrimeraka;
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


        public ActionResult Details(int id)
        {
            if (Session["Korisnik"] == null)
            {
                return RedirectToAction("Page404");
            }
            KontaktOsobe ko = (from k in oi.KontaktOsobes
                               where k.IDNarucilac == id
                               select k).Single();
            return View(ko);
        }

        public ActionResult Page404()
        {
            return View();
        }

    }
}
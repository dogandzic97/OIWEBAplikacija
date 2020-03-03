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
            List<Narucilac> narucilac = (from n in oi.Narucilacs
                             select n).ToList();

            ViewBag.Narucilac = narucilac;

            List<OstalaIzdanja> ostalaIzdanja = (from o in oi.OstalaIzdanjas
                                                 select o).ToList();
            ViewBag.OstalaIzdanja = ostalaIzdanja;
            return View(oi.Narudzbenicas.ToList());
        }

        [HttpGet]
        public ActionResult Create(int id)
        {
            ViewBag.IDIzdanja = id;
            
            return View();
        }

        [HttpPost]
        public ActionResult Create(int id, FormCollection fc)
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

            int idNarucioca = (from nar in oi.Narucilacs
                               select nar.IDNarucioca).Max();

            Narudzbenica narudzbenica = new Narudzbenica();
            narudzbenica.IDNarucilac = idNarucioca;
            narudzbenica.IDOstalihIzdanja = id;
            narudzbenica.Kolicina = Convert.ToInt32(fc["Kolicina"]);
            narudzbenica.DatumPorucivanja = Convert.ToString(DateTime.Today);
            try
            {
                oi.Narudzbenicas.InsertOnSubmit(narudzbenica);
                oi.SubmitChanges();
                SaljiMejl(narudzbenica, narucilac);
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
    }

}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OIWEB.Controllers
{
    public class StatistikaIPodaciController : Controller
    {
        ObrazovniInformatorDataContext oi = new ObrazovniInformatorDataContext();
        // GET: StatistikaIPodaci
        public ActionResult Index()
        {
            var statistika = from s in oi.StatistickiPodacis
                             select s;
            return View(statistika.ToList());
        }

        [HttpGet]
        public ActionResult Create()
        {
            if (Session["Korisnik"] == null)
            {
                return RedirectToAction("Page404");
            }
            return View();
        }

        [HttpPost]
        public ActionResult Create(FormCollection fc)
        {
            Korisnik k = (Korisnik)Session["Korisnik"];
            StatistickiPodaci sip = new StatistickiPodaci();
            sip.Naslov = fc["Naslov"];
            sip.PDF ="Content/Statistika/" + fc["PDF"];
            sip.IDKorisnik = k.IDKorisnik;
            try
            {
                oi.StatistickiPodacis.InsertOnSubmit(sip);
                oi.SubmitChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Delete(int id)
        {
            StatistickiPodaci s = (from st in oi.StatistickiPodacis
                                   where st.IDStatistickihPodataka == id
                                   select st).Single();
            try
            {
                oi.StatistickiPodacis.DeleteOnSubmit(s);
                oi.SubmitChanges();
                return RedirectToAction("Index"); 
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Page404()
        {
            return View();
        }
    }
}
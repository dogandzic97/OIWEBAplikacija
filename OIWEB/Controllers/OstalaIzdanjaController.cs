using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Web;
using System.Web.Mvc;

namespace OIWEB.Controllers
{
    public class OstalaIzdanjaController : Controller
    {
        ObrazovniInformatorDataContext oi = new ObrazovniInformatorDataContext();
        // GET: OstalaIzdanja
        public ActionResult Index()
        {
            var ostalaIzdanja = from o in oi.OstalaIzdanjas
                                select o;
            return View(ostalaIzdanja.ToList());
        }

        [HttpGet]
        public ActionResult Create()
        {
            if (Session["Korinik"] == null)
            {
                return RedirectToAction("Page404");
            }
            return View();
        }

        [HttpPost]
        public ActionResult Create(FormCollection fc)
        {
           
            Korisnik k = (Korisnik)Session["Korisnik"];
            OstalaIzdanja izdanja = new OstalaIzdanja();
            izdanja.Naslov = fc["Naslov"];
            izdanja.Sadrzaj = fc["Sadrzaj"];
            izdanja.Opis = fc["Opis"];
            izdanja.Slika = "~/Content/images/"+fc["Slika"];
            izdanja.Cena = Convert.ToDouble(fc["Cena"]);
            izdanja.IDKorisnik = k.IDKorisnik;
            try{
                oi.OstalaIzdanjas.InsertOnSubmit(izdanja);
                oi.SubmitChanges();
                
                return RedirectToAction("Index");
                
            }
            catch
            {
                return View();
            }
        }
        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if(Session["Korisnik"]==null || id == null)
            {
                return RedirectToAction("Page404");
            }
            OstalaIzdanja izdanja = (from o in oi.OstalaIzdanjas
                                     where o.IDOstalihIzdanja == id
                                     select o).Single();
            return View(izdanja);
        }
        [HttpPost]
        public ActionResult Edit(int id,FormCollection fc)
        {
            Korisnik k = (Korisnik)Session["Korisnik"];
            OstalaIzdanja izdanja = (from o in oi.OstalaIzdanjas
                                     where o.IDOstalihIzdanja == id
                                     select o).Single();
            izdanja.Naslov = fc["Naslov"];
            izdanja.Sadrzaj = fc["Sadrzaj"];
            izdanja.Opis = fc["Opis"];
            izdanja.Cena = Convert.ToDouble(fc["Cena"]);
            izdanja.IDKorisnik = k.IDKorisnik;
            try
            {
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
            OstalaIzdanja izdanja = (from o in oi.OstalaIzdanjas
                                     where o.IDOstalihIzdanja == id
                                     select o).Single();
            try
            {
                oi.OstalaIzdanjas.DeleteOnSubmit(izdanja);
                oi.SubmitChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Details(int id)
        {
            OstalaIzdanja izdanja = (from o in oi.OstalaIzdanjas
                                     where o.IDOstalihIzdanja == id
                                     select o).Single();
            return View(izdanja);
        }

        public ActionResult Page404()
        {
            return View();
        }
    }
}
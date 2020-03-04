using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OIWEB.Controllers
{
    public class CasopisiController : Controller
    {
        ObrazovniInformatorDataContext oi = new ObrazovniInformatorDataContext();
        // GET: Casopisi
        public ActionResult Index()
        {
            var casopisi = from c in oi.Casopis
                           select c;
            return View(casopisi.ToList());
        }

       [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(FormCollection fc)
        {
            Korisnik k = (Korisnik)Session["Korisnik"];
            Casopi casopi = new Casopi();
            casopi.Naslov = fc["Naslov"];
            casopi.Tekst = fc["Tekst"];
            casopi.Sadrzaj = fc["Sadrzaj"];
            casopi.Datum = fc["Datum"];
            casopi.BrojIzdanja = Convert.ToInt32(fc["BrojIzdanja"]);
            casopi.Slika = "~/Content/images/"+fc["Slika"];
            casopi.IDKorisnika = k.IDKorisnik;

            try
            {
                oi.Casopis.InsertOnSubmit(casopi);
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
            Casopi casopis = (from c in oi.Casopis
                              where c.IDCasopis == id
                              select c).Single();
            try
            {
                oi.Casopis.DeleteOnSubmit(casopis);
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
            Casopi casopis = (from c in oi.Casopis
                              where c.IDCasopis == id
                              select c).Single();
            return View(casopis);
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            Casopi casopis = (from c in oi.Casopis
                              where c.IDCasopis == id
                              select c).Single();
            return View(casopis);
        }

        [HttpPost]
        public ActionResult Edit(int id,FormCollection fc)
        {
            Casopi casopis = (from c in oi.Casopis
                              where c.IDCasopis == id
                              select c).Single();
            casopis.Naslov = fc["Naslov"];
            casopis.Tekst = fc["Tekst"];
            casopis.Sadrzaj = fc["Sadrzaj"];
            casopis.Datum = fc["Datum"];
            casopis.BrojIzdanja = Convert.ToInt32(fc["BrojIzdanja"]);
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


    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OIWEB.Controllers
{
    public class PropisiController : Controller
    {
        ObrazovniInformatorDataContext oi = new ObrazovniInformatorDataContext();
        // GET: Propisi
        public ActionResult Index()
        {
            var propisi = from p in oi.Propis
                          select p;
            List<PropisTxt> tekst = (from t in oi.PropisTxts
                        select t).ToList();
            ViewBag.Tekst = tekst.ToList();
            return View(propisi.ToList());
        }

        [HttpGet]
        public ActionResult Create()
        {
            List<VrstaSluzbenihGlasila> vrste = (from v in oi.VrstaSluzbenihGlasilas
                                                 select v).ToList();
            List<SpecificnostiBudzetskihKorisnika> specificnosti = (from s in oi.SpecificnostiBudzetskihKorisnikas
                                                                    select s).ToList();
           
            
            ViewBag.Vrste = vrste;
            ViewBag.Specificnosti = specificnosti;
            return View();
        }
        [HttpPost]
        public ActionResult Create(FormCollection fc)
        {
            Propi propis = new Propi();
            propis.Broj = Convert.ToInt32(fc["Broj"]);
            propis.Datum = fc["Datum"];
            propis.Naslov = fc["Naslov"];
            propis.IDVrste = Convert.ToInt32(fc["IDVrstePropisa"]);
            propis.IDSBK = Convert.ToInt32(fc["IDSBK"]);
            propis.IDKorisnik = Convert.ToInt32(fc["IDKorisnik"]);
            try
            {
                oi.Propis.InsertOnSubmit(propis);
                oi.SubmitChanges();
               
            }
            catch
            {
                return View();
            }
            PropisTxt tekst = new PropisTxt();
            tekst.Tekst = fc["Tekst"];
            tekst.IDPropisa = propis.IDPropisa;
            try
            {
                oi.PropisTxts.InsertOnSubmit(tekst);
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
            Propi propis = (from p in oi.Propis
                            where p.IDPropisa == id
                            select p).Single();
           List<PropisTxt> tekst = (from t in oi.PropisTxts
                               where t.IDPropisa == id
                               select t).ToList();
            try
            {
                foreach (PropisTxt t in tekst)
                    oi.PropisTxts.DeleteOnSubmit(t);
                oi.SubmitChanges();
            }
            catch
            {
                ViewBag.Message = "Greska";
            }

            try
            {
                oi.Propis.DeleteOnSubmit(propis);
                oi.SubmitChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return RedirectToAction("Index");
            }
        }


        [HttpGet]
        public ActionResult Edit(int id)
        {
            Propi propis = (from p in oi.Propis
                            where p.IDPropisa==id
                            select p).Single();
            List<PropisTxt> tekstvoi = (from text in oi.PropisTxts
                                        where text.IDPropisa == id
                                        select text).ToList();
            ViewBag.Tekstovi = tekstvoi;
            return View(propis);
        }
        [HttpPost]
        public ActionResult Edit(int id,FormCollection fc)
        {
            Korisnik k = (Korisnik)Session["Korisnik"];
            Propi propis = (from p in oi.Propis
                            where p.IDPropisa == id
                            select p).Single();
            PropisTxt text = (from t in oi.PropisTxts
                              where t.IDPropisa == id
                              select t).Single();
            propis.Broj = Convert.ToInt32(fc["Broj"]);
            propis.Datum = fc["Datum"];
            propis.Naslov = fc["Naslov"];
            propis.IDVrste = Convert.ToInt32(fc["IDVrste"]);
            propis.IDSBK = Convert.ToInt32(fc["IDSBK"]);
            propis.IDKorisnik = k.IDKorisnik;
            
           
            text.Tekst = fc["Tekst"];
            try
            {
                oi.SubmitChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                ViewBag.Message = "Greska";
                return View();

            }
        }
    }
}
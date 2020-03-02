using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OIWEB.Controllers
{
    public class StrucnaUsavrsavanjaController : Controller
    {
        ObrazovniInformatorDataContext oi = new ObrazovniInformatorDataContext();
        // GET: StrucnaUsavrsavanja
        public ActionResult Index()
        {
            //ISPIS SVIH SEMINARA
            var seminari = from s in oi.StrucnoUsavrsavanjes
                           where s.IDVrsteStrucnogUsavrsavanja == 1
                           select s;
            return View(seminari.ToList());
        }
        //ISPIS KURSEVA I OBUKA
        public ActionResult SviKurseviIObuke()
        {
            var obuke = from o in oi.StrucnoUsavrsavanjes
                        where o.IDVrsteStrucnogUsavrsavanja == 2
                        select o;
            return View(obuke.ToList());
        }

        //ISPIS STRUICNIH USAVRSAVANJA
        public ActionResult SticanjeStrucnihZvanja()
        {
            var zvanja = from z in oi.StrucnoUsavrsavanjes
                         where z.IDVrsteStrucnogUsavrsavanja == 3
                         select z;
            return View(zvanja.ToList());
        }
        //ISPIS VEBINARA
        public ActionResult Vebinari()
        {
            var vebinari = from v in oi.StrucnoUsavrsavanjes
                           where v.IDVrsteStrucnogUsavrsavanja == 4
                           select v;
            return View(vebinari.ToList());
        }

        //KREIRANJE STRUCNOG USAVRSAVANJA:

        [HttpGet]
        public ActionResult Create()
        {
            List<VrsteStrucnogUsavrsavanja> vrste = (from v in oi.VrsteStrucnogUsavrsavanjas
                                                     select v).ToList();
            ViewBag.Vrste = vrste;
            return View();
        }

        [HttpPost]
        public ActionResult Create(FormCollection fc)
        {
            Korisnik k = (Korisnik)Session["Korisnik"];
            StrucnoUsavrsavanje usavrsavanje = new StrucnoUsavrsavanje();
            usavrsavanje.Naslov = fc["Naslov"];
            usavrsavanje.DatumOdrzavanja = fc["DatumOdrzavanja"];
            usavrsavanje.AdresaOdrzavanja = fc["AdresaOdrzavanja"];
            usavrsavanje.Program = fc["Program"];
            usavrsavanje.IDVrsteStrucnogUsavrsavanja = Convert.ToInt32(fc["IDVrsteStrucnogUsavrsavanja"]);
            usavrsavanje.IDKorisnik = k.IDKorisnik;
            try
            {
                oi.StrucnoUsavrsavanjes.InsertOnSubmit(usavrsavanje);
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
            StrucnoUsavrsavanje usavrsavanje = (from u in oi.StrucnoUsavrsavanjes
                                                where u.IDUsavrsavanja == id
                                                select u).Single();

            try
            {
                oi.StrucnoUsavrsavanjes.DeleteOnSubmit(usavrsavanje);
                oi.SubmitChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            StrucnoUsavrsavanje usavrsavanje = (from u in oi.StrucnoUsavrsavanjes
                                                where u.IDUsavrsavanja == id
                                                select u).Single();
            List<VrsteStrucnogUsavrsavanja> vrste = (from v in oi.VrsteStrucnogUsavrsavanjas
                                                     select v).ToList();
            ViewBag.Vrste = vrste;
            return View(usavrsavanje);
        }

        [HttpPost]
        public ActionResult Edit(int id, FormCollection fc)
        {
            StrucnoUsavrsavanje usavrsavanje = (from u in oi.StrucnoUsavrsavanjes
                                                where u.IDUsavrsavanja == id
                                                select u).Single();
            usavrsavanje.Naslov = fc["Naslov"];
            usavrsavanje.DatumOdrzavanja = fc["DatumOdrzavanja"];
            usavrsavanje.AdresaOdrzavanja = fc["AdresaOdrzavanja"];
            usavrsavanje.Program = fc["Program"];
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

        public ActionResult Details(int id)
        {
            StrucnoUsavrsavanje usavrsavanje = (from u in oi.StrucnoUsavrsavanjes
                                                where u.IDUsavrsavanja == id
                                                select u).Single();
            return View(usavrsavanje);
        }


    }
}
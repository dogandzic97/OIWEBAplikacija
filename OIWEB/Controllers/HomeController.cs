using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OIWEB.Controllers
{
    public class HomeController : Controller
    {
        ObrazovniInformatorDataContext oi = new ObrazovniInformatorDataContext();
        public ActionResult Index()
        {
            var clanci = from c in oi.Clancis
                         orderby c.Datum
                         select c;
            
            List<ClanciTxt> text= (from t in oi.ClanciTxts
                                   select t).ToList();
            ViewBag.Tekstovi = text;

            return View(clanci.ToList());
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        [HttpGet]
        public ActionResult Create()
        {
            List<SpecificnostiBudzetskihKorisnika> specifikacijeBK = (from s in oi.SpecificnostiBudzetskihKorisnikas
                                  select s).ToList();
            ViewBag.SpecificnostiBudzetskihKorisnika = specifikacijeBK;
            return View();
        }
        [HttpPost]
        public ActionResult Create(FormCollection fc)
        {
            Clanci c = new Clanci();
            Korisnik k = (Korisnik)Session["Korisnik"];
            c.IDKorisnik = k.IDKorisnik;
            c.IDSBK = Convert.ToInt32(fc["IDSBK"]);
            c.Datum = fc["Datum"];
            c.Tip = fc["Tip"];

            try {
                oi.Clancis.InsertOnSubmit(c);
                oi.SubmitChanges();
            }
            catch(Exception ex)
            {
                List<SpecificnostiBudzetskihKorisnika> specifikacijeBK = (from s in oi.SpecificnostiBudzetskihKorisnikas
                                                                          select s).ToList();
                ViewBag.SpecificnostiBudzetskihKorisnika = specifikacijeBK;
                ViewBag.Greska = ex.Message;
                return View();
            }

            int id = (from o in oi.Clancis
                      select o.IDClanak).Max();
            ClanciTxt cTxt = new ClanciTxt();
            cTxt.IDClanak = id;
            cTxt.Naslov = fc["Naslov"];
            cTxt.Tekst = fc["Tekst"];
            try
            {
                oi.ClanciTxts.InsertOnSubmit(cTxt);
                oi.SubmitChanges();
                return RedirectToAction("Index");

            }
            catch(Exception ex)
            {
                List<SpecificnostiBudzetskihKorisnika> specifikacijeBK = (from s in oi.SpecificnostiBudzetskihKorisnikas
                                                                          select s).ToList();
                ViewBag.SpecificnostiBudzetskihKorisnika = specifikacijeBK;
                ViewBag.Greska = ex.Message;
                return View();
            }

        }

        public ActionResult Delete(int id)
        {
            ClanciTxt tekst = (from t in oi.ClanciTxts
                               where t.IDClanak == id
                               select t).Single();
            try {
                oi.ClanciTxts.DeleteOnSubmit(tekst);
                oi.SubmitChanges();
            }
            catch
            {
                return RedirectToAction("Index");
            }

            Clanci clanak = (from c in oi.Clancis
                             where c.IDClanak == id
                             select c).Single();
            try
            {
                oi.Clancis.DeleteOnSubmit(clanak);
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
            Clanci clanak = (from c in oi.Clancis
                             where c.IDClanak == id
                             select c).Single();

            ClanciTxt tekst= (from c in oi.ClanciTxts
                              where c.IDClanak == clanak.IDClanak
                              select c).Single();
            ViewBag.ClanciTxt = tekst;
            List<SpecificnostiBudzetskihKorisnika> specifikacijeBK = (from s in oi.SpecificnostiBudzetskihKorisnikas
                                                                      select s).ToList();
            ViewBag.SpecificnostiBudzetskihKorisnika = specifikacijeBK;
            return View(clanak);
        }

        [HttpPost]
        public ActionResult Edit(int id,FormCollection fc)
        {
            Clanci c = (from cl in oi.Clancis
                        where cl.IDClanak == id
                        select cl).Single();

            Korisnik k = (Korisnik)Session["Korisnik"];
            c.IDKorisnik = k.IDKorisnik;
            c.IDSBK = Convert.ToInt32(fc["IDSBK"]);
            c.Datum = fc["Datum"];
            c.Tip = fc["Tip"];

            try
            {
              oi.SubmitChanges();
            }
            catch (Exception ex)
            {
                List<SpecificnostiBudzetskihKorisnika> specifikacijeBK = (from s in oi.SpecificnostiBudzetskihKorisnikas
                                                                          select s).ToList();
                ViewBag.SpecificnostiBudzetskihKorisnika = specifikacijeBK;
                ViewBag.Greska = ex.Message;
                return View();
            }

            ClanciTxt cTxt = new ClanciTxt();
            cTxt.Naslov = fc["Naslov"];
            cTxt.Tekst = fc["Tekst"];
            try
            {
                oi.SubmitChanges();
                return RedirectToAction("Index");

            }
            catch (Exception ex)
            {
                List<SpecificnostiBudzetskihKorisnika> specifikacijeBK = (from s in oi.SpecificnostiBudzetskihKorisnikas
                                                                          select s).ToList();
                ViewBag.SpecificnostiBudzetskihKorisnika = specifikacijeBK;
                ViewBag.Greska = ex.Message;
                return View();
            }
        }

      
    }
}
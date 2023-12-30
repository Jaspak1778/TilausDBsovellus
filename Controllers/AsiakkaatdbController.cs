using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MVC_TKsovellus_1001.Models;

namespace MVC_TKsovellus_1001.Controllers
{
    public class AsiakkaatdbController : Controller
    {
        private TilauksetEntities db = new TilauksetEntities();

        // GET: Asiakkaatdb
        public ActionResult Index()
        {
            var asiakkaat = db.Asiakkaat.Include(a => a.Postitoimipaikat);
            return View(asiakkaat.ToList());
        }

        // GET: Asiakkaatdb/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Asiakkaat asiakkaat = db.Asiakkaat.Find(id);
            if (asiakkaat == null)
            {
                return HttpNotFound();
            }
            return View(asiakkaat);
        }


        public ActionResult GetCity(string postalCode)
        {
            var city = db.Postitoimipaikat
                .Where(p => p.Postinumero == postalCode)
                .Select(p => p.Postitoimipaikka)
                .FirstOrDefault();

            return Content(city);
        }
        public ActionResult Create()
        {
            // Populate dropdown with postal codes
            ViewBag.PostinumeroList = new SelectList(db.Postitoimipaikat.Select(p => p.Postinumero).Distinct().ToList());
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AsiakasID,Nimi,Osoite,Postinumero")] Asiakkaat asiakkaat)
        {
            if (ModelState.IsValid)
            {
                //Tallentaa tiedot kantaan
                //asiakkaat olio sisältää tiedot; ID, Nimi, Osoite ja Postinumeron
                db.Asiakkaat.Add(asiakkaat);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            // If ModelState is not valid, repopulate the dropdown
            ViewBag.PostitoimipaikkaList = new SelectList(db.Postitoimipaikat, "Postitoimipaikka", "Postitoimipaikka");
            return View(asiakkaat);
        }

        // GET: Asiakkaatdb/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Asiakkaat asiakkaat = db.Asiakkaat.Find(id);
            if (asiakkaat == null)
            {
                return HttpNotFound();
            }
            ViewBag.PostinumeroList = new SelectList(db.Postitoimipaikat.Select(p => p.Postinumero).Distinct().ToList());
            return View(asiakkaat);
        }

        // POST: Asiakkaatdb/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AsiakasID,Nimi,Osoite,Postinumero,Puhelinumero,Sähköposti")] Asiakkaat asiakkaat)
        {
            if (ModelState.IsValid)
            {
                db.Entry(asiakkaat).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Postinumero = new SelectList(db.Postitoimipaikat, "Postinumero", "Postitoimipaikka", asiakkaat.Postinumero);
            return View(asiakkaat);
        }

        // GET: Asiakkaatdb/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Asiakkaat asiakkaat = db.Asiakkaat.Find(id);
            if (asiakkaat == null)
            {
                return HttpNotFound();
            }
            return View(asiakkaat);
        }

        // POST: Asiakkaatdb/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Asiakkaat asiakkaat = db.Asiakkaat.Find(id);
            db.Asiakkaat.Remove(asiakkaat);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

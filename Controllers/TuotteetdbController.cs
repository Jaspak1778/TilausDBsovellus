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
    public class TuotteetdbController : Controller
    {
        private TilauksetEntities db = new TilauksetEntities();

        [CheckSession]
        public ActionResult Index(string currentFilter1, string searchString1)
        {/*
            return View(db.Tuotteet.ToList());
         */
            var tuotteet = from p in db.Tuotteet
                           select p;
            if (!String.IsNullOrEmpty(searchString1))
            {
                tuotteet = tuotteet.Where(p => p.Nimi.Contains(searchString1));
            }
            return View(tuotteet);
        }
    
        public ActionResult CardIndex()
        {
            return View(db.Tuotteet.ToList());
        }

        [CheckSession]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tuotteet tuotteet = db.Tuotteet.Find(id);
            if (tuotteet == null)
            {
                return HttpNotFound();
            }
            return View(tuotteet);
        }

        [CheckSession]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [CheckSession]
        public ActionResult Create([Bind(Include = "TuoteID,Nimi,Ahinta,Kuva,Lisätieto_1")] Tuotteet tuotteet)
        {
            if (ModelState.IsValid)
            {
                db.Tuotteet.Add(tuotteet);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tuotteet);
        }

        [CheckSession]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tuotteet tuotteet = db.Tuotteet.Find(id);
            if (tuotteet == null)
            {
                return HttpNotFound();
            }
            return View(tuotteet);
        }
        [CheckSession]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TuoteID,Nimi,Ahinta,Kuva,Lisätieto_1")] Tuotteet tuotteet)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tuotteet).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tuotteet);
        }

        [CheckSession]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tuotteet tuotteet = db.Tuotteet.Find(id);
            if (tuotteet == null)
            {
                return HttpNotFound();
            }
            return View(tuotteet);
        }

        [CheckSession]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Tuotteet tuotteet = db.Tuotteet.Find(id);
            db.Tuotteet.Remove(tuotteet);
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

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
    public class HenkilotdbController : Controller
    {
        private TilauksetEntities db = new TilauksetEntities();

        // GET: Henkilotdb
        public ActionResult Index()
        {
            return View(db.Henkilot.ToList());
        }

        // GET: Henkilotdb/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Henkilot henkilot = db.Henkilot.Find(id);
            if (henkilot == null)
            {
                return HttpNotFound();
            }
            return View(henkilot);
        }

        // GET: Henkilotdb/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Henkilotdb/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Henkilo_id,Etunimi,Sukunimi,Osoite,Esimies,Postinumero,Sahkoposti")] Henkilot henkilot)
        {
            if (ModelState.IsValid)
            {
                db.Henkilot.Add(henkilot);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(henkilot);
        }

        // GET: Henkilotdb/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Henkilot henkilot = db.Henkilot.Find(id);
            if (henkilot == null)
            {
                return HttpNotFound();
            }
            return View(henkilot);
        }

        // POST: Henkilotdb/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Henkilo_id,Etunimi,Sukunimi,Osoite,Esimies,Postinumero,Sahkoposti")] Henkilot henkilot)
        {
            if (ModelState.IsValid)
            {
                db.Entry(henkilot).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(henkilot);
        }

        // GET: Henkilotdb/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Henkilot henkilot = db.Henkilot.Find(id);
            if (henkilot == null)
            {
                return HttpNotFound();
            }
            return View(henkilot);
        }

        // POST: Henkilotdb/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Henkilot henkilot = db.Henkilot.Find(id);
            db.Henkilot.Remove(henkilot);
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

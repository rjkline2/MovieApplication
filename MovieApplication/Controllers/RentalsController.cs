﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MovieApplication.Models;

namespace MovieApplication.Controllers
{
    public class RentalsController : Controller
    {
        private MovieDBEntities db = new MovieDBEntities();

        // GET: Rentals
        public ActionResult Index()
        {
            var rentals = db.Rentals.Include(r => r.Movy).Include(r => r.Rentals1).Include(r => r.Rental1);
            return View(rentals.ToList());
        }

        // GET: Rentals/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Rental rental = db.Rentals.Find(id);
            if (rental == null)
            {
                return HttpNotFound();
            }
            return View(rental);
        }

        // GET: Rentals/Create
        public ActionResult Create()
        {
            ViewBag.MovieID = new SelectList(db.Movies, "MovieID", "Title");
            ViewBag.RentalID = new SelectList(db.Rentals, "RentalID", "RentalID");
            ViewBag.RentalID = new SelectList(db.Rentals, "RentalID", "RentalID");
            return View();
        }

        // POST: Rentals/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "RentalID,CustomerID,MovieID,DateRented")] Rental rental)
        {
            if (ModelState.IsValid)
            {
                db.Rentals.Add(rental);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MovieID = new SelectList(db.Movies, "MovieID", "Title", rental.MovieID);
            ViewBag.RentalID = new SelectList(db.Rentals, "RentalID", "RentalID", rental.RentalID);
            ViewBag.RentalID = new SelectList(db.Rentals, "RentalID", "RentalID", rental.RentalID);
            return View(rental);
        }

        // GET: Rentals/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Rental rental = db.Rentals.Find(id);
            if (rental == null)
            {
                return HttpNotFound();
            }
            ViewBag.MovieID = new SelectList(db.Movies, "MovieID", "Title", rental.MovieID);
            ViewBag.RentalID = new SelectList(db.Rentals, "RentalID", "RentalID", rental.RentalID);
            ViewBag.RentalID = new SelectList(db.Rentals, "RentalID", "RentalID", rental.RentalID);
            return View(rental);
        }

        // POST: Rentals/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "RentalID,CustomerID,MovieID,DateRented")] Rental rental)
        {
            if (ModelState.IsValid)
            {
                db.Entry(rental).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MovieID = new SelectList(db.Movies, "MovieID", "Title", rental.MovieID);
            ViewBag.RentalID = new SelectList(db.Rentals, "RentalID", "RentalID", rental.RentalID);
            ViewBag.RentalID = new SelectList(db.Rentals, "RentalID", "RentalID", rental.RentalID);
            return View(rental);
        }

        // GET: Rentals/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Rental rental = db.Rentals.Find(id);
            if (rental == null)
            {
                return HttpNotFound();
            }
            return View(rental);
        }

        // POST: Rentals/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Rental rental = db.Rentals.Find(id);
            db.Rentals.Remove(rental);
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

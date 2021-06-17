using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CurrencyConverter.currencyData;

namespace CurrencyConverter.Controllers
{
    public class RateController : Controller
    {
        private ConvertDataEntities2 db = new ConvertDataEntities2();

        // GET: Rate
        public ActionResult Index()
        {
            var exchange_rates = db.exchange_rates.Include(e => e.currency);
            return View(exchange_rates.ToList());
        }


        // GET: Rate/Create
        public ActionResult Create()
        {
            ViewBag.currency_code = new SelectList(db.currencies, "currency_code", "currency_code");
            return View();
        }

        // POST: Rate/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,currency_code,buy_rate,sell_rate")] exchange_rates exchange_rates)
        {
            if (ModelState.IsValid)
            {
                
                db.exchange_rates.Add(exchange_rates);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.currency_code = new SelectList(db.currencies, "currency_code", "currency_code", exchange_rates.currency_code);
            return View(exchange_rates);
        }

        // GET: Rate/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            exchange_rates exchange_rates = db.exchange_rates.Find(id);
            if (exchange_rates == null)
            {
                return HttpNotFound();
            }
            ViewBag.currency_code = new SelectList(db.currencies, "currency_code", "currency_code", exchange_rates.currency_code);
            return View(exchange_rates);
        }

        // POST: Rate/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,currency_code,buy_rate,sell_rate")] exchange_rates exchange_rates)
        {
            if (ModelState.IsValid)
            {
                db.Entry(exchange_rates).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.currency_code = new SelectList(db.currencies, "currency_code", "currency_code", exchange_rates.currency_code);
            return View(exchange_rates);
        }

        // GET: Rate/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            exchange_rates exchange_rates = db.exchange_rates.Find(id);
            if (exchange_rates == null)
            {
                return HttpNotFound();
            }
            return View(exchange_rates);
        }

        // POST: Rate/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            exchange_rates exchange_rates = db.exchange_rates.Find(id);
            db.exchange_rates.Remove(exchange_rates);
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

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
    public class ConvertController : Controller
    {
        private ConvertDataEntities db = new ConvertDataEntities();

        // GET: Convert
        public ActionResult Index()
        {
            var converts = db.converts.Include(c => c.currency);
            return View(converts.ToList());
        }



        // GET: Convert/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            convert convert = db.converts.Find(id);
            if (convert == null)
            {
                return HttpNotFound();
            }
            return View(convert);
        }

        // POST: Convert/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            convert convert = db.converts.Find(id);
            db.converts.Remove(convert);
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

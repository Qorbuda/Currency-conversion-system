using CurrencyConverter.currencyData;
using CurrencyConverter.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CurrencyConverter.Controllers
{
    public class CurrencyModelController : Controller
    {
        private ConvertDataEntities db = new ConvertDataEntities();
        // GET: CurrencyModel
        public ActionResult Index()
        {
            var codeList = db.exchange_rates.Select(c => c.currency_code).ToList();
            ViewBag.data = codeList;
            return View();
        }

        [HttpPost]
        public ActionResult Index(ConvertModel model)
        {
            convert temp = new convert();
            temp.currency_from = model.CurrFrom;
            temp.currency_to = model.CurrTo;
            temp.amount_from = model.AmountFrom;
            temp.amount_to = model.AmountTo;
            temp.comment = model.Comment;
            temp.convert_date = DateTime.Now;
            db.converts.Add(temp);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public JsonResult getCoeficient(string fromCurr, string toCurr)
        {
            var fromRate = db.exchange_rates.FirstOrDefault(e => e.currency_code == fromCurr).sell_rate;
            var toRate = db.exchange_rates.FirstOrDefault(e => e.currency_code == toCurr).buy_rate;
            return Json(Math.Round(fromRate/toRate, 4), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult getAmountInGel(string toCurr, decimal amount)
        {
            var toRate = db.exchange_rates.FirstOrDefault(e => e.currency_code == toCurr).buy_rate;
            return Json(Math.Round(amount * toRate, 4), JsonRequestBehavior.AllowGet);
        }

    }
}
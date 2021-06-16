using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CurrencyConverter.Models
{
    public class ConvertModel
    {
        public string CurrFrom { get; set; }
        public string CurrTo { get; set; }
        public decimal AmountFrom { get; set; }
        public decimal AmountTo { get; set; }
        public string Comment { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyNearInv.ViewModel
{
    public class BillingCartModel
    {
        public string ItemId { get; set; }
        public decimal Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Total { get; set; }
        public string ImagePath { get; set; }
        public string ItemName { get; set; }
    }
}
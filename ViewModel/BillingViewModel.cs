using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyNearInv.ViewModel
{
    public class BillingViewModel
    {
        public Guid Itemid { get; set; }
        public string ItemName { get; set; }
        public decimal ItemPrice { get; set; }
        public string ImagePath { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public string ItemCode { get; set; }
        public Guid ShopId { get; set; }
    }
}
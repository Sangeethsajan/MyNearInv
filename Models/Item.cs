//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MyNearInv.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Item
    {
        public System.Guid Itemid { get; set; }
        public string ItemCode { get; set; }
        public string ItemName { get; set; }
        public string Description { get; set; }
        public string ImagePath { get; set; }
        public decimal ItemPrice { get; set; }
        public Nullable<System.DateTime> Expiry { get; set; }
        public Nullable<System.Guid> ShopId { get; set; }
        public int CategoryId { get; set; }
    }
}

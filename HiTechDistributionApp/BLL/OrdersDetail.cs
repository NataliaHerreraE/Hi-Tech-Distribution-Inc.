//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace HiTechDistributionApp.BLL
{
    using System;
    using System.Collections.Generic;
    
    public partial class OrdersDetail
    {
        public int OrderID { get; set; }
        public int ItemSequencial { get; set; }
        public Nullable<int> BookID { get; set; }
        public Nullable<int> Quantity { get; set; }
        public Nullable<decimal> CurrentUnitPrice { get; set; }
        public Nullable<decimal> PriceTotal { get; set; }
    
        public virtual Book Book { get; set; }
        public virtual Order Order { get; set; }
    }
}

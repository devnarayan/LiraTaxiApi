//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace LiraTaxi.Web.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class DriverNWaySale
    {
        public int DriverNWaySaleID { get; set; }
        public int DriverID { get; set; }
        public string MemberID { get; set; }
        public Nullable<decimal> Amount { get; set; }
        public System.DateTime SaleDate { get; set; }
        public string InvoiceNo { get; set; }
    
        public virtual Driver Driver { get; set; }
    }
}

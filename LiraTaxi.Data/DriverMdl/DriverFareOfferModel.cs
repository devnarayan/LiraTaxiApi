using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiraTaxi.Data.DriverMdl
{
    public class DriverFareOfferModel
    {
        public int DriverFareOfferID { get; set; }
        public int DriverID { get; set; }
        public System.DateTime OfferFrom { get; set; }
        public System.DateTime OfferTo { get; set; }
        public string Details { get; set; }
        public Nullable<System.DateTime> CrDate { get; set; }
        public bool Status { get; set; }
        public string UserName { get; set; }
        public decimal Fare { get; set; }
    }
}

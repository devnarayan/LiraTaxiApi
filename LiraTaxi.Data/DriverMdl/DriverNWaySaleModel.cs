using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiraTaxi.Data.DriverMdl
{
    public class DriverNWaySaleModel
    {
        public int DriverNWaySaleID { get; set; }
        public int DriverID { get; set; }
        public string MemberID { get; set; }
        public Nullable<decimal> Amount { get; set; }
        public System.DateTime SaleDate { get; set; }
        public string InvoiceNo { get; set; }
    }
}
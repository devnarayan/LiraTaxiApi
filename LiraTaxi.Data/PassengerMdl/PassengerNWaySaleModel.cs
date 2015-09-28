using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiraTaxi.Data.PassengerMdl
{
   public class PassengerNWaySaleModel
    {
        public int PassengerNWaySaleID { get; set; }
        public int PassengerID { get; set; }
        public string MemberID { get; set; }
        public Nullable<decimal> Amount { get; set; }
        public System.DateTime SaleDate { get; set; }
        public string InvoiceNo { get; set; }
    }
}

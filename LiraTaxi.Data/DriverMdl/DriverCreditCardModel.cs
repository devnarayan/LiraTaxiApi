using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiraTaxi.Data.DriverMdl
{
   public class DriverCreditCardModel
    {
        public int DriverCreditCardID { get; set; }
        public int DriverID { get; set; }
        public string CardType { get; set; }
        public string CardNo { get; set; }
        public string ExpiryDate { get; set; }
        public string NameOnCard { get; set; }
        public Nullable<System.DateTime> AddDate { get; set; }
    }
}

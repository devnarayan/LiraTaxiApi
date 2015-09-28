using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiraTaxi.Data.DriverMdl
{
  public  class OfferApplyModel
    {
        public int OfferApplyID { get; set; }
        public string UserName { get; set; }
        public System.DateTime ApplyDate { get; set; }
        public int DriverFareOfferID { get; set; }
    }
}

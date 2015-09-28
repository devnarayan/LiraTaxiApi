using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiraTaxi.Data.DriverMdl
{
   public class DriverRatingModel
    {
        public int DriverRatingID { get; set; }
        public int DriverID { get; set; }
        public string MemberID { get; set; }
        public int Rating { get; set; }
        public System.DateTime CrDate { get; set; }
    }
}

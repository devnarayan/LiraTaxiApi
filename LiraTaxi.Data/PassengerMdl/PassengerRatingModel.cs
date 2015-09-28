using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiraTaxi.Data.PassengerMdl
{
   public class PassengerRatingModel
    {
        public int PassengerRatignID { get; set; }
        public int PassengerID { get; set; }
        public string UserName { get; set; }
        public int Rating { get; set; }
        public System.DateTime CrDate { get; set; }
    }
}

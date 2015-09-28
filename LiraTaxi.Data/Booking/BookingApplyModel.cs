using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiraTaxi.Data.Booking
{
    public class BookingApplyModel
    {
        public int BookingID { get; set; }
        public int DriverID { get; set; }
        public System.DateTime ApplyTime { get; set; }
    }
}

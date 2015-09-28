using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiraTaxi.Data.Booking
{
    public class BookingShareApplyModel
    {
        public int BookingShareID { get; set; }
        public int PassengerID { get; set; }
        public Nullable<int> Rating { get; set; }
        public Nullable<System.DateTime> ApplyTime { get; set; }
        public bool IsAuthorized { get; set; }
    }
}

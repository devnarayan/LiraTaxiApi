using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiraTaxi.Data.Booking
{
    public class BookingShareModel  
    {
        public int BookingShareID { get; set; }
        public Nullable<int> PassengerID { get; set; }
        public string BookingType { get; set; }
        public string VehicalType { get; set; }
        public string PickupPoint { get; set; }
        public string DestinationPoint { get; set; }
        public Nullable<System.TimeSpan> PickupTime { get; set; }
        public Nullable<System.TimeSpan> DropTime { get; set; }
        public Nullable<System.DateTime> ServiceDate { get; set; }
        public Nullable<System.DateTime> ShareDate { get; set; }
        public Nullable<decimal> Amount { get; set; }
        public Nullable<decimal> DriverTip { get; set; }
        public Nullable<decimal> nWayPaid { get; set; }
    }
}
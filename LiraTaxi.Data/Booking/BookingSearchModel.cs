using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiraTaxi.Data.Booking
{
    public class BookingSearchModel
    {
        public string Stype { get; set; }
        public string SValue { get; set; }
        public Nullable<int> BookingID { get; set; }
        public Nullable<DateTime> FromDate { get; set; }
        public Nullable<DateTime> ToDate { get; set; }
    }
}

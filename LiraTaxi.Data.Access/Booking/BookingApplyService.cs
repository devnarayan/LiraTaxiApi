using LiraTaxi.Data.Booking;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiraTaxi.Data.Access.Booking
{
    public interface IBookingApplyService
    {
        DataTable GetBookigApply(int BookingID);
        int ApplyForBooking(BookingApplyModel model);
        int DeleteBookingApply(int id);
    }
    public  class BookingApplyService:IBookingApplyService
    {
        private DataFunctions dbContext;
        public BookingApplyService()
        {
            dbContext = new DataFunctions();
        }
        public DataTable GetBookigApply(int BookingID)
        {
            Hashtable HT = new Hashtable();
            HT.Add("BookingID", BookingID);
            DataTable dt= dbContext.spGetDatatable("udp_BookingApply_sel", HT);
            return dt;
        }
        public int ApplyForBooking(BookingApplyModel model)
        {
            Hashtable HT = new Hashtable();
            HT.Add("BookingID", model.BookingID);
            HT.Add("DriverID", model.DriverID);
            HT.Add("ApplyTime", model.ApplyTime);
            int i = dbContext.ExecuteSP("udp_BookingApply_ups", HT);
            return i;
        }
        public int DeleteBookingApply(int id)
        {
            Hashtable HT = new Hashtable();
            HT.Add("BookingID", id);
            HT.Add("DriverID", id);
            int i = dbContext.ExecuteSP("udp_BookingApply_del", HT);
            return i;
        }
        public void Dispose()
        {

        }
    }
}
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
    public interface IBookingShareApplyService
    {
        DataTable GetBookigShareApply(int BookingID);
        int ApplyForBookingShare(BookingShareApplyModel model);
        int DeleteBookingShareApply(int id);
    }
    public  class BookingShareApplyService
    {
        private DataFunctions dbContext;
        public BookingShareApplyService()
        {
            dbContext = new DataFunctions();
        }
        public DataTable GetBookigShareApply(int BookingID)
        {
            Hashtable HT = new Hashtable();
            HT.Add("BookingID", BookingID);
            DataTable dt= dbContext.spGetDatatable("udp_BookingShareApply_sel", HT);
            return dt;
        }
        public int ApplyForBookingShare(BookingShareApplyModel model)
        {
            Hashtable HT = new Hashtable();
            HT.Add("BookingShareID", model.BookingShareID);
            HT.Add("PassengerID", model.PassengerID);
            HT.Add("Rating", model.Rating);
            HT.Add("ApplyTime", model.ApplyTime);
            HT.Add("IsAuthorized", model.IsAuthorized);
            int i = dbContext.ExecuteSP("udp_BookingShareApply_ups", HT);
            return i;
        }
        public int DeleteBookingShareApply(int id)
        {
            Hashtable HT = new Hashtable();
            HT.Add("BookingID", id);
            HT.Add("DriverID", id);
            int i = dbContext.ExecuteSP("udp_BookingShareApply_del", HT);
            return i;
        }
        public void Dispose()
        {

        }
    }
}
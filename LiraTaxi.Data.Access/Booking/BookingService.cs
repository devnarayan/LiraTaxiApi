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
    public interface IBookingService
    {
        DataTable GetBookings();
        DataTable GetBooking(int id);
        DataTable GetBookingSearch(BookingSearchModel model);
        int AddBooking(BookingModel model);
        int EditBooking(BookingModel model);
        int DeleteBooking(int id);
    }
    public class BookingService : IBookingService
    {
        private DataFunctions dbContext;
        public BookingService()
        {
            dbContext = new DataFunctions();
        }
        public DataTable GetBookings()
        {
            Hashtable HT = new Hashtable();
            DataTable dt= dbContext.spGetDatatable("udp_Booking_lst", HT);
            return dt;
        }
        public DataTable GetBooking(int id)
        {
            Hashtable HT = new Hashtable();
            HT.Add("SType", "BookingID");
            HT.Add("SValue", id);
            HT.Add("BookingID", id);
            DataTable dt = dbContext.spGetDatatable("udp_Booking_sel", HT);
            return dt;
        }
        public DataTable GetBookingSearch(BookingSearchModel model)
        {
            Hashtable HT = new Hashtable();
            HT.Add("SType", model.Stype);
            HT.Add("SValue", model.SValue);
            HT.Add("BookingID", model.BookingID);
            HT.Add("FromDate", model.FromDate);
            HT.Add("ToDate", model.ToDate);
            DataTable dt = dbContext.spGetDatatable("udp_Booking_sel", HT);
            return dt;
        }
        public int AddBooking(BookingModel model)
        {
            Hashtable HT = new Hashtable();
            HT.Add("BookingID", 0);
            HT.Add("DriverID", model.DriverID);
            HT.Add("PassengerID", model.PassengerID);
            HT.Add("BookingType", model.BookingType);
            HT.Add("VehicalType", model.VehicalType);
            HT.Add("PickupPoint", model.PickupPoint);
            HT.Add("DestinationPoint", model.DestinationPoint);
            HT.Add("PickupTime", model.PickupTime);
            HT.Add("DropTime", model.DropTime);
            HT.Add("ServiceDate", model.ServiceDate);
            HT.Add("BookingDate", model.BookingDate);
            HT.Add("Amount", model.Amount);
            HT.Add("DriverTip", model.DriverTip);
            HT.Add("nWayPaid", model.nWayPaid);
            HT.Add("Status", model.Status);
            HT.Add("BarCode", model.BarCode);
            int i = dbContext.ExecuteSP("udp_Booking_ups", HT);
            return i;
        }
        public int EditBooking(BookingModel model)
        {
            Hashtable HT = new Hashtable();
            HT.Add("BookingID", model.BookingID);
            HT.Add("DriverID", model.DriverID);
            HT.Add("PassengerID", model.PassengerID);
            HT.Add("BookingType", model.BookingType);
            HT.Add("VehicalType", model.VehicalType);
            HT.Add("PickupPoint", model.PickupPoint);
            HT.Add("DestinationPoint", model.DestinationPoint);
            HT.Add("PickupTime", model.PickupTime);
            HT.Add("DropTime", model.DropTime);
            HT.Add("ServiceDate", model.ServiceDate);
            HT.Add("BookingDate", model.BookingDate);
            HT.Add("Amount", model.Amount);
            HT.Add("DriverTip", model.DriverTip);
            HT.Add("nWayPaid", model.nWayPaid);
            HT.Add("Status", model.Status);
            HT.Add("BarCode", model.BarCode);
            int i = dbContext.ExecuteSP("udp_Booking_ups", HT);
            return i;
        }
        public int DeleteBooking(int id)
        {
            Hashtable HT = new Hashtable();
            HT.Add("BookingID",id);
            int i = dbContext.ExecuteSP("udp_Booking_del", HT);
            return i;
        }
        public void Dispose()
        {
          
        }
    }
}

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
    public interface IBookingShareService
    {
        DataTable GetBookingsShareList(int BookingID);
        DataTable GetBookingShareDetails(int BookingShareid);
        int AddBookingShare(BookingShareModel model);
        int EditBookingShare(BookingShareModel model);
        int DeleteBookingShare(int id);
    }
    public class BookingShareService : IBookingShareService
    {
        private DataFunctions dbContext;
        public BookingShareService()
        {
            dbContext = new DataFunctions();
        }
        public DataTable GetBookingsShareList(int PassengerID)
        {
            Hashtable HT = new Hashtable();
            HT.Add("ID", PassengerID);
            HT.Add("Type", "PassengerID");
            DataTable dt = dbContext.spGetDatatable("udp_BookingShare_sel", HT);
            return dt;
        }
        public DataTable GetBookingShareDetails(int BookingShareID)
        {
            Hashtable HT = new Hashtable();
            HT.Add("ID", BookingShareID);
            HT.Add("Type", "BookingShareID");
            DataTable dt = dbContext.spGetDatatable("udp_BookingShare_sel", HT);
            return dt;
        }
        public int AddBookingShare(BookingShareModel model)
        {
            Hashtable HT = new Hashtable();
            HT.Add("BookingShareID", model.BookingShareID);
            HT.Add("PassengerID", model.PassengerID);
            HT.Add("BookingType", model.BookingType);
            HT.Add("VehicalType", model.VehicalType);
            HT.Add("PickupPoint", model.PickupPoint);
            HT.Add("DestinationPoint", model.DestinationPoint);
            HT.Add("PickupTime", model.PickupTime);
            HT.Add("DropTime", model.DropTime);
            HT.Add("ServiceDate", model.ServiceDate);
            HT.Add("ShareDate", model.ShareDate);
            HT.Add("Amount", model.Amount);
            HT.Add("DriverTip", model.DriverTip);
            HT.Add("nWayPaid", model.nWayPaid);
        int i = dbContext.ExecuteSP("udp_BookingShare_ups", HT);
            return i;
        }
        public int EditBookingShare(BookingShareModel model)
        {
            Hashtable HT = new Hashtable();
            HT.Add("BookingShareID", model.BookingShareID);
            HT.Add("PassengerID", model.PassengerID);
            HT.Add("BookingType", model.BookingType);
            HT.Add("VehicalType", model.VehicalType);
            HT.Add("PickupPoint", model.PickupPoint);
            HT.Add("DestinationPoint", model.DestinationPoint);
            HT.Add("PickupTime", model.PickupTime);
            HT.Add("DropTime", model.DropTime);
            HT.Add("ServiceDate", model.ServiceDate);
            HT.Add("ShareDate", model.ShareDate);
            HT.Add("Amount", model.Amount);
            HT.Add("DriverTip", model.DriverTip);
            HT.Add("nWayPaid", model.nWayPaid);
            int i = dbContext.ExecuteSP("udp_BookingShare_ups", HT);
            return i;
        }
        public int DeleteBookingShare(int id)
        {
            Hashtable HT = new Hashtable();
            HT.Add("BookingShareID",id);
            int i = dbContext.ExecuteSP("udp_BookingShare_del", HT);
            return i;
        }
        public void Dispose()
        {
            //dbContext = null;
        }
    }
}

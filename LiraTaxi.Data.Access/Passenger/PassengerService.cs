using LiraTaxi.Data.PassengerMdl;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiraTaxi.Data.Access.Passenger
{
    public interface IPassengerService
    {
        DataTable GetPassengerList();
        DataTable GetPassengerByID(int PassengerID);
        DataTable GetPassengerSearch(int PassengerID, string SType, string SValue);
        int AddPassenger(PassengerModel model);
        int EditPassenger(PassengerModel model);
        int DeletePassenger(int PassengerID);
    }
    public class PassengerService : IPassengerService
    {
        private DataFunctions dbContext;
        public PassengerService()
        {
            dbContext = new DataFunctions();
        }
        public DataTable GetPassengerList()
        {
            Hashtable HT = new Hashtable();
            DataTable dt = dbContext.spGetDatatable("udp_Passenger_lst", HT);
            return dt;
        }
        public DataTable GetPassengerByID(int PassengerID)
        {
            Hashtable HT = new Hashtable();
            HT.Add("PassengerID", PassengerID);
            DataTable dt = dbContext.spGetDatatable("udp_Passenger_sel", HT);
            return dt;
        }
        public DataTable GetPassengerSearch(int PassengerID, string SType, string SValue)
        {
            Hashtable HT = new Hashtable();
            HT.Add("PassengerID", PassengerID);
            HT.Add("SType", SType);
            HT.Add("SValue", SValue);
            DataTable dt = dbContext.spGetDatatable("udp_Passenger_src", HT);
            return dt;
        }
        public int AddPassenger(PassengerModel model)
        {
            Hashtable HT = new Hashtable();
            HT.Add("PassengerID", 0);
            HT.Add("UserId", model.UserId);
            HT.Add("FirstName", model.FirstName);
            HT.Add("Email", model.Email);
            HT.Add("Mobile", model.Mobile);
            HT.Add("Password", model.Password);
            HT.Add("Language", model.Language);
            HT.Add("CrDate", model.CrDate);
            HT.Add("nWayPoint", model.nWayPoint);
            HT.Add("nWayInfo", model.nWayInfo);
            HT.Add("Paypal", model.Paypal);
            HT.Add("Latitute", model.Latitude);
            HT.Add("Longitude", model.Longitude);
            HT.Add("UpdateTime", model.UpdateTime);
            HT.Add("DeviceType", model.DeviceType);
            HT.Add("DeviceToken", model.DeviceToken);
            HT.Add("PhotoUrl", model.PhotoUrl);
            HT.Add("CreateTimeM", model.CreateTimeM);
            HT.Add("UpdateTimeM", model.UpdateTimeM);
            HT.Add("VarifyTime", model.VarifyTime);
            HT.Add("PhoneVerified", model.PhoneVerified);
            HT.Add("PhoneCode", model.PhoneCode);
            int i = dbContext.ExecuteSP("udp_Passenger_ups", HT);
            return i;
        }
        public int EditPassenger(PassengerModel model)
        {
            Hashtable HT = new Hashtable();
            HT.Add("PassengerID", model.PassengerID);
            HT.Add("UserId", model.UserId);
            HT.Add("FirstName", model.FirstName);
            HT.Add("Email", model.Email);
            HT.Add("Mobile", model.Mobile);
            HT.Add("Password", model.Password);
            HT.Add("Language", model.Language);
            HT.Add("CrDate", model.CrDate);
            HT.Add("nWayPoint", model.nWayPoint);
            HT.Add("nWayInfo", model.nWayInfo);
            HT.Add("Paypal", model.Paypal);
            HT.Add("Latitute", model.Latitude);
            HT.Add("Longitude", model.Longitude);
            HT.Add("UpdateTime", model.UpdateTime);
            HT.Add("DeviceType", model.DeviceType);
            HT.Add("DeviceToken", model.DeviceToken);
            HT.Add("PhotoUrl", model.PhotoUrl);
            int i = dbContext.ExecuteSP("udp_Passenger_ups", HT);
            return i;
        }
        public int DeletePassenger(int PassengerID)
        {
            Hashtable HT = new Hashtable();
            HT.Add("PassengerID", PassengerID);
            int i = dbContext.ExecuteSP("udp_Passenger_del", HT);
            return i;
        }
        public void Dispose()
        {

        }
    }
}
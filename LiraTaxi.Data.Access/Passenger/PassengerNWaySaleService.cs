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
    public interface IPassengerNWaySaleService
    {
        int AddPassengerNWaySale(PassengerNWaySaleModel model);
        int EditPassengerNWaySale(PassengerNWaySaleModel model);
        int DeletePassengerNWaySale(int PassengerNWaySaleID);
        DataTable GetNWaySaleList(int DriverID);
        DataTable GetNWaySale(int DriverID);
    }
    public class PassengerNWaySaleService : IPassengerNWaySaleService
    {
        private DataFunctions dbContext;
        public PassengerNWaySaleService()
        {
            dbContext = new DataFunctions();
        }
        public int AddPassengerNWaySale(PassengerNWaySaleModel model)
        {
            Hashtable HT = new Hashtable();
            HT.Add("PassengerNWaySaleID", 0);
            HT.Add("PassengerID", model.PassengerID);
            HT.Add("MemberID", model.MemberID);
            HT.Add("Amount", model.Amount);
            HT.Add("SaleDate", model.SaleDate);
            HT.Add("InvoiceNo", model.InvoiceNo);
            int i = dbContext.ExecuteSP("udp_PassengerNWaySale_ups", HT);
            return i;
        }
        public int EditPassengerNWaySale(PassengerNWaySaleModel model)
        {
            Hashtable HT = new Hashtable();
            HT.Add("PassengerNWaySaleID", model.PassengerNWaySaleID);
            HT.Add("PassengerID", model.PassengerID);
            HT.Add("MemberID", model.MemberID);
            HT.Add("Amount", model.Amount);
            HT.Add("SaleDate", model.SaleDate);
            HT.Add("InvoiceNo", model.InvoiceNo);
            int i = dbContext.ExecuteSP("udp_PassengerNWaySale_ups", HT);
            return i;
        }
        public int DeletePassengerNWaySale(int PassengerNWaySaleID)
        {
            Hashtable HT = new Hashtable();
            HT.Add("PassengerNWaySaleID", PassengerNWaySaleID);
            int i = dbContext.ExecuteSP("udp_PassengerNWaySale_del", HT);
            return i;
        }
        public DataTable GetNWaySaleList(int PassengerID)
        {
            Hashtable HT = new Hashtable();
            HT.Add("PassengerID", PassengerID);
            DataTable dt = dbContext.spGetDatatable("udp_PassengerNWaySale_lst", HT);
            return dt;
        }
        public DataTable GetNWaySale(int PassengerNWaySaleID)
        {
            Hashtable HT = new Hashtable();
            HT.Add("PassengerNWaySaleID", PassengerNWaySaleID);
            DataTable dt = dbContext.spGetDatatable("udp_PassengerNWaySale_sel", HT);
            return dt;
        }
        public void Dispose()
        {

        }
    }
}

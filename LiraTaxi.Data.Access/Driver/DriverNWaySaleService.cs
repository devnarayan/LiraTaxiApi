using LiraTaxi.Data.DriverMdl;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiraTaxi.Data.Access.Driver
{
    public interface IDriverNWaySaleService
    {
        int AddDriverNWaySale(DriverNWaySaleModel model);
        int EditDriverNWaySale(DriverNWaySaleModel model);
        int DeleteDriverNWaySale(int DriverNWaySaleID);
        DataTable GetNWaySaleList(int DriverID);
        DataTable GetNWaySale(int DriverID);
    }
    public class DriverNWaySaleService : IDriverNWaySaleService
    {
        private DataFunctions dbContext;
        public DriverNWaySaleService()
        {
            dbContext = new DataFunctions();
        }
        public int AddDriverNWaySale(DriverNWaySaleModel model)
        {
            Hashtable HT = new Hashtable();
            HT.Add("DriverNWaySaleID", 0);
            HT.Add("DriverID", model.DriverID);
            HT.Add("MemberID", model.MemberID);
            HT.Add("Amount", model.Amount);
            HT.Add("SaleDate", model.SaleDate);
            HT.Add("InvoiceNo", model.InvoiceNo);
            int i = dbContext.ExecuteSP("udp_DriverNWaySale_ups", HT);
            return i;
        }
        public int EditDriverNWaySale(DriverNWaySaleModel model)
        {
            Hashtable HT = new Hashtable();
            HT.Add("DriverNWaySaleID", model.DriverNWaySaleID);
            HT.Add("DriverID", model.DriverID);
            HT.Add("MemberID", model.MemberID);
            HT.Add("Amount", model.Amount);
            HT.Add("SaleDate", model.SaleDate);
            HT.Add("InvoiceNo", model.InvoiceNo);
            int i = dbContext.ExecuteSP("udp_DriverNWaySale_ups", HT);
            return i;
        }
        public int DeleteDriverNWaySale(int DriverNWaySaleID)
        {
            Hashtable HT = new Hashtable();
            HT.Add("DriverNWaySaleID", DriverNWaySaleID);
            int i = dbContext.ExecuteSP("udp_DriverNWaySale_del", HT);
            return i;
        }
        public DataTable GetNWaySaleList(int DriverID)
        {
            Hashtable HT = new Hashtable();
            HT.Add("DriverID", DriverID);
            DataTable dt = dbContext.spGetDatatable("udp_DriverNWaySale_lst", HT);
            return dt;
        }
        public DataTable GetNWaySale(int DriverNWaySaleID)
        {
            Hashtable HT = new Hashtable();
            HT.Add("DriverNWaySaleID", DriverNWaySaleID);
            DataTable dt = dbContext.spGetDatatable("udp_DriverNWaySale_sel", HT);
            return dt;
        }
        public void Dispose()
        {

        }
    }
}

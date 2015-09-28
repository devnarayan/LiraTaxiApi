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
    public interface IDriverFareOfferService
    {
        int AddDriverFareOffer(DriverFareOfferModel model);
        int EditDriverFareOffer(DriverFareOfferModel model);
        int DeleteDriverFareOffer(int DriverFareOfferID);
        DataTable GetFareOfferList(int DriverID);
        DataTable GetFareOffer(int DriverFareOfferID);
    }
    public class DriverFareOfferService : IDriverFareOfferService
    {
        private DataFunctions dbContext;
        public DriverFareOfferService()
        {
            dbContext = new DataFunctions();
        }
        public int AddDriverFareOffer(DriverFareOfferModel model)
        {
            Hashtable HT = new Hashtable();
            HT.Add("DriverFareOfferID", 0);
            HT.Add("DriverID", model.DriverID);
            HT.Add("OfferFrom", model.OfferFrom);
            HT.Add("OfferTo", model.OfferTo);
            HT.Add("Details", model.Details);
            HT.Add("CrDate", model.CrDate);
            HT.Add("Status", model.Status);
            HT.Add("UserName", model.UserName);
            HT.Add("Fare", model.Fare);
            int i= dbContext.ExecuteSP("udp_DriverFareOffer_ups", HT);
            return i;
        }
        public int EditDriverFareOffer(DriverFareOfferModel model)
        {
            Hashtable HT = new Hashtable();
            HT.Add("DriverFareOfferID", model.DriverFareOfferID);
            HT.Add("DriverID", model.DriverID);
            HT.Add("OfferFrom", model.OfferFrom);
            HT.Add("OfferTo", model.OfferTo);
            HT.Add("Details", model.Details);
            HT.Add("CrDate", model.CrDate);
            HT.Add("Status", model.Status);
            HT.Add("UserName", model.UserName);
            HT.Add("Fare", model.Fare);
            int i = dbContext.ExecuteSP("udp_DriverFareOffer_ups", HT);
            return i;
        }
        public int DeleteDriverFareOffer(int DriverFareOfferID)
        {
            Hashtable HT = new Hashtable();
            HT.Add("DriverFareOfferID", DriverFareOfferID);
            int i = dbContext.ExecuteSP("udp_DriverFareOffer_del", HT);
            return i;
        }
        public DataTable GetFareOfferList(int DriverID)
        {
            Hashtable HT = new Hashtable();
            HT.Add("DriverID", DriverID);
            DataTable dt = dbContext.spGetDatatable("udp_DriverFareOffer_lst", HT);
            return dt;
        }
        public DataTable GetFareOffer(int DriverFareOfferID)
        {
            Hashtable HT = new Hashtable();
            HT.Add("DriverFareOfferID", DriverFareOfferID);
            DataTable dt = dbContext.spGetDatatable("udp_DriverFareOffer_sel", HT);
            return dt;
        }
        public void Dispose()
        {

        }
    }
}

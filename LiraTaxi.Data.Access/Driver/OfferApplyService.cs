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
    public interface IOfferApplyService
    {
        int AddOfferApply(OfferApplyModel model);
        int EditOfferApply(OfferApplyModel model);
        int DeleteOfferApply(int OfferApplyID);
        DataTable GetOfferApplyList(int DriverFareOfferID);
        DataTable GetOfferApply(int OfferApplyID);
    }
    public class OfferApplyService : IOfferApplyService
    {
         private DataFunctions dbContext = new DataFunctions();
        public OfferApplyService()
        {
            dbContext = new DataFunctions();
        }
        public int AddOfferApply(OfferApplyModel model)
        {
            Hashtable HT = new Hashtable();
            HT.Add("OfferApplyID", 0);
            HT.Add("UserName", model.UserName);
            HT.Add("ApplyDate", model.ApplyDate);
            HT.Add("DriverFareOfferID", model.DriverFareOfferID);
            int i= dbContext.ExecuteSP("udp_OfferApply_ups", HT);
            return i;
        }
        public int EditOfferApply(OfferApplyModel model)
        {
            Hashtable HT = new Hashtable();
            HT.Add("OfferApplyID", model.OfferApplyID);
            HT.Add("UserName", model.UserName);
            HT.Add("ApplyDate", model.ApplyDate);
            HT.Add("DriverFareOfferID", model.DriverFareOfferID);
            int i = dbContext.ExecuteSP("udp_OfferApply_ups", HT);
            return i;
        }
        public int DeleteOfferApply(int OfferApplyID)
        {
            Hashtable HT = new Hashtable();
            HT.Add("OfferApplyID", OfferApplyID);
            int i = dbContext.ExecuteSP("udp_OfferApply_del", HT);
            return i;
        }
        public DataTable GetOfferApplyList(int DriverFareOfferID)
        {
            Hashtable HT = new Hashtable();
            HT.Add("DriverFareOfferID", DriverFareOfferID);
            DataTable dt = dbContext.spGetDatatable("udp_OfferApply_lst", HT);
            return dt;
        }
        public DataTable GetOfferApply(int OfferApplyID)
        {
            Hashtable HT = new Hashtable();
            HT.Add("OfferApplyID", OfferApplyID);
            DataTable dt = dbContext.spGetDatatable("udp_OfferApply_sel", HT);
            return dt;
        }
        public void Dispose()
        {

        }
    }
}
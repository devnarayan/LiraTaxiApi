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
    public interface IDriverRatingService
    {
        int AddDriverRating(DriverRatingModel model);
        int DeleteDriverRating(int DriverRatingID);
        DataTable GetDriverRatingList(int DriverID);
        DataTable GetDriverRating(int DriverID);
    }
    public class DriverRatingService:IDriverRatingService
    {
        private  DataFunctions dbContext;
        public DriverRatingService()
        {
            dbContext = new DataFunctions();
        }
        public int AddDriverRating(DriverRatingModel model)
        {
            Hashtable HT = new Hashtable();
            HT.Add("DriverRatingID", model.DriverRatingID);
            HT.Add("DriverID", model.DriverID);
            HT.Add("MemberID", model.MemberID);
            HT.Add("Rating", model.Rating);
            HT.Add("CrDate", model.CrDate);
            int i= dbContext.ExecuteSP("udp_DriverRating_ups", HT);
            return i;
        }
        public int DeleteDriverRating(int DriverRatingID)
        {
            Hashtable HT = new Hashtable();
            HT.Add("DriverRatingID", DriverRatingID);
            int i = dbContext.ExecuteSP("udp_DriverRating_del", HT);
            return i;
        }
        public DataTable GetDriverRatingList(int DriverID)
        {
            Hashtable HT = new Hashtable();
            HT.Add("DriverID", DriverID);
            DataTable dt = dbContext.spGetDatatable("udp_DriverRating_lst", HT);
            return dt;
        }
        public DataTable GetDriverRating(int DriverID)
        {
            Hashtable HT = new Hashtable();
            HT.Add("DriverID", DriverID);
            DataTable dt = dbContext.spGetDatatable("udp_DriverRating_sum", HT);
            return dt;
        }
        public void Dispose()
        {

        }
    }
}

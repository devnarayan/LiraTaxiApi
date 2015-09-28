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
    public interface IPassengerRatingService
    {
        int AddPassengerRating(PassengerRatingModel model);
        int DeletePassengerRating(int PassengerRatingID);
        DataTable GetPassengerRatingList(int DriverID);
        DataTable GetPassengerRating(int DriverID);
    }
    public class PassengerRatingService:IPassengerRatingService
    {
        private DataFunctions dbContext;
        public PassengerRatingService()
        {
            dbContext = new DataFunctions();
        }
        public int AddPassengerRating(PassengerRatingModel model)
        {
            Hashtable HT = new Hashtable();
            HT.Add("PassengerRatingID", model.PassengerRatignID);
            HT.Add("PassengerID", model.PassengerID);
            HT.Add("UserName", model.UserName);
            HT.Add("Rating", model.Rating);
            HT.Add("CrDate", model.CrDate);
            int i = dbContext.ExecuteSP("udp_PassengerRating_ups", HT);
            return i;
        }
        public int DeletePassengerRating(int PassengerRatingID)
        {
            Hashtable HT = new Hashtable();
            HT.Add("PassengerRatingID", PassengerRatingID);
            int i = dbContext.ExecuteSP("udp_PassengerRating_del", HT);
            return i;
        }
        public DataTable GetPassengerRatingList(int PassengerID)
        {
            Hashtable HT = new Hashtable();
            HT.Add("PassengerID", PassengerID);
            DataTable dt = dbContext.spGetDatatable("udp_PassengerRating_lst", HT);
            return dt;
        }
        public DataTable GetPassengerRating(int PassengerID)
        {
            Hashtable HT = new Hashtable();
            HT.Add("PassengerID", PassengerID);
            DataTable dt = dbContext.spGetDatatable("udp_PassengerRating_sum", HT);
            return dt;
        }
        public void Dispose()
        {

        }
    }

}

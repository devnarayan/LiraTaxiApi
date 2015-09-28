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
    public interface IPassengerCreditCardService
    {
        int AddPassengerCreditCard(PassengerCreditCardModel model);
        int EditPassengerCreditCard(PassengerCreditCardModel model);
        int DeletePassengerCreditCard(int PassengerCreditCardID);
        DataTable GetCreditCardList(int DriverID);
        DataTable GetCreditCard(int CreditCardID);
    }
    public class PassengerCreditCardService
    {
        private DataFunctions dbContext;
        public PassengerCreditCardService()
        {
            dbContext = new DataFunctions();
        }
        public int AddPassengerCreditCard(PassengerCreditCardModel model)
        {
            Hashtable HT = new Hashtable();
            HT.Add("PassengerCreditCardID", 0);
            HT.Add("PassengerID", model.PassengerID);
            HT.Add("CardType", model.CardType);
            HT.Add("CardNo", model.CardNo);
            HT.Add("ExpiryDate", model.ExpiryDate);
            HT.Add("NameOnCard", model.NameOnCard);
            HT.Add("AddDate", model.AddDate);
            int i = dbContext.ExecuteSP("udp_PassengerCreditCard_ups", HT);
            return i;
        }
        public int EditPassengerCreditCard(PassengerCreditCardModel model)
        {
            Hashtable HT = new Hashtable();
            HT.Add("PassengerCreditCardID", model.PassengerCreditCardID);
            HT.Add("PassengerID", model.PassengerID);
            HT.Add("CardType", model.CardType);
            HT.Add("CardNo", model.CardNo);
            HT.Add("ExpiryDate", model.ExpiryDate);
            HT.Add("NameOnCard", model.NameOnCard);
            HT.Add("AddDate", model.AddDate);
            int i = dbContext.ExecuteSP("udp_PassengerCreditCard_ups", HT);
            return i;
        }
        public int DeletePassengerCreditCard(int PassengerCreditCardID)
        {
            Hashtable HT = new Hashtable();
            HT.Add("PassengerCreditCardID", PassengerCreditCardID);
            int i = dbContext.ExecuteSP("udp_PassengerCreditCard_del", HT);
            return i;
        }
        public DataTable GetCreditCardList(int PassengerID)
        {
            Hashtable HT = new Hashtable();
            HT.Add("PassengerID", PassengerID);
            DataTable dt = dbContext.spGetDatatable("udp_PassengerCreditCard_lst", HT);
            return dt;
        }
        public DataTable GetCreditCard(int CreditCardID)
        {
            Hashtable HT = new Hashtable();
            HT.Add("PassengerCreditCardID", CreditCardID);
            DataTable dt = dbContext.spGetDatatable("udp_PassengerCreditCard_sel", HT);
            return dt;
        }
        public void Dispose()
        {

        }
    }
}
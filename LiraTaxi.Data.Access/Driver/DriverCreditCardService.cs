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
    public interface IDriverCreditCardService
    {
        int AddDriverCreditCard(DriverCreditCardModel model);
        int EditDriverCreditCard(DriverCreditCardModel model);
        int DeleteDriverCreditCard(int DriverCreditCardID);
        DataTable GetCreditCardList(int DriverID);
        DataTable GetCreditCard(int CreditCardID);
    }
    public class DriverCreditCardService
    {
        private DataFunctions dbContext;
        public DriverCreditCardService()
        {
            dbContext = new DataFunctions();
        }
        public int AddDriverCreditCard(DriverCreditCardModel model)
        {
            Hashtable HT = new Hashtable();
            HT.Add("DriverCreditCardID", 0);
            HT.Add("DriverID", model.DriverID);
            HT.Add("CardType", model.CardType);
            HT.Add("CardNo", model.CardNo);
            HT.Add("ExpiryDate", model.ExpiryDate);
            HT.Add("NameOnCard", model.NameOnCard);
            HT.Add("AddDate", model.AddDate);
            int i= dbContext.ExecuteSP("udp_DriverCreditCard_ups", HT);
            return i;
        }
        public int EditDriverCreditCard(DriverCreditCardModel model)
        {
            Hashtable HT = new Hashtable();
            HT.Add("DriverCreditCardID", model.DriverCreditCardID);
            HT.Add("DriverID", model.DriverID);
            HT.Add("CardType", model.CardType);
            HT.Add("CardNo", model.CardNo);
            HT.Add("ExpiryDate", model.ExpiryDate);
            HT.Add("NameOnCard", model.NameOnCard);
            HT.Add("AddDate", model.AddDate);
            int i = dbContext.ExecuteSP("udp_DriverCreditCard_ups", HT);
            return i;
        }
        public int DeleteDriverCreditCard(int DriverCreditCardID)
        {
            Hashtable HT = new Hashtable();
            HT.Add("DriverCreditCardID", DriverCreditCardID);
            int i = dbContext.ExecuteSP("udp_DriverCreditCard_del", HT);
            return i;
        }
        public DataTable GetCreditCardList(int DriverID)
        {
            Hashtable HT = new Hashtable();
            HT.Add("DriverID", DriverID);
            DataTable dt = dbContext.spGetDatatable("udp_DriverCreditCard_lst", HT);
            return dt;
        }
        public DataTable GetCreditCard(int CreditCardID)
        {
            Hashtable HT = new Hashtable();
            HT.Add("DriverCreditCardID", CreditCardID);
            DataTable dt = dbContext.spGetDatatable("udp_DriverCreditCard_sel", HT);
            return dt;
        }
        public void Dispose()
        {

        }
    }
}
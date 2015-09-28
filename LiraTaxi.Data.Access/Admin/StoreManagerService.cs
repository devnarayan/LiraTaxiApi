using LiraTaxi.Data.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiraTaxi.Data.Access.Admin
{
   
    public interface IStoreManagerService
    {
        DataTable GetStoreManagerSearch(StoreManagerSearchModel model);
        int AddStoreManager(StoreManagerModel model);
        int EditStoreManager(StoreManagerModel model);
        int DeleteStoreManager(int StoreManagerID);
    }
    public class StoreManagerService : IStoreManagerService
    {
        private DataFunctions dbContext;
        public StoreManagerService()
        {
            dbContext = new DataFunctions();
        }
        public DataTable GetStoreManagerSearch(StoreManagerSearchModel model)
        {
            Hashtable HT = new Hashtable();
            HT.Add("StoreManagerID", model.StoreManagerID);
            HT.Add("SType", model.SType);
            HT.Add("SValue", model.SValue);
            DataTable dt = dbContext.spGetDatatable("udp_StoreManager_sel", HT);
            return dt;
        }
        public int AddStoreManager(StoreManagerModel model)
        {
            Hashtable HT = new Hashtable();
            HT.Add("StoreManagerID", 0);
            HT.Add("UserName", model.Name);
            HT.Add("StoreManagerName",model.RegistrationNo);
            HT.Add("Email", model.RegDate);
            HT.Add("Mobile", model.Status);
            int i = dbContext.ExecuteSP("udp_StoreManager_ups", HT);
            return i;
        }
        public int EditStoreManager(StoreManagerModel model)
        {
            Hashtable HT = new Hashtable();
            HT.Add("StoreManagerID", model.StoreManagerID);
            HT.Add("UserName", model.Name);
            HT.Add("StoreManagerName", model.RegistrationNo);
            HT.Add("Email", model.RegDate);
            HT.Add("Mobile", model.Status);
            int i = dbContext.ExecuteSP("udp_StoreManager_ups", HT);
            return i;
        }
        public int DeleteStoreManager(int StoreManagerID)
        {
            Hashtable HT = new Hashtable();
            HT.Add("StoreManagerID", StoreManagerID);
            int i = dbContext.ExecuteSP("udp_StoreManager_del", HT);
            return i;
        }
        public void Dispose()
        {

        }
    }
}

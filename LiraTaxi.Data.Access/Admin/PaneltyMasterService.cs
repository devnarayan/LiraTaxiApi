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
    public interface IPaneltyMasterService
    {
        DataTable GetPaneltyMasterList();
        DataTable GetPaneltyMasterByID(int PaneltyMasterID);
        int AddPaneltyMaster(PaneltyMasterModel model);
        int EditPaneltyMaster(PaneltyMasterModel model);
        int DeletePaneltyMaster(int PaneltyMasterID);
    }
    public class PaneltyMasterService : IPaneltyMasterService
    {
        private DataFunctions dbContext;
        public PaneltyMasterService()
        {
            dbContext = new DataFunctions();
        }
        public DataTable GetPaneltyMasterList()
        {
            Hashtable HT = new Hashtable();
            DataTable dt = dbContext.spGetDatatable("udp_PaneltyMaster_lst", HT);
            return dt;
        }
        public DataTable GetPaneltyMasterByID(int PaneltyMasterID)
        {
            Hashtable HT = new Hashtable();
            HT.Add("PaneltyMasterID", PaneltyMasterID);
            DataTable dt = dbContext.spGetDatatable("udp_PaneltyMaster_sel", HT);
            return dt;
        }
        public int AddPaneltyMaster(PaneltyMasterModel model)
        {
            Hashtable HT = new Hashtable();
            HT.Add("PaneltyMasterID", 0);
            HT.Add("PaneltyType", model.PaneltyType);
            HT.Add("Amount", model.Amount);
            HT.Add("Description", model.Description);
            int i = dbContext.ExecuteSP("udp_PaneltyMaster_ups", HT);
            return i;
        }
        public int EditPaneltyMaster(PaneltyMasterModel model)
        {
            Hashtable HT = new Hashtable();
            HT.Add("PaneltyMasterID", model.PaneltyMasterID);
            HT.Add("PaneltyType", model.PaneltyType);
            HT.Add("Amount", model.Amount);
            HT.Add("Description", model.Description);
            int i = dbContext.ExecuteSP("udp_PaneltyMaster_ups", HT);
            return i;
        }
        public int DeletePaneltyMaster(int PaneltyMasterID)
        {
            Hashtable HT = new Hashtable();
            HT.Add("PaneltyMasterID", PaneltyMasterID);
            int i = dbContext.ExecuteSP("udp_PaneltyMaster_del", HT);
            return i;
        }
        public void Dispose()
        {

        }
    }
}

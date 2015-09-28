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
    public interface IDriverService
    {
        DataTable GetDriverSearch(DriverSearchModel model);
        int AddDriver(DriverModel model);
        int EditDriver(DriverModel model);
        int DeleteDriver(int DriverID);
        bool ChangeStatus(string UserId, string Status);
        Task<int> UpdateLatLong(string UserId, string UserType, string latitude, string longitude);
    }
    public class DriverService : IDriverService
    {
        private DataFunctions dbContext;
        public DriverService()
        {
            dbContext = new DataFunctions();
        }
        public DataTable GetDriverSearch(DriverSearchModel model)
        {
            Hashtable HT = new Hashtable();
            HT.Add("DriverID", model.DriverID);
            HT.Add("SType", model.SType); // All, UserName, DriverID,Status, UserId
            HT.Add("SValue", model.SValue);
            DataTable dt = dbContext.spGetDatatable("udp_Driver_sel", HT);
            return dt;
        }
        public int AddDriver(DriverModel model)
        {
            Hashtable HT = new Hashtable();
            HT.Add("DriverID", 0);
            HT.Add("UserId", model.UserId);
            HT.Add("DriverName", model.DriverName);
            HT.Add("Email", model.Email);
            HT.Add("Mobile", model.Mobile);
            HT.Add("Password", model.Password);
            HT.Add("CrDate", model.CrDate);
            HT.Add("Language", model.Language);
            HT.Add("nWayInfo", model.nWayInfo);
            HT.Add("Paypal", model.Paypal);
            HT.Add("nWayPoint", model.nWayPoint);
            HT.Add("TaxiType", model.TaxiType);
            HT.Add("Status", model.Status);
            HT.Add("Latitute", model.Latitude);
            HT.Add("Longitude", model.Longitude);
            HT.Add("UpdateTime", model.UpdateTime);
            HT.Add("DeviceType", model.DeviceType);
            HT.Add("DeviceToken", model.DeviceToken);
            HT.Add("PhotoUrl", model.PhotoUrl);
            HT.Add("CreateTimeM", model.CreateTimeM);
            HT.Add("UpdateTimeM", model.UpdateTimeM);
            HT.Add("AuthorityCardUrl", model.AuthorityCardUrl);
            HT.Add("DriverLicenceUrl", model.DriverLicenceUrl);
            HT.Add("TaxiNumber", model.TaxiNumber);
            HT.Add("VarifyTime", model.VarifyTime);
            HT.Add("PhoneVerified", model.PhoneVerified);
            HT.Add("PhoneCode", model.PhoneCode);
            int i = dbContext.ExecuteSP("udp_Driver_ups", HT);
            return i;
        }
        public int EditDriver(DriverModel model)
        {
            Hashtable HT = new Hashtable();
            HT.Add("DriverID", model.DriverID);
            HT.Add("UserId", model.UserId);
            HT.Add("DriverName", model.DriverName);
            HT.Add("Email", model.Email);
            HT.Add("Mobile", model.Mobile);
            HT.Add("Password", model.Password);
            HT.Add("CrDate", model.CrDate);
            HT.Add("Language", model.Language);
            HT.Add("nWayInfo", model.nWayInfo);
            HT.Add("Paypal", model.Paypal);
            HT.Add("nWayPoint", model.nWayPoint);
            HT.Add("TaxiType", model.TaxiType);
            HT.Add("Status", model.Status);
            HT.Add("Latitute", model.Latitude);
            HT.Add("Longitude", model.Longitude);
            HT.Add("UpdateTime", model.UpdateTime);
            HT.Add("DeviceType", model.DeviceType);
            HT.Add("DeviceToken", model.DeviceToken);
            HT.Add("PhootUrl", model.PhotoUrl);
            HT.Add("CreateTimeM", model.CreateTimeM);
            HT.Add("UpdateTimeM", model.UpdateTimeM);
            HT.Add("AuthorityCardUrl", model.AuthorityCardUrl);
            HT.Add("DriverLicenceUrl", model.DriverLicenceUrl);
            HT.Add("TaxiNumber", model.TaxiNumber);
            HT.Add("VarifyTime", model.VarifyTime);
            HT.Add("PhoneVerified", model.PhoneVerified);
            int i = dbContext.ExecuteSP("udp_Driver_ups", HT);
            return i;
        }
        public int DeleteDriver(int DriverID)
        {
            Hashtable HT = new Hashtable();
            HT.Add("DriverID", DriverID);
            int i = dbContext.ExecuteSP("udp_Driver_del", HT);
            return i;
        }
        public bool ChangeStatus(string UserId, string Status)
        {
           var bl= dbContext.ExecuteNonQuery("Update Driver set Status='" + Status + "' where UserId='" + UserId + "'");
            return bl;
        }

        public async Task<int> UpdateLatLong(string UserId, string UserType, string latitude, string longitude)
        {
            Hashtable HT = new Hashtable();
            HT.Add("UserId", UserId);
            HT.Add("UserType", UserType);
            HT.Add("Latitude", latitude);
            HT.Add("Longitude", longitude);
            int i =await dbContext.ExecuteSPAsyn("upd_UserLatiLongi_ups", HT);
            return i;
        }
        public async Task<int> UpdateLatLong2(string UserId, string UserType, string latitude, string longitude,DateTime? updateDate,string UpdateTime)
        {
            Hashtable HT = new Hashtable();
            HT.Add("UserId", UserId);
            HT.Add("UserType", UserType);
            HT.Add("Latitude", latitude);
            HT.Add("Longitude", longitude);
            HT.Add("UpdateDate", updateDate);
            HT.Add("UpdateTime", UpdateTime);
            int i = await dbContext.ExecuteSPAsyn("upd_UserLatiLongi_oly", HT);
            return i;
        }
        public async Task<int> UpdateLatLong3(string UserId, string UserType, string latitude, string longitude, DateTime? updateDate,string UpdateTime,string DeviceType,string DeviceToken)
        {
            Hashtable HT = new Hashtable();
            HT.Add("UserId", UserId);
            HT.Add("UserType", UserType);
            HT.Add("Latitude", latitude);
            HT.Add("Longitude", longitude);
            HT.Add("UpdateDate", updateDate);
            HT.Add("UpdateTime", UpdateTime);
            HT.Add("DeviceType", DeviceType);
            HT.Add("DeviceToken", DeviceToken);
            int i = await dbContext.ExecuteSPAsyn("upd_UserLatiLongi_ups", HT);
            return i;
        }
        public void Dispose()
        {

        }
    }
}
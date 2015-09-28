using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace LiraTaxi.Data.DriverMdl
{
    public class DriverModel
    {
        public int DriverID { get; set; }
        public string UserId { get; set; }
        public string DriverName { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public string Password { get; set; }
        public Nullable<System.DateTime> CrDate { get; set; }
        public string Language { get; set; }
        public string nWayInfo { get; set; }
        public string Paypal { get; set; }
        public decimal nWayPoint { get; set; }
        public string TaxiType { get; set; }
        public string Status { get; set; }
        public string DeviceToken { get; set; }
        public string DeviceType { get; set; }
        public Nullable<DateTime> UpdateTime { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string PhotoUrl { get; set; }
        public string CreateTimeM { get; set; }
        public string UpdateTimeM { get; set; }
        public string AuthorityCardUrl { get; set; }
        public string DriverLicenceUrl { get; set; }
        public string TaxiNumber { get; set; }
        public string AuthorityCardNo { get; set; }
        public string VarifyTime { get; set; }
        public Nullable<int> PhoneVerified { get; set; }
        public int PhoneCode { get; set; }
        public HttpPostedFileBase file { get; set; }
        public HttpPostedFileBase fileCard { get; set; }
        public HttpPostedFileBase fileLicence { get; set; }
    }
}
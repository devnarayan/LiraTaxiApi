using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiraTaxi.Data.PassengerMdl
{
  public  class PassengerModel
    {
        public int PassengerID { get; set; }
        public string UserId { get; set; }
        public string FirstName { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public string Password { get; set; }
        public Nullable<System.DateTime> CrDate { get; set; }
        public string Language { get; set; }
        public string nWayInfo { get; set; }
        public string Paypal { get; set; }
        public Nullable<decimal> nWayPoint { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public Nullable<DateTime> UpdateTime { get; set; }
        public string DeviceToken { get; set; }
        public string DeviceType { get; set; }
        public string PhotoUrl { get; set; }
        public string CreateTimeM { get; set; }
        public string UpdateTimeM { get; set; }
        public string VarifyTime { get; set; }
        public Nullable<int> PhoneVerified { get; set; }
        public int PhoneCode { get; set; }
    }
}
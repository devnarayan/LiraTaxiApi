using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiraTaxi.Data.Access.Auth
{
    public interface IUserService
    {
        string GetUserID(string UserName);
        string GetUserNameViaEmail(string Email);
        string GetUserType(string UserId);
        bool DeleteUser(string UserId);
        string GetSecurityStamp(string UserId);
        bool EmailConfirmed(string UserId);
        bool IsPhoneConfirmed(string UserName);
        int PhoneConfirmed(string UserId, string VarifyTime);
    }
   public class UserService:IUserService
    {
        private DataFunctions dbContext;
        public UserService()
        {
            dbContext = new DataFunctions();
        }
        public string GetUserID(string UserName)
        {
          DataTable dt=  dbContext.GetDataTable("select Id from AspNetUsers where UserName='" + UserName + "'");
            if (dt.Rows.Count > 0)
            {
                return dt.Rows[0]["Id"].ToString();
            }
            else return "";
        }
        public string GetUserNameViaEmail(string Email)
        {
            DataTable dt = dbContext.GetDataTable("select UserName from AspNetUsers where Email='" + Email + "'");
            if (dt.Rows.Count > 0)
            {
                return dt.Rows[0]["UserName"].ToString();
            }
            else return "";
        }
        public string GetUserType(string UserId)
        {
            DataTable dt = dbContext.GetDataTable("select DriverID from Driver where UserId='" + UserId + "'");
            if (dt.Rows.Count > 0)
            {
                return "Driver";
            }
            else return "Passenger";
        }
        public bool DeleteUser(string UserId)
        {
            bool i = dbContext.ExecuteNonQuery("delete Aspnetusers where id='" + UserId + "'");
            return i;
        }
        public string GetSecurityStamp(string UserId)
        {
            DataTable dt = dbContext.GetDataTable("select SecurityStamp from AspNetUsers where Id='" + UserId + "'");
            if (dt.Rows.Count > 0)
            {
                return dt.Rows[0]["SecurityStamp"].ToString();
            }
            else return "";
        }
        public bool EmailConfirmed(string UserId)
        {
            bool i = dbContext.ExecuteNonQuery("update AspNetUsers set EmailConfirmed=1 where Id='" + UserId + "'");
            return i;
        }
        public bool IsPhoneConfirmed(string UserName)
        {
            DataTable dt = dbContext.GetDataTable("select PhoneNumber,PhoneNumberConfirmed from AspNetUsers where UserName='" + UserName + "' and PhoneNumberConfirmed=1");
            if (dt.Rows.Count > 0)
            {
                return true;
            }
            else return false;
        }
        public int PhoneConfirmed(string UserId,string VarifyTime)
        {
            Hashtable HT = new Hashtable();
            HT.Add("UserId", UserId);
            HT.Add("VarifyTime", VarifyTime);
            int i = dbContext.ExecuteSP("udp_UserPhoneConfirm_ups",HT);
            return i;
        }

    }
}

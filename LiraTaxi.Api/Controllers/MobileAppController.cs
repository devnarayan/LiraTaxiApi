using LiraTaxi.Data.Access.App;
using LiraTaxi.Data.Access.Driver;
using LiraTaxi.Data.DriverMdl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace LiraTaxi.Api.Controllers
{
    public class MobileAppController : ApiController
    {
        [HttpPost]
        public async Task<object> UpdateLatLong(UpdateLatLongModel model)
        {
            DriverService dservice = new DriverService();
            int i=await dservice.UpdateLatLong2(model.UserId, model.UserType, model.Latitude, model.Longitude,model.UpdateDate,model.UpdateTime);
            Dictionary<string, object> dic = new Dictionary<string, object>();
            if (i != 0)
            {
                dic.Add("Code", "200");
                dic.Add("Message", "Global Cords Updated Successfully.");
            }
            else
            {
                dic.Add("Code", "201");
                dic.Add("Message", "Global Cords Not Updated.");
            }
            return dic;
        }
       
        [HttpGet]
        public HttpResponseMessage SendNotification(string deviceId, string message)
        {
            AndroidGCMPushNotification apnGCM = new AndroidGCMPushNotification();
            string strResponse = apnGCM.SendNotification(deviceId, message);
            return Request.CreateResponse(HttpStatusCode.OK, strResponse);
        }
        [HttpGet]
        public string Android(string RegistrationID, string SenderID, string Password, string Message)
        {
            string Status = "";
            //--Validating the required parameter--//
            //-- Check Authentication --//
            Android objAndroid = new Android();
            string AuthString = objAndroid.CheckAuthentication(SenderID, Password);

            if (AuthString == "Fail")
            {
                Status = "Authentication Fail";
            }
            else
            {
                Status = objAndroid.SendMessage(RegistrationID, Message, AuthString);
            }


            //-- Return the Status of Push Notification --//
            return Status;
        }
    }
}

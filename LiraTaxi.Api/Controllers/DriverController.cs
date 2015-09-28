using LiraTaxi.Data.Access.App;
using LiraTaxi.Data.Access.Driver;
using LiraTaxi.Data.DriverMdl;
using log4net;
using log4net.Config;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity.Infrastructure;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace LiraTaxi.Api.Controllers
{
    public class DriverController : ApiController
    {

        DriverService db = new DriverService();
        //get all customer
        ILog logger = log4net.LogManager.GetLogger(typeof(DriverController));
        public object Get()
        {
            DriverSearchModel md = new DriverSearchModel();
            md.DriverID = 0;
            md.SType = "All";
            md.SValue = "All";
            DataTable dirver = db.GetDriverSearch(md);
            if (dirver == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }
            if (dirver.Rows.Count > 0)
            {
                Dictionary<string, object> dic = new Dictionary<string, object>();
                dic.Add("Code", "200");
                dic.Add("Message", "Driver get successfully.");
                dic.Add("data", dirver);
                return dic;
            }
            else
            {
                Dictionary<string, object> dic = new Dictionary<string, object>();
                dic.Add("Code", "201");
                dic.Add("Message", "Driver not found!");
                dic.Add("data", dirver);
                return dic;
            }

        }

        //get customer by id
        public object Get(int id)
        {
            DriverSearchModel md = new DriverSearchModel();
            md.DriverID = id;
            md.SType = "DriverID";
            md.SValue = "All";
            DataTable dirver = db.GetDriverSearch(md);
            if (dirver == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }
            if (dirver.Rows.Count > 0)
            {
                Dictionary<string, object> dic = new Dictionary<string, object>();
                dic.Add("Code", "200");
                dic.Add("Message", "Driver get successfully.");
                dic.Add("data", dirver);
                return dic;
            }
            else
            {
                Dictionary<string, object> dic = new Dictionary<string, object>();
                dic.Add("Code", "201");
                dic.Add("Message", "Driver not found!");
                dic.Add("data", dirver);
                return dic;
            }

        }
        public object GetByUserName(string UserName)
        {
            DriverSearchModel md = new DriverSearchModel();
            md.DriverID = 0;
            md.SType = "UserName";
            md.SValue = UserName;
            DataTable dirver = db.GetDriverSearch(md);
            if (dirver == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }
            if (dirver.Rows.Count > 0)
            {
                Dictionary<string, object> dic = new Dictionary<string, object>();
                dic.Add("Code", "200");
                dic.Add("Message", "Driver get successfully.");
                dic.Add("data", dirver);
                return dic;
            }
            else
            {
                Dictionary<string, object> dic = new Dictionary<string, object>();
                dic.Add("Code", "201");
                dic.Add("Message", "Driver not found!");
                dic.Add("data", dirver);
                return dic;
            }

        }
        public object GetByUserId(string UserId)
        {
            DriverSearchModel md = new DriverSearchModel();
            md.DriverID = 0;
            md.SType = "UserId";
            md.SValue = UserId;
            DataTable dirver = db.GetDriverSearch(md);
            if (dirver == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }
            if (dirver.Rows.Count > 0)
            {
                Dictionary<string, object> dic = new Dictionary<string, object>();
                dic.Add("Code", "200");
                dic.Add("Message", "Driver get successfully.");
                dic.Add("data", dirver);
                return dic;
            }
            else
            {
                Dictionary<string, object> dic = new Dictionary<string, object>();
                dic.Add("Code", "201");
                dic.Add("Message", "Driver not found!");
                dic.Add("data", dirver);
                return dic;
            }

        }
        [HttpPost]
        public object GetDriverInRange(DriverSearchRangeModel model)
        {
            DriverSearchModel md = new DriverSearchModel();
            md.DriverID = 0;
            md.SType = "Status";
            md.SValue = model.Status; // OnDuty,OffDuty,Inactive
            DataTable dirver = db.GetDriverSearch(md);
            if (dirver == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }
            if (dirver.Rows.Count > 0)
            {
                List<DriverModel> dmdl = new List<DriverModel>();
                AppService appService = new AppService();
                foreach(DataRow row in dirver.Rows)
                {
                    var dist =  appService.Distance(model.Latitude, model.Longitude,Convert.ToDouble(row["Latitute"]),Convert.ToDouble(row["Longitude"]), model.Unit);
                    if (dist <= model.Distance)
                    {
                        DriverModel dm = new DriverModel();
                       // dm.CrDate = Convert.ToDateTime(row["CrDate"]);
                        dm.DeviceToken = row["DeviceToken"].ToString();
                        dm.DeviceType = row["DeviceType"].ToString();
                        dm.DriverID = Convert.ToInt32(row["DriverID"]);
                        dm.DriverName = row["DriverName"].ToString();
                        dm.Email = row["Email"].ToString();
                        dm.Language = row["Language"].ToString();
                        dm.Latitude = row["Latitute"].ToString();
                        dm.Longitude = row["Longitude"].ToString();
                        dm.Mobile = row["Mobile"].ToString();
                        dm.nWayInfo = row["nWayInfo"].ToString();
                        dm.nWayPoint =Convert.ToDecimal(row["nWayPoint"].ToString());
                        dm.Password = row["Password"].ToString();
                        dm.Paypal = row["PayPal"].ToString();
                        dm.PhotoUrl = row["PhotoUrl"].ToString();
                        dm.Status = row["Status"].ToString();
                        dm.TaxiType = row["TaxiType"].ToString();
                        //dm.UpdateTime = Convert.ToDateTime(row["UpdateTime"]);
                        dm.UpdateTimeM = row["UpdateTimeM"].ToString();
                        dm.CreateTimeM = row["CreateTimeM"].ToString();
                        dm.UserId = row["UserId"].ToString();
                        dmdl.Add(dm);
                    }
                }
                Dictionary<string, object> dic = new Dictionary<string, object>();
                dic.Add("Code", "200");
                dic.Add("Message", "Driver get successfully.");
                dic.Add("data", dmdl);
                return dic;
            }
            else
            {
                Dictionary<string, object> dic = new Dictionary<string, object>();
                dic.Add("Code", "201");
                dic.Add("Message", "Driver not found!");
                dic.Add("data", dirver);
                return dic;
            }

        }
        
        [HttpGet]
        public object ChangeDutyStatus(string UserId,string Status)
        {
            var bl= db.ChangeStatus(UserId, Status);
            Dictionary<string, string> dic = new Dictionary<string, string>();
            if (bl)
            {
                dic.Add("Code", "200");
                dic.Add("Message", "Status Updated Successfully.");
            }
            else
            {
                dic.Add("Code", "201");
                dic.Add("Message", "Status Not Updated.");
            }
            return dic;
        }
        //insert customer
        public async Task<HttpResponseMessage> Post(DriverModel driver)
        {
           
            if (!Request.Content.IsMimeMultipartContent())
            {
                this.Request.CreateResponse(HttpStatusCode.UnsupportedMediaType);
            }

            var provider = GetMultipartProvider();
            var result = await Request.Content.ReadAsMultipartAsync(provider);

            // On upload, files are given a generic name like "BodyPart_26d6abe1-3ae1-416a-9429-b35f15e6e5d5"
            // so this is how you can get the original file name
            var originalFileName = GetDeserializedFileName(result.FileData.First());

            // uploadedFileInfo object will give you some additional stuff like file length,
            // creation time, directory name, a few filesystem methods etc..
            var uploadedFileInfo = new FileInfo(result.FileData.First().LocalFileName);

            // Remove this line as well as GetFormData method if you're not
            // sending any form data with your upload request
            var fileUploadObj = GetFormData<DriverModel>(result);

            // Through the request response you can return an object to the Angular controller
            // You will be able to access this in the .success callback through its data attribute
            // If you want to send something to the .error callback, use the HttpStatusCode.BadRequest instead
            var returnData = "ReturnTest";
            if (ModelState.IsValid)
            {
                db.AddDriver(driver);
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, driver);
                response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = driver.DriverID }));
                return response;
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }

        [HttpPost]
        public async Task<HttpResponseMessage> Register()
        {

            if (!Request.Content.IsMimeMultipartContent())
            {
                this.Request.CreateResponse(HttpStatusCode.UnsupportedMediaType);
            }

            var provider = GetMultipartProvider();
            var result = await Request.Content.ReadAsMultipartAsync(provider);

            // On upload, files are given a generic name like "BodyPart_26d6abe1-3ae1-416a-9429-b35f15e6e5d5"
            // so this is how you can get the original file name
            var originalFileName = GetDeserializedFileName(result.FileData.First());

            // uploadedFileInfo object will give you some additional stuff like file length,
            // creation time, directory name, a few filesystem methods etc..
            var uploadedFileInfo = new FileInfo(result.FileData.First().LocalFileName);

            // Remove this line as well as GetFormData method if you're not
            // sending any form data with your upload request
            

            // Through the request response you can return an object to the Angular controller
            // You will be able to access this in the .success callback through its data attribute
            // If you want to send something to the .error callback, use the HttpStatusCode.BadRequest instead
            if (ModelState.IsValid)
            {
                DriverModel fileUploadObj =(DriverModel)GetFormData<DriverModel>(result);
                fileUploadObj.PhotoUrl = ConfigurationManager.AppSettings["PhotoDir"] + "" + uploadedFileInfo.Name;
                fileUploadObj.CrDate = DateTime.Now;
                fileUploadObj.Status = "OnDuty";
                fileUploadObj.UpdateTime = DateTime.Now;
                db.AddDriver(fileUploadObj);
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created,fileUploadObj);
                response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = fileUploadObj.DriverID }));
                return response;
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }

        // You could extract these two private methods to a separate utility class since
        // they do not really belong to a controller class but that is up to you
        private MultipartFormDataStreamProvider GetMultipartProvider()
        {
            // IMPORTANT: replace "(tilde)" with the real tilde character
            // (our editor doesn't allow it, so I just wrote "(tilde)" instead)
            var uploadFolder = ConfigurationManager.AppSettings["PhotoDir"]; // you could put this to web.config
            var root = HttpContext.Current.Server.MapPath(uploadFolder);
            Directory.CreateDirectory(root);
            return new MultipartFormDataStreamProvider(root);
        }

        // Extracts Request FormatData as a strongly typed model
        private object GetFormData<T>(MultipartFormDataStreamProvider result)
        {
            if (result.FormData.HasKeys())
            {
                DriverModel mdl = new DriverModel();
                for (int i = 0; i < result.FormData.AllKeys.Length; i++)
                {
                    if (result.FormData.GetKey(i) == "DriverID")
                    {
                        mdl.DriverID =Convert.ToInt32(result.FormData.GetValues(i)[0]);
                    }
                    if (result.FormData.GetKey(i) == "UserId")
                    {
                        mdl.UserId = result.FormData.GetValues(i)[0];
                    }
                    if (result.FormData.GetKey(i) == "DriverName")
                    {
                        mdl.DriverName = result.FormData.GetValues(i)[0];
                    }
                    if (result.FormData.GetKey(i) == "Email")
                    {
                        mdl.Email = result.FormData.GetValues(i)[0];
                    }
                    if (result.FormData.GetKey(i) == "Mobile")
                    {
                        mdl.Mobile = result.FormData.GetValues(i)[0];
                    }
                    if (result.FormData.GetKey(i) == "Password")
                    {
                        mdl.Password = result.FormData.GetValues(i)[0];
                    }
                    if (result.FormData.GetKey(i) == "Language")
                    {
                        mdl.Language = result.FormData.GetValues(i)[0];
                    }
                    if (result.FormData.GetKey(i) == "TaxiType")
                    {
                        mdl.TaxiType = result.FormData.GetValues(i)[0];
                    }
                }
                return mdl;
                // Code for angularjs
                //var unescapedFormData = Uri.UnescapeDataString(result.FormData
                //    .GetValues(0).FirstOrDefault() ?? String.Empty);
                //if (!String.IsNullOrEmpty(unescapedFormData))
                //    return JsonConvert.DeserializeObject<T>(unescapedFormData);
            }

            return null;
        }

        private string GetDeserializedFileName(MultipartFileData fileData)
        {
            var fileName = GetFileName(fileData);
            return JsonConvert.DeserializeObject(fileName).ToString();
        }

        public string GetFileName(MultipartFileData fileData)
        {
            return fileData.Headers.ContentDisposition.FileName;
        }

        //update customer
        public HttpResponseMessage Put(int id, DriverModel driver)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
            if (id != driver.DriverID)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
            try
            {
                db.EditDriver(driver);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
            }
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        //delete customer by id
        public HttpResponseMessage Delete(int DriverID)
        {
            if (DriverID == 0)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }
            try
            {
                db.DeleteDriver(DriverID);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
            }
            return Request.CreateResponse(HttpStatusCode.OK, DriverID);
        }

        //prevent memory leak
        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}

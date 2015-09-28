using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.ModelBinding;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OAuth;
using LiraTaxi.Api.Models;
using LiraTaxi.Api.Providers;
using LiraTaxi.Api.Results;
using LiraTaxi.Data.Access.Driver;
using LiraTaxi.Data.DriverMdl;
using LiraTaxi.Data.Access.Passenger;
using LiraTaxi.Data.PassengerMdl;
using System.Net;
using System.IO;
using System.Linq;
using System.Configuration;
using Newtonsoft.Json;
using LiraTaxi.Data.Access.Auth;
using LiraTaxi.Api.Code;
using System.Data;
using LiraTaxi.Data.Access.App;

namespace LiraTaxi.Api.Controllers
{
    [Authorize]
    [RoutePrefix("api/Account")]
    public class AccountController : ApiController
    {
        private const string LocalLoginProvider = "Local";
        private ApplicationUserManager _userManager;
        private SendMail sendMail;
        private UserService uservice;

        public AccountController()
        {
            sendMail = new SendMail();
            uservice = new UserService();
        }

        public AccountController(ApplicationUserManager userManager,
            ISecureDataFormat<AuthenticationTicket> accessTokenFormat)
        {
            UserManager = userManager;
            AccessTokenFormat = accessTokenFormat;
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? Request.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        public ISecureDataFormat<AuthenticationTicket> AccessTokenFormat { get; private set; }

        // GET api/Account/UserInfo
        [HostAuthentication(DefaultAuthenticationTypes.ExternalBearer)]
        [Route("UserInfo")]
        public UserInfoViewModel GetUserInfo()
        {
            ExternalLoginData externalLogin = ExternalLoginData.FromIdentity(User.Identity as ClaimsIdentity);

            return new UserInfoViewModel
            {
                Email = User.Identity.GetUserName(),
                HasRegistered = externalLogin == null,
                LoginProvider = externalLogin != null ? externalLogin.LoginProvider : null
            };
        }

        // POST api/Account/Logout
        [Route("Logout")]
        public IHttpActionResult Logout()
        {
            Authentication.SignOut(CookieAuthenticationDefaults.AuthenticationType);
            return Ok();
        }

        [AllowAnonymous]
        public async Task<object> GetUserID(string UserName)
        {
            UserService uservice = new UserService();
            string st =uservice.GetUserID(UserName);
            if (st != "")
            {
                Dictionary<string, object> dic = new Dictionary<string, object>();
                dic.Add("Code", "200");
                dic.Add("Message", "UserId Get successfully.");
                dic.Add("UserId", st);
                return dic;
            }
            else
            {
                Dictionary<string, object> dic = new Dictionary<string, object>();
                dic.Add("Code", "201");
                dic.Add("Message", "UserId not found.");
                dic.Add("UserId", "");
                return dic;
            }
        }
        // GET api/Account/ManageInfo?returnUrl=%2F&generateState=true
        [Route("ManageInfo")]
        public async Task<ManageInfoViewModel> GetManageInfo(string returnUrl, bool generateState = false)
        {
            IdentityUser user = await UserManager.FindByIdAsync(User.Identity.GetUserId());

            if (user == null)
            {
                return null;
            }

            List<UserLoginInfoViewModel> logins = new List<UserLoginInfoViewModel>();

            foreach (IdentityUserLogin linkedAccount in user.Logins)
            {
                logins.Add(new UserLoginInfoViewModel
                {
                    LoginProvider = linkedAccount.LoginProvider,
                    ProviderKey = linkedAccount.ProviderKey
                });
            }

            if (user.PasswordHash != null)
            {
                logins.Add(new UserLoginInfoViewModel
                {
                    LoginProvider = LocalLoginProvider,
                    ProviderKey = user.UserName,
                });
            }

            return new ManageInfoViewModel
            {
                LocalLoginProvider = LocalLoginProvider,
                Email = user.UserName,
                Logins = logins,
                ExternalLoginProviders = GetExternalLogins(returnUrl, generateState)
            };
        }

        // POST api/Account/ChangePassword
        [Route("ChangePassword")]
        public async Task<IHttpActionResult> ChangePassword(ChangePasswordBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            IdentityResult result = await UserManager.ChangePasswordAsync(User.Identity.GetUserId(), model.OldPassword,
                model.NewPassword);
            
            if (!result.Succeeded)
            {
                return GetErrorResult(result);
            }

            return Ok();
        }

        // POST api/Account/SetPassword
        [Route("SetPassword")]
        public async Task<IHttpActionResult> SetPassword(SetPasswordBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            IdentityResult result = await UserManager.AddPasswordAsync(User.Identity.GetUserId(), model.NewPassword);

            if (!result.Succeeded)
            {
                return GetErrorResult(result);
            }

            return Ok();
        }

        // POST api/Account/AddExternalLogin
        [Route("AddExternalLogin")]
        public async Task<IHttpActionResult> AddExternalLogin(AddExternalLoginBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Authentication.SignOut(DefaultAuthenticationTypes.ExternalCookie);

            AuthenticationTicket ticket = AccessTokenFormat.Unprotect(model.ExternalAccessToken);

            if (ticket == null || ticket.Identity == null || (ticket.Properties != null
                && ticket.Properties.ExpiresUtc.HasValue
                && ticket.Properties.ExpiresUtc.Value < DateTimeOffset.UtcNow))
            {
                return BadRequest("External login failure.");
            }

            ExternalLoginData externalData = ExternalLoginData.FromIdentity(ticket.Identity);

            if (externalData == null)
            {
                return BadRequest("The external login is already associated with an account.");
            }

            IdentityResult result = await UserManager.AddLoginAsync(User.Identity.GetUserId(),
                new UserLoginInfo(externalData.LoginProvider, externalData.ProviderKey));

            if (!result.Succeeded)
            {
                return GetErrorResult(result);
            }

            return Ok();
        }

        // POST api/Account/RemoveLogin
        [Route("RemoveLogin")]
        public async Task<IHttpActionResult> RemoveLogin(RemoveLoginBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            IdentityResult result;

            if (model.LoginProvider == LocalLoginProvider)
            {
                result = await UserManager.RemovePasswordAsync(User.Identity.GetUserId());
            }
            else
            {
                result = await UserManager.RemoveLoginAsync(User.Identity.GetUserId(),
                    new UserLoginInfo(model.LoginProvider, model.ProviderKey));
            }

            if (!result.Succeeded)
            {
                return GetErrorResult(result);
            }

            return Ok();
        }

        // GET api/Account/ExternalLogin
        [OverrideAuthentication]
        [HostAuthentication(DefaultAuthenticationTypes.ExternalCookie)]
        [AllowAnonymous]
        [Route("ExternalLogin", Name = "ExternalLogin")]
        public async Task<IHttpActionResult> GetExternalLogin(string provider, string error = null)
        {
            if (error != null)
            {
                return Redirect(Url.Content("~/") + "#error=" + Uri.EscapeDataString(error));
            }

            if (!User.Identity.IsAuthenticated)
            {
                return new ChallengeResult(provider, this);
            }

            ExternalLoginData externalLogin = ExternalLoginData.FromIdentity(User.Identity as ClaimsIdentity);

            if (externalLogin == null)
            {
                return InternalServerError();
            }

            if (externalLogin.LoginProvider != provider)
            {
                Authentication.SignOut(DefaultAuthenticationTypes.ExternalCookie);
                return new ChallengeResult(provider, this);
            }

            ApplicationUser user = await UserManager.FindAsync(new UserLoginInfo(externalLogin.LoginProvider,
                externalLogin.ProviderKey));

            bool hasRegistered = user != null;

            if (hasRegistered)
            {
                Authentication.SignOut(DefaultAuthenticationTypes.ExternalCookie);
                
                 ClaimsIdentity oAuthIdentity = await user.GenerateUserIdentityAsync(UserManager,
                    OAuthDefaults.AuthenticationType);
                ClaimsIdentity cookieIdentity = await user.GenerateUserIdentityAsync(UserManager,
                    CookieAuthenticationDefaults.AuthenticationType);

                AuthenticationProperties properties = ApplicationOAuthProvider.CreateProperties(user.UserName);
                Authentication.SignIn(properties, oAuthIdentity, cookieIdentity);
            }
            else
            {
                IEnumerable<Claim> claims = externalLogin.GetClaims();
                ClaimsIdentity identity = new ClaimsIdentity(claims, OAuthDefaults.AuthenticationType);
                Authentication.SignIn(identity);
            }

            return Ok();
        }

        // GET api/Account/ExternalLogins?returnUrl=%2F&generateState=true
        [AllowAnonymous]
        [Route("ExternalLogins")]
        public IEnumerable<ExternalLoginViewModel> GetExternalLogins(string returnUrl, bool generateState = false)
        {
            IEnumerable<AuthenticationDescription> descriptions = Authentication.GetExternalAuthenticationTypes();
            List<ExternalLoginViewModel> logins = new List<ExternalLoginViewModel>();

            string state;

            if (generateState)
            {
                const int strengthInBits = 256;
                state = RandomOAuthStateGenerator.Generate(strengthInBits);
            }
            else
            {
                state = null;
            }

            foreach (AuthenticationDescription description in descriptions)
            {
                ExternalLoginViewModel login = new ExternalLoginViewModel
                {
                    Name = description.Caption,
                    Url = Url.Route("ExternalLogin", new
                    {
                        provider = description.AuthenticationType,
                        response_type = "token",
                        client_id = Startup.PublicClientId,
                        redirect_uri = new Uri(Request.RequestUri, returnUrl).AbsoluteUri,
                        state = state
                    }),
                    State = state
                };
                logins.Add(login);
            }

            return logins;
        }

        // POST api/Account/Register
        [AllowAnonymous]
        [Route("Register")]
        public async Task<IHttpActionResult> Register(RegisterBindingModel model)
        {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var user = new ApplicationUser() { UserName = model.UserName, Email = model.Email };
                IdentityResult result = await UserManager.CreateAsync(user, model.Password);

                if (!result.Succeeded)
                {
                    return GetErrorResult(result);
                }
                else
                {
                    int i = 0;
                    if (model.UserType == "Driver")
                    {
                        DriverService db = new DriverService();
                        DriverModel md = new DriverModel();
                        md.UserId = user.Id;
                        md.DriverName = model.FullName;
                        md.Email = model.Email;
                        md.Mobile = model.Mobile;
                        md.Password = model.Password;
                        md.Longitude = model.Longitude;
                        md.Latitude = model.Latitude;
                        md.DeviceToken = model.DeviceToken;
                        md.DeviceType = model.DeviceType;
                        md.UpdateTime = DateTime.Now;
                        md.nWayPoint = 0;
                        md.nWayInfo = "N/A";
                        md.Paypal = "N/A";
                        md.Status = "OnDuty";
                        md.Language = "En";
                        md.CrDate = DateTime.Now;
                        i=  db.AddDriver(md);
                    }
                   else if (model.UserType == "Passenger")
                    {
                        PassengerService db = new PassengerService();
                        PassengerModel md = new PassengerModel();
                        md.UserId = user.Id;
                        md.FirstName = model.FullName;
                        md.Email = model.Email;
                        md.Mobile = model.Mobile;
                        md.Password = model.Password;
                        md.Longitude = model.Longitude;
                        md.Latitude = model.Latitude;
                        md.DeviceToken = model.DeviceToken;
                        md.DeviceType = model.DeviceType;
                        md.UpdateTime = DateTime.Now;
                        md.nWayPoint = 0;
                        md.nWayInfo = "N/A";
                        md.Paypal = "N/A";
                        md.Language = "En";
                        md.CrDate = DateTime.Now;
                      i=  db.AddPassenger(md);
                    }
                    if (i == 0)
                    {
                        return BadRequest("Invalid "+ model.UserType+" Profile values.");
                    }
                }
            
                return Ok(Newtonsoft.Json.JsonConvert.SerializeObject(user.Id));
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<object> RegisterV2()
        {
            string filename = ConfigurationManager.AppSettings["PhotoDir"].TrimStart('~')+"user.png";
            string AuthCard = "";
            string Licenceurl = "";
            if (!Request.Content.IsMimeMultipartContent())
            {
                this.Request.CreateResponse(HttpStatusCode.UnsupportedMediaType);
            }

            var provider = GetMultipartProvider(ConfigurationManager.AppSettings["PhotoDir"]);
            var result = await Request.Content.ReadAsMultipartAsync(provider);
            if (result.FileData.Count > 0)
            {
               for(int i = 0; i < result.FileData.Count; i++)
                {
                    MultipartFileData f = result.FileData[i];
                    string st = f.Headers.ContentDisposition.Name.Trim('"');
                    var originalFileName = GetDeserializedFileName(result.FileData[i]);
                    string ext = Path.GetExtension(originalFileName);
                    if (st == "file") { 
                    var uploadedFileInfo = new FileInfo(result.FileData[i].LocalFileName);
                    File.Move(uploadedFileInfo.FullName, uploadedFileInfo.FullName + ext);
                    filename = ConfigurationManager.AppSettings["PhotoDir"].TrimStart('~') + "" + uploadedFileInfo.Name + "" + ext;
                    }
                    else if(st== "fileLicence")
                    {
                        var uploadedFileInfo = new FileInfo(result.FileData[i].LocalFileName);
                        File.Move(uploadedFileInfo.FullName, uploadedFileInfo.FullName + ext);
                        Licenceurl = ConfigurationManager.AppSettings["PhotoDir"].TrimStart('~') + "" + uploadedFileInfo.Name + "" + ext;
                    }
                    else if (st == "fileCard")
                    {
                        var uploadedFileInfo = new FileInfo(result.FileData[i].LocalFileName);
                        File.Move(uploadedFileInfo.FullName, uploadedFileInfo.FullName + ext);
                        AuthCard = ConfigurationManager.AppSettings["PhotoDir"].TrimStart('~') + "" + uploadedFileInfo.Name + "" + ext;
                    }
                }
               
            }
            if (ModelState.IsValid)
            {
                RegisterBindingModel model = (RegisterBindingModel)GetFormData<RegisterBindingModel>(result);
              
                var user = new ApplicationUser() { UserName = model.UserName, Email = model.Email };
                IdentityResult result2 = await UserManager.CreateAsync(user, model.Password);
              // DateTime date = new DateTime(double.Parse(lg));
          
                if (!result2.Succeeded)
                {
                    Dictionary<string,string> dc = new Dictionary<string,string>();
                    dc.Add("Code", "200");
                    dc.Add("Message", result2.Errors.First());
                    return dc;
                }
                else
                {
                    // Send Email.
                    EmailModel em = new EmailModel();
                    string token= uservice.GetSecurityStamp(user.Id);
                    em.UserName = model.FullName;
                    em.Subject = "[Lira Taxi]- Your Registration Successfully.";
                    em.Summary = "Please active your account to click on below button.";
                    em.ModuleLink = ConfigurationManager.AppSettings["WebDomain"] + "Account/ConfirmEmail?UserId="+user.Id+"&code="+token;
                    em.LeadName = "Thanks for Register on Lirataxi.com";
                    em.EmailTo = ConfigurationManager.AppSettings["EstimationMail"] + ";" + model.Email; // ; saperated email ids.
                    var bl = await sendMail.ActivationMail(em);
                    int code = await sendMail.SendSMS(user.Id, model.Mobile);
                    int i = 0;
                    if (model.UserType == "Driver")
                    {
                        DriverService db = new DriverService();
                        DriverModel md = new DriverModel();
                        md.UserId = user.Id;
                        md.DriverName = model.FullName;
                        md.Email = model.Email;
                        md.Mobile = model.Mobile;
                        md.Password = model.Password;
                        md.Longitude = model.Longitude;
                        md.Latitude = model.Latitude;
                        md.DeviceToken = model.DeviceToken;
                        md.DeviceType = model.DeviceType;
                        md.UpdateTime = model.CreateDate;
                        md.nWayPoint = 0;
                        md.nWayInfo = "";
                        md.Paypal = "";
                        md.Status = "OnDuty";
                        md.Language = "En";
                        md.CrDate =model.CreateDate;
                        md.PhotoUrl = filename;
                        md.CreateTimeM = model.CreateTime;
                        md.UpdateTimeM = model.CreateTime;
                        md.AuthorityCardUrl = AuthCard;
                        md.DriverLicenceUrl = Licenceurl;
                        md.TaxiNumber = model.TaxiNumber;
                        md.AuthorityCardNo = model.AuthorityCardNo;
                        md.VarifyTime = model.VarifyTime;
                        md.PhoneVerified = 0;
                        md.PhoneCode = code;
                        md.TaxiType = model.TaxiType;
                        i = db.AddDriver(md);
                        if (i == 0)
                        {
                            uservice.DeleteUser(user.Id);
                            Dictionary<object, object> dc2 = new Dictionary<object, object>();
                            dc2.Add("Code", "201");
                            dc2.Add("Message", "Invalid Profile data.");
                            return dc2;
                        }
                        else
                        {
                            Dictionary<object, object> dc1 = new Dictionary<object, object>();
                            dc1.Add("Code", "200");
                            dc1.Add("Message", "Registration Successfully.");
                            dc1.Add("data", md);
                            return dc1;
                        }
                    }
                    else if (model.UserType == "Passenger")
                    {
                            uservice.DeleteUser(user.Id);
                            Dictionary<object, object> dc2 = new Dictionary<object, object>();
                            dc2.Add("Code", "201");
                            dc2.Add("Message", "Please Use RegisterPassenger Api for Passenger.");
                            return dc2;
                    }
                    Dictionary<object, object> dc3 = new Dictionary<object, object>();
                    dc3.Add("Code", "201");
                    dc3.Add("Message", "Provide Correct User Type");
                    dc3.Add("data", "");
                    return dc3;
                }
            }
            else
            {
                Dictionary<object, object> dc = new Dictionary<object, object>();
                dc.Add("Code", "201");
                dc.Add("Message", "Model State not valid");
                dc.Add("data", "");
                return dc;
            }
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<object> RegisterPassenger()
        {
            string filename = ConfigurationManager.AppSettings["PhotoDir"].TrimStart('~') + "user.png";
           
            if (!Request.Content.IsMimeMultipartContent())
            {
                this.Request.CreateResponse(HttpStatusCode.UnsupportedMediaType);
            }

            var provider = GetMultipartProvider(ConfigurationManager.AppSettings["PhotoDir"]);
            var result = await Request.Content.ReadAsMultipartAsync(provider);
            if (result.FileData.Count > 0)
            {
                MultipartFileData f = result.FileData.First();

                var originalFileName = GetDeserializedFileName(result.FileData.First());
                string ext = Path.GetExtension(originalFileName);
                var uploadedFileInfo = new FileInfo(result.FileData.First().LocalFileName);
                File.Move(uploadedFileInfo.FullName, uploadedFileInfo.FullName + ext);
                filename = ConfigurationManager.AppSettings["PhotoDir"].TrimStart('~') + "" + uploadedFileInfo.Name + "" + ext;
            }
            if (ModelState.IsValid)
            {
                RegisterBindingModel model = (RegisterBindingModel)GetFormData<RegisterBindingModel>(result);

                var user = new ApplicationUser() { UserName = model.UserName, Email = model.Email };
                IdentityResult result2 = await UserManager.CreateAsync(user, model.Password);
                // DateTime date = new DateTime(double.Parse(lg));

                if (!result2.Succeeded)
                {
                    Dictionary<string, string> dc = new Dictionary<string, string>();
                    dc.Add("Code", "200");
                    dc.Add("Message", result2.Errors.First());
                    return dc;
                }
                else
                {
                    // Send Email.
                    EmailModel em = new EmailModel();
                    string token = uservice.GetSecurityStamp(user.Id);
                    em.UserName = model.FullName;
                    em.Subject = "[Lira Taxi]- Your Registration Successfully.";
                    em.Summary = "Please active your account to click on below button.";
                    em.ModuleLink = ConfigurationManager.AppSettings["WebDomain"] + "Account/ConfirmEmail?UserId=" + user.Id + "&code=" + token;
                    em.LeadName = "Thanks for Register on Lirataxi.com";
                    em.EmailTo = ConfigurationManager.AppSettings["EstimationMail"] + ";" + model.Email; // ; saperated email ids.
                    var bl = await sendMail.ActivationMail(em);
                    int code= await sendMail.SendSMS(user.Id, model.Mobile);
                    int i = 0;
                 
                        PassengerService db = new PassengerService();
                        PassengerModel md = new PassengerModel();
                        md.UserId = user.Id;
                        md.FirstName = model.FullName;
                        md.Email = model.Email;
                        md.Mobile = model.Mobile;
                        md.Password = model.Password;
                        md.Longitude = model.Longitude;
                        md.Latitude = model.Latitude;
                        md.DeviceToken = model.DeviceToken;
                        md.DeviceType = model.DeviceType;
                        md.UpdateTime = model.CreateDate;
                        md.nWayPoint = 0;
                        md.nWayInfo = "";
                        md.Paypal = "";
                        md.Language = "En";
                        md.CrDate = model.CreateDate;
                        md.PhotoUrl = filename;
                        md.CreateTimeM = model.CreateTime;
                        md.UpdateTimeM = model.CreateTime;
                        md.PhoneVerified = 0;
                        md.VarifyTime = model.VarifyTime;
                        md.PhoneCode = code;
                        i = db.AddPassenger(md);
                        if (i == 0)
                        {
                            uservice.DeleteUser(user.Id);
                            Dictionary<object, object> dc2 = new Dictionary<object, object>();
                            dc2.Add("Code", "201");
                            dc2.Add("Message", "Invalid Profile data.");
                            return dc2;
                        }
                        else
                        {
                            Dictionary<object, object> dc2 = new Dictionary<object, object>();
                            dc2.Add("Code", "200");
                            dc2.Add("Message", "Registration Successfully.");
                            dc2.Add("data", md);
                            return dc2;
                        }
                    Dictionary<object, object> dc3 = new Dictionary<object, object>();
                    dc3.Add("Code", "201");
                    dc3.Add("Message", "Provide Correct User Type");
                    dc3.Add("data", "");
                    return dc3;
                }
            }
            else
            {
                Dictionary<object, object> dc = new Dictionary<object, object>();
                dc.Add("Code", "201");
                dc.Add("Message", "Model State not valid");
                dc.Add("data", "");
                return dc;
            }
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<object> Login(LoginModel model)
        {
            Dictionary<string, object> dic = new Dictionary<string, object>();
            var bl = uservice.IsPhoneConfirmed(model.UserName);
            if (!bl)
            {
                dic.Add("Code", "201");
                dic.Add("Message", "Phone Number Not Varified.");
                return dic;
            }
            if(model.UserName==null || model.UserName == "")
            {
                string username= uservice.GetUserNameViaEmail(model.Email);
                model.UserName = username;
            }
            AccessToken tkn=  AuthToken.GetLoingInfo(model);
            if (tkn != null)
            {
                // Update Lat and Long.
                DriverService dservice = new DriverService();
                if (model.UserType == "Driver")
                {
                    DriverSearchModel md = new DriverSearchModel();
                    md.DriverID = 0;
                    md.SType = "UserName";
                    md.SValue = model.UserName;
                    DataTable dirver = dservice.GetDriverSearch(md);
                    if (dirver.Rows.Count > 0)
                    {
                        // update Lat and long.
                        int i = await dservice.UpdateLatLong3(dirver.Rows[0]["UserId"].ToString(), model.UserType, model.Latitude, model.Longitude,model.UpdateDate,model.UpdateTime,model.DeviceType,model.DeviceToken);
                        dirver.Rows[0]["DeviceType"] = model.DeviceType;
                        dirver.Rows[0]["DeviceToken"] = model.DeviceToken;
                        dirver.Rows[0]["UpdateTimeM"] = model.UpdateTime;
                        // dirver.Rows[0]["UpdateDate"] = model.UpdateDate;
                        dirver.Rows[0]["Latitute"] = model.Latitude;
                        dirver.Rows[0]["Longitude"] = model.Longitude;
                    }
                    dic.Add("Code", "200");
                    dic.Add("Message", model.UserType + " loggedin successfully");
                    dic.Add("AuthToken", tkn.access_token);
                    dic.Add("UserName", model.UserName);
                    dic.Add("data", dirver);
                }
                else if(model.UserType=="Passenger")
                {
                    PassengerService db = new PassengerService();
                    DataTable dirver = db.GetPassengerSearch(0, "UserName", model.UserName);
                    if (dirver.Rows.Count > 0)
                    {
                        // update Lat and long.
                        int i = await dservice.UpdateLatLong3(dirver.Rows[0]["UserId"].ToString(), model.UserType, model.Latitude, model.Longitude,model.UpdateDate, model.UpdateTime, model.DeviceType, model.DeviceToken);
                        dirver.Rows[0]["DeviceType"] = model.DeviceType;
                        dirver.Rows[0]["DeviceToken"] = model.DeviceToken;
                        dirver.Rows[0]["UpdateTimeM"] = model.UpdateTime;
                        // dirver.Rows[0]["UpdateDate"] = model.UpdateDate;
                        dirver.Rows[0]["Latitute"] = model.Latitude;
                        dirver.Rows[0]["Longitude"] = model.Longitude;
                    }
                    dic.Add("Code", "200");
                    dic.Add("AuthToken", tkn.access_token);
                    dic.Add("UserName", model.UserName);
                    dic.Add("Message", model.UserType + " loggedin successfully");
                    dic.Add("data", dirver);
                }
                else
                {
                    dic.Add("Code", "201");
                    dic.Add("Message", model.UserType + " Info not found.");
                }
            }
            else
            {
                dic.Add("Code", "201");
                dic.Add("Message", model.UserType + " Invalid login Credentials.");
            }
            return dic;
        }
        [HttpGet]
        [AllowAnonymous]
        public async Task<object> VarifyPhoneNo(string Code,string UserId,string VarifyTime)
        {
            Dictionary<string, object> dic = new Dictionary<string, object>();
           int i= uservice.PhoneConfirmed(UserId, VarifyTime);
            if (i==0)
            {
                dic.Add("Code", "201");
                dic.Add("Message", "Phone Number Not Varified.");
                return dic;
            }
            else
            {
                dic.Add("Code", "200");
                dic.Add("Message", "Phone Number Varified Successfylly.");
                return dic;
            }
           
        }
        // You could extract these two private methods to a separate utility class since
        // they do not really belong to a controller class but that is up to you
        private MultipartFormDataStreamProvider GetMultipartProvider(string uploadFolder)
        {
            // IMPORTANT: replace "(tilde)" with the real tilde character
            // (our editor doesn't allow it, so I just wrote "(tilde)" instead)
           
            var root = HttpContext.Current.Server.MapPath(uploadFolder);
            Directory.CreateDirectory(root);
            return new MultipartFormDataStreamProvider(root);
        }

        // Extracts Request FormatData as a strongly typed model
        private object GetFormData<T>(MultipartFormDataStreamProvider result)
        {
            if (result.FormData.HasKeys())
            {
                RegisterBindingModel mdl = new RegisterBindingModel();
                for (int i = 0; i < result.FormData.AllKeys.Length; i++)
                {
                    if (result.FormData.GetKey(i) == "email")
                    {
                        mdl.Email = result.FormData.GetValues(i)[0];
                    }
                    if (result.FormData.GetKey(i) == "userName")
                    {
                        mdl.UserName = result.FormData.GetValues(i)[0];
                    }
                    if (result.FormData.GetKey(i) == "password")
                    {
                        mdl.Password = result.FormData.GetValues(i)[0];
                    }
                    if (result.FormData.GetKey(i) == "confirmPassword")
                    {
                        mdl.ConfirmPassword = result.FormData.GetValues(i)[0];
                    }
                    if (result.FormData.GetKey(i) == "FullName")
                    {
                        mdl.FullName = result.FormData.GetValues(i)[0];
                    }
                    if (result.FormData.GetKey(i) == "UserType")
                    {
                        mdl.UserType = result.FormData.GetValues(i)[0];
                    }
                    if (result.FormData.GetKey(i) == "Mobile")
                    {
                        mdl.Mobile = result.FormData.GetValues(i)[0];
                    }
                    if (result.FormData.GetKey(i) == "DeviceToken")
                    {
                        mdl.DeviceToken = result.FormData.GetValues(i)[0];
                    }
                    if (result.FormData.GetKey(i) == "DeviceType")
                    {
                        mdl.DeviceType = result.FormData.GetValues(i)[0];
                    }
                    if (result.FormData.GetKey(i) == "Latitude")
                    {
                        mdl.Latitude = result.FormData.GetValues(i)[0];
                    }
                    if (result.FormData.GetKey(i) == "Longitude")
                    {
                        mdl.Longitude = result.FormData.GetValues(i)[0];
                    }

                    if (result.FormData.GetKey(i) == "CreateDate")
                    {
                        mdl.CreateDate =Convert.ToDateTime(result.FormData.GetValues(i)[0]);
                    }
                    if (result.FormData.GetKey(i) == "CreateTime")
                    {
                        mdl.CreateTime = result.FormData.GetValues(i)[0];
                    }
                    if (result.FormData.GetKey(i) == "TaxiNumber")
                    {
                        mdl.TaxiNumber = result.FormData.GetValues(i)[0];
                    }
                    if (result.FormData.GetKey(i) == "AuthorityCardNo")
                    {
                        mdl.AuthorityCardNo = result.FormData.GetValues(i)[0];
                    }
                    if (result.FormData.GetKey(i) == "VarifyTime")
                    {
                        mdl.VarifyTime = result.FormData.GetValues(i)[0];
                    }
                    if (result.FormData.GetKey(i) == "PhoneVerified")
                    {
                        mdl.PhoneVerified = result.FormData.GetValues(i)[0];
                    }
                    if(result.FormData.GetKey(i)== "TaxiType")
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


        // POST api/Account/RegisterExternal
        [OverrideAuthentication]
        [HostAuthentication(DefaultAuthenticationTypes.ExternalBearer)]
        [Route("RegisterExternal")]
        public async Task<IHttpActionResult> RegisterExternal(RegisterExternalBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var info = await Authentication.GetExternalLoginInfoAsync();
            if (info == null)
            {
                return InternalServerError();
            }

            var user = new ApplicationUser() { UserName = model.Email, Email = model.Email };

            IdentityResult result = await UserManager.CreateAsync(user);
            if (!result.Succeeded)
            {
                return GetErrorResult(result);
            }

            result = await UserManager.AddLoginAsync(user.Id, info.Login);
            if (!result.Succeeded)
            {
                return GetErrorResult(result); 
            }
            return Ok();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && _userManager != null)
            {
                _userManager.Dispose();
                _userManager = null;
            }

            base.Dispose(disposing);
        }

        #region Helpers

        private IAuthenticationManager Authentication
        {
            get { return Request.GetOwinContext().Authentication; }
        }

        private IHttpActionResult GetErrorResult(IdentityResult result)
        {
            if (result == null)
            {
                return InternalServerError();
            }

            if (!result.Succeeded)
            {
                if (result.Errors != null)
                {
                    foreach (string error in result.Errors)
                    {
                        ModelState.AddModelError("", error);
                    }
                }

                if (ModelState.IsValid)
                {
                    // No ModelState errors are available to send, so just return an empty BadRequest.
                    return BadRequest();
                }

                return BadRequest(ModelState);
            }

            return null;
        }

        private class ExternalLoginData
        {
            public string LoginProvider { get; set; }
            public string ProviderKey { get; set; }
            public string UserName { get; set; }

            public IList<Claim> GetClaims()
            {
                IList<Claim> claims = new List<Claim>();
                claims.Add(new Claim(ClaimTypes.NameIdentifier, ProviderKey, null, LoginProvider));

                if (UserName != null)
                {
                    claims.Add(new Claim(ClaimTypes.Name, UserName, null, LoginProvider));
                }

                return claims;
            }

            public static ExternalLoginData FromIdentity(ClaimsIdentity identity)
            {
                if (identity == null)
                {
                    return null;
                }

                Claim providerKeyClaim = identity.FindFirst(ClaimTypes.NameIdentifier);

                if (providerKeyClaim == null || String.IsNullOrEmpty(providerKeyClaim.Issuer)
                    || String.IsNullOrEmpty(providerKeyClaim.Value))
                {
                    return null;
                }

                if (providerKeyClaim.Issuer == ClaimsIdentity.DefaultIssuer)
                {
                    return null;
                }

                return new ExternalLoginData
                {
                    LoginProvider = providerKeyClaim.Issuer,
                    ProviderKey = providerKeyClaim.Value,
                    UserName = identity.FindFirstValue(ClaimTypes.Name)
                };
            }
        }

        private static class RandomOAuthStateGenerator
        {
            private static RandomNumberGenerator _random = new RNGCryptoServiceProvider();

            public static string Generate(int strengthInBits)
            {
                const int bitsPerByte = 8;

                if (strengthInBits % bitsPerByte != 0)
                {
                    throw new ArgumentException("strengthInBits must be evenly divisible by 8.", "strengthInBits");
                }

                int strengthInBytes = strengthInBits / bitsPerByte;

                byte[] data = new byte[strengthInBytes];
                _random.GetBytes(data);
                return HttpServerUtility.UrlTokenEncode(data);
            }
        }

        #endregion
    }
}

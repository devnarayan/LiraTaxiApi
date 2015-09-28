using LiraTaxi.Data.Access.Driver;
using LiraTaxi.Data.DriverMdl;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LiraTaxi.Web.Areas.En.Controllers
{
    public class DriverController : Controller
    {
        private DriverService driverService = new DriverService();
        public DriverController()
        {
            driverService = new DriverService();
        }
        // GET: En/Driver
        public ActionResult Index()
        {
            ViewBag.UserName = User.Identity.Name;
            return View();
        }
        public ActionResult BookingHistory()
        {
            return View();
        }
       public ActionResult BookingDetails()
        {
            return View();
        }
        public ActionResult UpdateProfile()
        {
            return View();
        }
        public ActionResult BuynWay()
        {
            return View();
        }
        public ActionResult SaleNWay()
        {
            return View();
        }
        public ActionResult AccountDetails()
        {
            return View();
        }
        public ActionResult Offers()
        {
            return View();
        }
        public ActionResult ChangePassword()
        {
            return View();
        }
        public ActionResult ManageDocs()
        {
            return View();
        }

        #region DriverService
        public string GetDriverByUserName(string UserName)
        {
            DriverSearchModel model = new DriverSearchModel();
            model.SType = "UserName";
            model.SValue = UserName;
           DataTable dt= driverService.GetDriverSearch(model);
           return JsonConvert.SerializeObject(dt);
        }
        #endregion
    }
}
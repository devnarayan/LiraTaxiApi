using LiraTaxi.Web.Code;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LiraTaxi.Web.Areas.Admin.Controllers
{
    public class AccessController : BaseController
    {
        // GET: Admin/Access
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Login()
        {
            return View();
        }

        public ActionResult Signup(int Id)
        {
            if (Id == 1) ViewBag.Name = "Driver";
            else if (Id == 2) ViewBag.Name = "Passenger";
            else return RedirectToRoute("/");
            ViewBag.ID = Id;
            return View();
        }
    }
}
using LiraTaxi.Data.Access.Passenger;
using LiraTaxi.Data.PassengerMdl;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace LiraTaxi.Api.Controllers
{
    public class PassengerController : ApiController
    {
        PassengerService db = new PassengerService();
        //get all customer

        [HttpGet]
        public object Get()
        {
            DataTable dirver = db.GetPassengerList();
            if (dirver == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }
            if (dirver.Rows.Count > 0)
            {
                Dictionary<string, object> dic = new Dictionary<string, object>();
                dic.Add("Code", "200");
                dic.Add("Message", "Passenger get successfully.");
                dic.Add("data", dirver);
                return dic;
            }
            else
            {
                Dictionary<string, object> dic = new Dictionary<string, object>();
                dic.Add("Code", "201");
                dic.Add("Message", "Passenger not found!");
                dic.Add("data", dirver);
                return dic;
            }
        }

        //get customer by id
        public object Get(int id)
        {
            DataTable dirver = db.GetPassengerByID(id);
            if (dirver == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }
            if (dirver.Rows.Count > 0)
            {
                Dictionary<string, object> dic = new Dictionary<string, object>();
                dic.Add("Code", "200");
                dic.Add("Message", "Passenger get successfully.");
                dic.Add("data", dirver);
                return dic;
            }
            else
            {
                Dictionary<string, object> dic = new Dictionary<string, object>();
                dic.Add("Code", "201");
                dic.Add("Message", "Passenger not found!");
                dic.Add("data", dirver);
                return dic;
            }

        }
        public object GetPassengerByUserName(string UName)
        {
            DataTable dirver = db.GetPassengerSearch(0, "UserName", UName);
            if (dirver == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }
            if (dirver.Rows.Count > 0)
            {
                Dictionary<string, object> dic = new Dictionary<string, object>();
                dic.Add("Code", "200");
                dic.Add("Message", "Passenger get successfully.");
                dic.Add("data", dirver);
                return dic;
            }
            else
            {
                Dictionary<string, object> dic = new Dictionary<string, object>();
                dic.Add("Code", "201");
                dic.Add("Message", "Passenger not found!");
                dic.Add("data", dirver);
                return dic;
            }

        }
        public object GetByUserId(string UserId)
        {
            DataTable dirver = db.GetPassengerSearch(0, "UserId", UserId);
            if (dirver == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }
            if (dirver.Rows.Count > 0)
            {
                Dictionary<string, object> dic = new Dictionary<string, object>();
                dic.Add("Code", "200");
                dic.Add("Message", "Passenger get successfully.");
                dic.Add("data", dirver);
                return dic;
            }
            else
            {
                Dictionary<string, object> dic = new Dictionary<string, object>();
                dic.Add("Code", "201");
                dic.Add("Message", "Passenger not found!");
                dic.Add("data", dirver);
                return dic;
            }

        }

        //insert customer
        public HttpResponseMessage Post(PassengerModel Passenger)
        {
            if (ModelState.IsValid)
            {
                db.AddPassenger(Passenger);
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, Passenger);
                response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = Passenger.PassengerID }));
                return response;
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }

        //update customer
        public HttpResponseMessage Put(int id, PassengerModel Passenger)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
            if (id != Passenger.PassengerID)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
            try
            {
                db.EditPassenger(Passenger);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
            }
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        //delete customer by id
        public HttpResponseMessage Delete(int PassengerID)
        {
            if (PassengerID == 0)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }
            try
            {
                db.DeletePassenger(PassengerID);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
            }
            return Request.CreateResponse(HttpStatusCode.OK, PassengerID);
        }

        //prevent memory leak
        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }

    }
}

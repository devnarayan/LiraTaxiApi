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
    public class PassengerNWaySaleController : ApiController
    {
        PassengerNWaySaleService db = new PassengerNWaySaleService();
        //get all customer

        [HttpGet]
        public DataTable GetList(int PassengerID)
        {
            return db.GetNWaySaleList(PassengerID);
        }

        //get customer by id
        public DataTable Get(int PassengerNWaySaleID)
        {
            DataTable creditcard = db.GetNWaySale(PassengerNWaySaleID);
            if (creditcard == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }
            return creditcard;
        }

        //insert customer
        public HttpResponseMessage Post(PassengerNWaySaleModel nwaySale)
        {
            if (ModelState.IsValid)
            {
                db.AddPassengerNWaySale(nwaySale);
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, nwaySale);
                response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = nwaySale.PassengerNWaySaleID }));
                return response;
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }

        //update customer
        public HttpResponseMessage Put(int id, PassengerNWaySaleModel nwaySale)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
            if (id != nwaySale.PassengerNWaySaleID)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
            try
            {
                db.EditPassengerNWaySale(nwaySale);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
            }
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        //delete customer by id
        public HttpResponseMessage Delete(int id)
        {
            if (id == 0)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }
            try
            {
                db.DeletePassengerNWaySale(id);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
            }
            return Request.CreateResponse(HttpStatusCode.OK, id);
        }

        //prevent memory leak
        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}

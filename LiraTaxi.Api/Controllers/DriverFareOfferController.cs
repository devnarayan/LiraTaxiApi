using LiraTaxi.Data.Access.Driver;
using LiraTaxi.Data.DriverMdl;
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
    public class DriverFareOfferController : ApiController
    {
        DriverFareOfferService db = new DriverFareOfferService();
        //get all customer

        [HttpGet]
        public DataTable GetList(int driverID)
        {
            return db.GetFareOfferList(driverID);
        }

        //get customer by id
        public DataTable Get(int DriverFareOfferID)
        {
            DataTable creditcard = db.GetFareOffer(DriverFareOfferID);
            if (creditcard == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }
            return creditcard;
        }

        //insert customer
        public HttpResponseMessage Post(DriverFareOfferModel fareOffer)
        {
            if (ModelState.IsValid)
            {
                db.AddDriverFareOffer(fareOffer);
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, fareOffer);
                response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = fareOffer.DriverFareOfferID}));
                return response;
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }

        //update customer
        public HttpResponseMessage Put(int id, DriverFareOfferModel fareOffer)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
            if (id != fareOffer.DriverFareOfferID)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
            try
            {
                db.EditDriverFareOffer(fareOffer);
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
                db.DeleteDriverFareOffer(id);
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

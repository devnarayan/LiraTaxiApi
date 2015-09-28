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
    public class PassengerRatingController : ApiController
    {
        PassengerRatingService db = new PassengerRatingService();
        //get all customer

        [HttpGet]
        public DataTable GetList(int PassengerID)
        {
            return db.GetPassengerRatingList(PassengerID);
        }

        //get customer by id
        public DataTable Get(int PassengerID)
        {
            DataTable creditcard = db.GetPassengerRating(PassengerID);
            if (creditcard == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }
            return creditcard;
        }

        //insert customer
        public HttpResponseMessage Post(PassengerRatingModel PassengerRating)
        {
            if (ModelState.IsValid)
            {
                db.AddPassengerRating(PassengerRating);
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, PassengerRating);
                response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = PassengerRating.PassengerRatignID }));
                return response;
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
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
                db.DeletePassengerRating(id);
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

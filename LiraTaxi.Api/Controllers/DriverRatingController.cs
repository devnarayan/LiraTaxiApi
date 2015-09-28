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
    public class DriverRatingController : ApiController
    {
        DriverRatingService db = new DriverRatingService();
        //get all customer

        [HttpGet]
        public DataTable GetList(int driverID)
        {
            return db.GetDriverRatingList(driverID);
        }

        //get customer by id
        public DataTable Get(int driverID)
        {
            DataTable creditcard = db.GetDriverRating(driverID);
            if (creditcard == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }
            return creditcard;
        }

        //insert customer
        public HttpResponseMessage Post(DriverRatingModel driverRating)
        {
            if (ModelState.IsValid)
            {
                db.AddDriverRating(driverRating);
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, driverRating);
                response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = driverRating.DriverRatingID }));
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
                db.DeleteDriverRating(id);
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

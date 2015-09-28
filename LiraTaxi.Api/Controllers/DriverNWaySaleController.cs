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
    public class DriverNWaySaleController : ApiController
    {
        DriverNWaySaleService db = new DriverNWaySaleService();
        //get all customer

        [HttpGet]
        public DataTable GetList(int driverID)
        {
            return db.GetNWaySaleList(driverID);
        }

        //get customer by id
        public DataTable Get(int DriverNWaySaleID)
        {
            DataTable creditcard = db.GetNWaySale(DriverNWaySaleID);
            if (creditcard == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }
            return creditcard;
        }

        //insert customer
        public HttpResponseMessage Post(DriverNWaySaleModel nwaySale)
        {
            if (ModelState.IsValid)
            {
                db.AddDriverNWaySale(nwaySale);
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, nwaySale);
                response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = nwaySale.DriverNWaySaleID }));
                return response;
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }

        //update customer
        public HttpResponseMessage Put(int id, DriverNWaySaleModel nwaySale)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
            if (id != nwaySale.DriverNWaySaleID)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
            try
            {
                db.EditDriverNWaySale(nwaySale);
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
                db.DeleteDriverNWaySale(id);
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

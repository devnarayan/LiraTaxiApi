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
    public class OfferApplyController : ApiController
    {
        OfferApplyService db = new OfferApplyService();
        //get all customer

        [HttpGet]
        public DataTable GetList(int fareOfferID)
        {
            return db.GetOfferApplyList(fareOfferID);
        }

        //get customer by id
        public DataTable Get(int OfferApplyID)
        {
            DataTable creditcard = db.GetOfferApply(OfferApplyID);
            if (creditcard == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }
            return creditcard;
        }

        //insert customer
        public HttpResponseMessage Post(OfferApplyModel offerApply)
        {
            if (ModelState.IsValid)
            {
                db.AddOfferApply(offerApply);
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, offerApply);
                response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = offerApply.OfferApplyID}));
                return response;
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }

        //update customer
        public HttpResponseMessage Put(int id, OfferApplyModel offerApply)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
            if (id != offerApply.OfferApplyID)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
            try
            {
                db.EditOfferApply(offerApply);
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
                db.DeleteOfferApply(id);
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

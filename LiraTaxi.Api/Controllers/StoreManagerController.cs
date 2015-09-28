using LiraTaxi.Data.Access.Admin;
using LiraTaxi.Data.Model;
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
    public class StoreManagerController : ApiController
    {
        StoreManagerService db = new StoreManagerService();
        //get all customer

        [HttpGet]
        public DataTable Get()
        {
            StoreManagerSearchModel md = new StoreManagerSearchModel();
            md.StoreManagerID = 0;
            md.SType = "All";
            md.SValue = "All";
            DataTable dirver = db.GetStoreManagerSearch(md);
            if (dirver == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }
            return dirver;
        }

        //get customer by id
        public DataTable Get(int id)
        {
            StoreManagerSearchModel md = new StoreManagerSearchModel();
            md.StoreManagerID = id;
            md.SType = "StoreManagerID";
            md.SValue = "All";
            DataTable dirver = db.GetStoreManagerSearch(md);
            if (dirver == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }
            return dirver;
        }

        //insert customer
        public HttpResponseMessage Post(StoreManagerModel StoreManager)
        {
            if (ModelState.IsValid)
            {
                db.AddStoreManager(StoreManager);
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, StoreManager);
                response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = StoreManager.StoreManagerID }));
                return response;
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }

        //update customer
        public HttpResponseMessage Put(int id, StoreManagerModel StoreManager)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
            if (id != StoreManager.StoreManagerID)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
            try
            {
                db.EditStoreManager(StoreManager);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
            }
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        //delete customer by id
        public HttpResponseMessage Delete(int StoreManagerID)
        {
            if (StoreManagerID == 0)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }
            try
            {
                db.DeleteStoreManager(StoreManagerID);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
            }
            return Request.CreateResponse(HttpStatusCode.OK, StoreManagerID);
        }

        //prevent memory leak
        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }

    }
}

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
    public class PaneltyMasterController : ApiController
    {
        PaneltyMasterService db = new PaneltyMasterService();
        //get all customer

        [HttpGet]
        public DataTable Get()
        {
            DataTable dirver = db.GetPaneltyMasterList();
            if (dirver == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }
            return dirver;
        }

        //get customer by id
        public DataTable Get(int id)
        {
            DataTable dirver = db.GetPaneltyMasterByID(id);
            if (dirver == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }
            return dirver;
        }

        //insert customer
        public HttpResponseMessage Post(PaneltyMasterModel PaneltyMaster)
        {
            if (ModelState.IsValid)
            {
                db.AddPaneltyMaster(PaneltyMaster);
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, PaneltyMaster);
                response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = PaneltyMaster.PaneltyMasterID }));
                return response;
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }

        //update customer
        public HttpResponseMessage Put(int id, PaneltyMasterModel PaneltyMaster)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
            if (id != PaneltyMaster.PaneltyMasterID)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
            try
            {
                db.EditPaneltyMaster(PaneltyMaster);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
            }
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        //delete customer by id
        public HttpResponseMessage Delete(int PaneltyMasterID)
        {
            if (PaneltyMasterID == 0)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }
            try
            {
                db.DeletePaneltyMaster(PaneltyMasterID);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
            }
            return Request.CreateResponse(HttpStatusCode.OK, PaneltyMasterID);
        }

        //prevent memory leak
        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }

    }
}

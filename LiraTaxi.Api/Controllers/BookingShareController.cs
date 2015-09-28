using LiraTaxi.Data.Access.Booking;
using LiraTaxi.Data.Booking;
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
    public class BookingShareController : ApiController
    {
        BookingShareService db = new BookingShareService();
        //get all customer

        [HttpGet]
        public DataTable Get()
        {
            return null;
        }

        [HttpGet]
        public DataTable GetBookingShareList(int PassengerID)
        {
            DataTable customer = db.GetBookingsShareList(PassengerID);
            if (customer == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }
            return customer;
        }

        [HttpGet]
        public DataTable Get(int id)
        {
            DataTable customer = db.GetBookingShareDetails(id);
            if (customer == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }
            return customer;
        }

        //insert customer
        public HttpResponseMessage Post(BookingShareModel customer)
        {
            if (ModelState.IsValid)
            {
                db.AddBookingShare(customer);
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, customer);
                response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = customer.BookingShareID }));
                return response;
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }

        //update customer
        public HttpResponseMessage Put(int id, BookingShareModel customer)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
            if (id != customer.BookingShareID)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
            try
            {
                db.EditBookingShare(customer);
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
                db.DeleteBookingShare(id);
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

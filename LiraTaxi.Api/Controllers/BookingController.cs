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
    public class BookingController : ApiController
    {
        BookingService db = new BookingService();
        //get all customer
       
        [HttpGet]
        public DataTable Get()
        {
            return db.GetBookings();
        }

        //get customer by id
        public DataTable Get(int id)
        {
            DataTable customer = db.GetBooking(id);
            if (customer == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }
            return customer;
        }

        //insert customer
        public HttpResponseMessage Post(BookingModel customer)
        {
            if (ModelState.IsValid)
            {
                db.AddBooking(customer);
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, customer);
                response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = customer.BookingID }));
                return response;
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }

        //update customer
        public HttpResponseMessage Put(int id, BookingModel customer)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
            if (id != customer.BookingID)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
            try
            {
                db.EditBooking(customer);
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
            DataTable customer = db.GetBooking(id);
            if (customer == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }
            try
            {
                db.DeleteBooking(id);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
            }
            return Request.CreateResponse(HttpStatusCode.OK, customer);
        }

        //prevent memory leak
        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}

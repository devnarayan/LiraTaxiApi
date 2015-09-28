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
    public class BookingApplyController : ApiController
    {
        private BookingApplyService db = new BookingApplyService();
        //get BookingApply by id
        public DataTable Get(int id)
        {
            DataTable booking = db.GetBookigApply(id);
            if (booking == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }
            return booking;
        }

        //insert BookingApply
        public HttpResponseMessage Post(BookingApplyModel bookingApply)
        {
            if (ModelState.IsValid)
            {
                db.ApplyForBooking(bookingApply);
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, bookingApply);
                response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = bookingApply.BookingID }));
                return response;
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }


        //delete BookingApply by id
        public HttpResponseMessage Delete(int id)
        {
            if (id == 0)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }
            try
            {
                db.DeleteBookingApply(id);
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

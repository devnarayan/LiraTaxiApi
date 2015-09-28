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
    public class PassengerCreditCardController : ApiController
    {
        PassengerCreditCardService db = new PassengerCreditCardService();
        //get all customer

        [HttpGet]
        public DataTable GetList(int PassengerID)
        {
            return db.GetCreditCardList(PassengerID);
        }

        //get customer by id
        public DataTable Get(int CreditCardID)
        {
            DataTable creditcard = db.GetCreditCard(CreditCardID);
            if (creditcard == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }
            return creditcard;
        }

        //insert customer
        public HttpResponseMessage Post(PassengerCreditCardModel creditCard)
        {
            if (ModelState.IsValid)
            {
                db.AddPassengerCreditCard(creditCard);
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, creditCard);
                response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = creditCard.PassengerCreditCardID }));
                return response;
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }

        //update customer
        public HttpResponseMessage Put(int id, PassengerCreditCardModel creditCard)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
            if (id != creditCard.PassengerCreditCardID)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
            try
            {
                db.EditPassengerCreditCard(creditCard);
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
                db.DeletePassengerCreditCard(id);
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

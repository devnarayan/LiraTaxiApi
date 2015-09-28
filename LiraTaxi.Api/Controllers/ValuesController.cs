using LiraTaxi.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace LiraTaxi.Api.Controllers
{
    [Authorize]
    public class ValuesController : ApiController
    {
        // GET api/values
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public RegisterBindingModel Post(RegisterBindingModel model)
        {
            return model;
        }

        // PUT api/values/5
        [HttpPut]
        public RegisterBindingModel Put(int id,RegisterBindingModel userData)
        {
            return userData;
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace group_buy.Controllers
{
    public class SONController : ApiController
    {
        // GET: api/SON
        public string Get()
        {
            return "http://47.93.220.57/test.txt";
        }

        // GET: api/SON/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/SON
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/SON/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/SON/5
        public void Delete(int id)
        {
        }
    }
}

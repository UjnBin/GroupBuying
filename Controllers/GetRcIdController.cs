using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace group_buy.Controllers
{
    public class GetRcIdController : ApiController
    {
        // GET: api/GetRcId
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/GetRcId/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/GetRcId
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/GetRcId/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/GetRcId/5
        public void Delete(int id)
        {
        }
    }
}

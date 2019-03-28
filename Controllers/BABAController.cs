using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace group_buy.Controllers
{
    public class BABAController : ApiController
    {
        // GET: api/BABA
        public string Get()
        {
            if (HttpContext.Current.Session["BABAS"] == null )
            {
                HttpContext.Current.Session["BABAS"] = "BABA";
            }
            else
            {
                return HttpContext.Current.Session["BABAS"].ToString();
            }
            return "";
        }

        // GET: api/BABA/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/BABA
        public string Post()
        {
            return HttpContext.Current.Request.Form["seafood"];
        }

        // PUT: api/BABA/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/BABA/5
        public void Delete(int id)
        {
        }
    }
}

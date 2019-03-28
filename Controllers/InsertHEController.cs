using group_buy.MYSQL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Http;

namespace group_buy.Controllers
{
    public class InsertHEController : ApiController
    {
        // GET: api/InsertHE
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/InsertHE/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/InsertHE
        public string Post([FromBody]string value)
        {
            HttpContext.Current.Response.AddHeader("Access-Control-Allow-Credentials", "true");
            HttpContext.Current.Response.AppendHeader("Access-Control-Allow-Origin", "http://xmhome.xyz");//实现跨域
            string address = HttpContext.Current.Request.Form["address"].ToString().Trim();
            string[] ADS = Regex.Split(address, ",", RegexOptions.IgnoreCase);
            HashSet<string> hs = new HashSet<string>(ADS);
            if (HE.HE_INSERT(hs))
            {
                return "1";
            }
            else
            {
                return "0";
            }
        }

        // PUT: api/InsertHE/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/InsertHE/5
        public void Delete(int id)
        {
        }
    }
}

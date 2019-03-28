using group_buy.MYSQL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

/// <summary>
/// 立即关团
/// 2018.11.25
/// @author:Han Rubing, Li Yongshu
/// </summary>

namespace group_buy.Controllers
{
    public class CloseGroupController : ApiController
    {
        // GET: api/CloseGroup
        public string Get()
        {
            HttpContext.Current.Response.AddHeader("Access-Control-Allow-Credentials", "true");
            HttpContext.Current.Response.AppendHeader("Access-Control-Allow-Origin", "http://xmhome.xyz");//实现跨域
            if (TIME.timer != null)
            {
                TIME.timer.Enabled = false;
                TIME.timer.Stop();
            }
            if (ISOPEN.ISOPEN_CLOSE())
            {
                return "1";
            }
            else
                return "0";
        }

        // GET: api/CloseGroup/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/CloseGroup
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/CloseGroup/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/CloseGroup/5
        public void Delete(int id)
        {
        }
    }
}

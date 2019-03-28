using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Policy;
using System.Web;
using System.Web.Http;

/// <summary>
/// 301永久重定向
/// 2018.11.25
/// @author:hanrubing, liyongshu
/// </summary>

namespace group_buy.Controllers
{
    public class s301Controller : ApiController
    {
        // GET: api/s01
        public void Get()
        {
            string Url = "https://open.weixin.qq.com/connect/oauth2/authorize?appid=wx4acf59cd381f3e28&redirect_uri=http%3a%2f%2fxmhome.xyz%2fConch%2f&response_type=code&scope=snsapi_userinfo&state=STATE#wechat_redirect";
            HttpContext.Current.Response.StatusCode = 301;
            HttpContext.Current.Response.Status = "301 Moved Permanently";
            HttpContext.Current.Response.AppendHeader("Location", Url);
            HttpContext.Current.Response.AppendHeader("Cache-Control", "no-cache");  //这里很重要的一个设置， no-cache 表示不做本地缓存
            HttpContext.Current.Response.End();
        }

        // GET: api/s01/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/s01
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/s01/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/s01/5
        public void Delete(int id)
        {
        }
    }
}

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

/// <summary>
// 获取微信用户openid
/// 2018.11.23
/// @author:hanrubing, liyongshu
/// </summary>

namespace group_buy.Controllers
{
    public class GetOpenIdController : ApiController
    {
        // GET: api/GetOpenId
        public string Get(string code)
        {
            code = code.Trim();
            var request = (HttpWebRequest)WebRequest.Create("https://api.weixin.qq.com/sns/oauth2/access_token?appid=wx4acf59cd381f3e28&secret=04c36604ecb6165c4133eaaf71f4835b&code="+ code + "&grant_type=authorization_code");
            var response = (HttpWebResponse)request.GetResponse();
            var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
            string json = responseString.ToString();
            JObject jo = (JObject)JsonConvert.DeserializeObject(json);
            if(jo.Property("openid") != null)
            {
                return jo["openid"].ToString();
            }
            else
            {
                return "";
            }
        }

        // GET: api/GetOpenId/5
        public string Get(int id)
        {
            return "";
        }

        // POST: api/GetOpenId
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/GetOpenId/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/GetOpenId/5
        public void Delete(int id)
        {
        }
    }
}

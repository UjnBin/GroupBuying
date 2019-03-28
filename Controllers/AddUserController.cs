using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

/// <summary>
/// 添加，删除用户
/// 2018.11.25
/// @author:hanrubing, liyongshu
/// </summary>

namespace group_buy.Controllers
{
    public class AddUserController : ApiController
    {
        // GET: api/AddUser
        public string Get(string code, string name, string phone, string address)
        {
            HttpContext.Current.Response.AddHeader("Access-Control-Allow-Credentials", "true");
            HttpContext.Current.Response.AppendHeader("Access-Control-Allow-Origin", "http://xmhome.xyz");//实现跨域
            string user_id = "", client_name = "";
            code = code.Trim();
            var request = (HttpWebRequest)WebRequest.Create("https://api.weixin.qq.com/sns/oauth2/access_token?appid=wx4acf59cd381f3e28&secret=04c36604ecb6165c4133eaaf71f4835b&code=" + code + "&grant_type=authorization_code");
            var response = (HttpWebResponse)request.GetResponse();
            var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
            string json = responseString.ToString();
            JObject jo = (JObject)JsonConvert.DeserializeObject(json);
            if (jo.Property("openid") != null)
            {
                user_id = jo["openid"].ToString();
            }
            else
            {
                return "0";
                //return "异常，未获得正确的用户id";
            }
            if (user_id == null || user_id == "")
            {
                return "0";
                //return "异常，未获得正确的用户id";
            }
            if (jo.Property("access_token") != null)
            {
                string access_token = jo["access_token"].ToString();
                if (access_token != null && access_token != "" && user_id != null && user_id != "")
                {
                    var request_name = (HttpWebRequest)WebRequest.Create("https://api.weixin.qq.com/sns/userinfo?access_token=" + access_token + "&openid=" + user_id + "&lang=zh_CN");
                    var response_name = (HttpWebResponse)request_name.GetResponse();
                    var responseString_name = new StreamReader(response_name.GetResponseStream()).ReadToEnd();
                    string json_name = responseString_name.ToString();
                    JObject jo_name = (JObject)JsonConvert.DeserializeObject(json_name);
                    if (jo_name.Property("nickname") != null)
                    {
                        client_name = jo_name["nickname"].ToString();
                    }
                }
            }
            if (name == null || name == "" || address == null || address == "")
            {
                return "0";
                //return "添加失败,请输入正确的名字";
            }
            MySqlConnection add_user = MySQL.getMySqlConnection();
            MySqlCommand add_rcMySqlCommands = MySQL.getSqlCommand("INSERT user VALUES ('" + user_id + "','" + client_name + "','" + name + "','" + phone + "','" + address + "')", add_user);
            add_user.Open();
            if (add_rcMySqlCommands.ExecuteNonQuery() > 0)
            {
                add_user.Close();
                return "1";
                //return "添加成功";
            }
            add_user.Close();
            return "0";
            //return "添加失败，请勿重复添加，如有疑问，请联系管理员";
        }

        // GET: api/AddUser/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/AddUser
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/AddUser/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/AddUser/5
        public void Delete(int id)
        {
        }
    }
}

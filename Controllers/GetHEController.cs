using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

/// <summary>
/// 获取对应可提供服务的小区列表
/// 2018.11.25
/// @author:hanrubing, liyongshu
/// </summary>

namespace group_buy.Controllers
{
    public class GetHEController : ApiController
    {
        // GET: api/GetHE
        public JObject Get()
        {
            string json1 = "{ 'address' : [ ", json2 = "", json = "";
            HttpContext.Current.Response.AddHeader("Access-Control-Allow-Credentials", "true");
            HttpContext.Current.Response.AppendHeader("Access-Control-Allow-Origin", "http://xmhome.xyz");//实现跨域
            MySqlConnection mysql = MySQL.getMySqlConnection();
            MySqlCommand mySqlCommand = MySQL.getSqlCommand("SELECT * FROM housingestate", mysql);
            mysql.Open();//开启数据库连接
            int i = 0;
            MySqlDataReader reader = mySqlCommand.ExecuteReader();//执行sql语句
            try
            {
                while (reader.Read())
                {
                    if (reader.HasRows)//用seaFood封装读取会的数据并添加到list_sea_product
                    {
                        if ( i == 0)
                        {
                            json2 += "'" + reader.GetString(0) + "'";
                            i++;
                        }
                        else
                        {
                            json2 += ",'" + reader.GetString(0) + "'";
                        }
                    }
                }
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            finally
            {
                mysql.Close();
                json = json1 + json2 + "]}";
            }
            return (JObject)JsonConvert.DeserializeObject(json.ToString());
        }

        // GET: api/GetHE/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/GetHE
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/GetHE/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/GetHE/5
        public void Delete(int id)
        {
        }
    }
}

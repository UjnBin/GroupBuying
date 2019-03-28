using group_buy.MYSQL;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Net;
using System.Web;
using System.Web.Http;

/// <summary>
/// 返回团长所对应的伪单
/// 2018.12.3
/// @author:Han Rubing, Li Yongshu
/// </summary>


namespace group_buy.Controllers
{
    public class GetRCPSingleController : ApiController
    {
        // GET: api/GetRCPSingle
        public JObject Get(string rc_id)
        {
            HashSet<string> hs = new HashSet<string>();
            HttpContext.Current.Response.AddHeader("Access-Control-Allow-Credentials", "true");
            HttpContext.Current.Response.AppendHeader("Access-Control-Allow-Origin", "http://xmhome.xyz");//实现跨域
            string json = "{ 'ordernumber' : [";
            int number = NUMBER.NUMBER_SELECT();//获取当前期数
            int i = 0;
            MySqlConnection mysql = MySQL.getMySqlConnection();
            MySqlCommand mySqlCommand = MySQL.getSqlCommand("SELECT order_number FROM orderform_1 WHERE group_number = " + number.ToString() + " and rc_id = '" + rc_id + "'" + " and p_single = 1", mysql);
            mysql.Open();//开启数据库连接
            MySqlDataReader reader = mySqlCommand.ExecuteReader();//执行sql语句
            try
            {
                while (reader.Read())
                {
                    if (reader.HasRows)//用seaFood封装读取会的数据并添加到list_sea_product
                    {
                        hs.Add(reader.GetString(0));
                    }
                }
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            finally
            {
                foreach ( string s in hs )
                {
                    if (i == 0)
                    {
                        json += "'" + s + "'";
                        i++;
                    }
                    else
                    {
                        json += ", '" + s + "'";
                    }
                }
                mysql.Close();
            }
            json += "]}";
            return (JObject)JsonConvert.DeserializeObject(json.ToString());
        }

        // GET: api/GetRCPSingle/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/GetRCPSingle
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/GetRCPSingle/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/GetRCPSingle/5
        public void Delete(int id)
        {
        }
    }
}

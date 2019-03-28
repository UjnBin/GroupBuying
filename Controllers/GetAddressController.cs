using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace group_buy.Controllers
{
    public class GetAddressController : ApiController
    {
        // GET: api/GetAddress
        public IEnumerable<string> Get()
        {
            HttpContext.Current.Response.AddHeader("Access-Control-Allow-Credentials", "true");
            HttpContext.Current.Response.AppendHeader("Access-Control-Allow-Origin", "http://xmhome.xyz");//实现跨域
            List<string> list_sea_product = new List<string>();
            MySqlConnection mysql = MySQL.getMySqlConnection();
            MySqlCommand mySqlCommand = MySQL.getSqlCommand("SELECT * FROM housingestate", mysql);
            mysql.Open();//开启数据库连接
            MySqlDataReader reader = mySqlCommand.ExecuteReader();//执行sql语句
            try
            {
                while (reader.Read())
                {
                    if (reader.HasRows)//用seaFood封装读取会的数据并添加到list_sea_product
                    {
                        string s = reader.GetString(0);
                        list_sea_product.Add(s);
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
            }
            return list_sea_product;
        }
        // GET: api/GetAddress/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/GetAddress
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/GetAddress/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/GetAddress/5
        public void Delete(int id)
        {
        }
    }
}

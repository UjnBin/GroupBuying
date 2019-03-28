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
    public class RCC
    {
        public string rc_id { get; set; }
        public string re_name { get; set; }
        public string rc_phone { get; set; }
        public string re_nickame { get; set; }
        public string rc_address { get; set; }
    }

    public class GetRCCController : ApiController
    {
        // GET: api/GetRCC
        public IEnumerable<RCC> Get()
        {
            HttpContext.Current.Response.AddHeader("Access-Control-Allow-Credentials", "true");
            HttpContext.Current.Response.AppendHeader("Access-Control-Allow-Origin", "http://xmhome.xyz");//实现跨域
            List<RCC> list_sea_product = new List<RCC>();
            MySqlConnection mysql = MySQL.getMySqlConnection();
            MySqlCommand mySqlCommand = MySQL.getSqlCommand("SELECT * FROM reg_c", mysql);
            mysql.Open();//开启数据库连接
            MySqlDataReader reader = mySqlCommand.ExecuteReader();//执行sql语句
            try
            {
                while (reader.Read())
                {
                    if (reader.HasRows)//用seaFood封装读取会的数据并添加到list_sea_product
                    {
                        RCC rcc = new RCC();
                        rcc.rc_id = reader.GetString(0);
                        rcc.re_nickame = reader.GetString(1);
                        rcc.re_name = reader.GetString(2);
                        rcc.rc_phone = reader.GetString(3);
                        rcc.rc_address = reader.GetString(4);
                        list_sea_product.Add(rcc);
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

        // GET: api/GetRCC/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/GetRCC
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/GetRCC/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/GetRCC/5
        public void Delete(int id)
        {
        }
    }
}

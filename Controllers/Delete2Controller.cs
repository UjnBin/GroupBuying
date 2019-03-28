using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

/// <summary>
/// 删除产品
/// 2018.11.25
/// @author:Han Rubing, Li Yongshu
/// </summary>

namespace group_buy.Controllers
{
    public class Delete2Controller : ApiController
    {
        // GET: api/Delete2
        public string Get(string ids)
        {
            HttpContext.Current.Response.AddHeader("Access-Control-Allow-Credentials", "true");
            HttpContext.Current.Response.AppendHeader("Access-Control-Allow-Origin", "http://xmhome.xyz");//实现跨域
            MySqlConnection delete2 = MySQL.getMySqlConnection();
            MySqlCommand delete2Command = MySQL.getSqlCommand("DELETE FROM p_table_1 WHERE product_id = '" + ids.ToString() + "'", delete2);
            delete2.Open();//开启数据库连接
            if (delete2Command.ExecuteNonQuery() > 0)
            {
                delete2.Close();
                return "1";
            }
            else
            {
                delete2.Close();
                return "0";
            }
        }

        // GET: api/Delete2/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Delete2
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Delete2/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Delete2/5
        public void Delete(int id)
        {
        }
    }
}

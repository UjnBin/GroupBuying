using group_buy.MYSQL;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

/// <summary>
/// 修改商品价格
/// 2018.11.25
/// @author:hanrubing, liyongshu
/// </summary>

namespace group_buy.Controllers
{
    public class AlterPriceController : ApiController
    {
        // GET: api/AlterPrice
        public string Get(string product_id, float price)
        {
            HttpContext.Current.Response.AddHeader("Access-Control-Allow-Credentials", "true");
            HttpContext.Current.Response.AppendHeader("Access-Control-Allow-Origin", "http://xmhome.xyz");//实现跨域
            if ( ISOPEN.ISOPEN_SELECT())
            {
                return "0";
            }
            MySqlConnection alter = MySQL.getMySqlConnection();
            MySqlCommand alterMySqlCommands = MySQL.getSqlCommand("UPDATE p_table_1 SET product_price = " + price.ToString() + " WHERE product_id = '" + product_id + "'", alter);
            alter.Open();
            if (alterMySqlCommands.ExecuteNonQuery() > 0)
            {
                alter.Close();
                return "1";
            }
            alter.Close();
            return "0";
        }

        // GET: api/AlterPrice/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/AlterPrice
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/AlterPrice/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/AlterPrice/5
        public void Delete(int id)
        {
        }
    }
}

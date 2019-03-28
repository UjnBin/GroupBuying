using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

/// <summary>
/// 删除订单
/// 2018.11.25
/// @author:Han Rubing, Li Yongshu
/// </summary>

namespace group_buy.Controllers
{
    public class DeleteController : ApiController
    {
        // GET: api/Delete
        public string Get(string ordernumber)
        {
            HttpContext.Current.Response.AddHeader("Access-Control-Allow-Credentials", "true");
            HttpContext.Current.Response.AppendHeader("Access-Control-Allow-Origin", "http://xmhome.xyz");//实现跨域
            MySqlConnection delete = MySQL.getMySqlConnection();
            MySqlCommand deleteCommand = MySQL.getSqlCommand("DELETE FROM orderform_1 WHERE order_number = '" + ordernumber.ToString() + "'", delete);
            delete.Open();//开启数据库连接
            if(deleteCommand.ExecuteNonQuery() > 0)
            {
                delete.Close();
                return "1";
            }
            else
            {
                delete.Close();
                return "0";
            }
        }

        // GET: api/Delete/5
        public string Get()
        {
            return "as";
        }

        // POST: api/Delete
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Delete/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Delete/5
        public void Delete(int id)
        {
        }
    }
}

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
    public class DeleteRCController : ApiController
    {
        // GET: api/DeleteRC
        public string Get(string rc_id)
        {
            HttpContext.Current.Response.AddHeader("Access-Control-Allow-Credentials", "true");
            HttpContext.Current.Response.AppendHeader("Access-Control-Allow-Origin", "http://xmhome.xyz");//实现跨域
            MySqlConnection add_rc = MySQL.getMySqlConnection();
            MySqlCommand add_rcMySqlCommands = MySQL.getSqlCommand("DELETE FROM reg_c WHERE rc_id = '" + rc_id + "'", add_rc);
            add_rc.Open();
            if (add_rcMySqlCommands.ExecuteNonQuery() > 0)
            {
                add_rc.Close();
                return "1";
            }
            add_rc.Close();
            return "0";
        }

        // GET: api/DeleteRC/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/DeleteRC
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/DeleteRC/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/DeleteRC/5
        public void Delete(int id)
        {
        }
    }
}

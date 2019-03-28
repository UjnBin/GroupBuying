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
    public class DeleteAddressController : ApiController
    {
        // GET: api/DeleteAddress
        public string Get(string address)
        {
            HttpContext.Current.Response.AddHeader("Access-Control-Allow-Credentials", "true");
            HttpContext.Current.Response.AppendHeader("Access-Control-Allow-Origin", "http://xmhome.xyz");//实现跨域
            MySqlConnection add_rc1 = MySQL.getMySqlConnection();
            MySqlCommand add_rcMySqlCommands1 = MySQL.getSqlCommand("DELETE FROM housingestate WHERE address = '" + address + "'", add_rc1);
            add_rc1.Open();
            if (add_rcMySqlCommands1.ExecuteNonQuery() > 0)
            {
                add_rc1.Close();
            }
            else
            {
                add_rc1.Close();
                return "0";
            }
            MySqlConnection add_rc = MySQL.getMySqlConnection();
            MySqlCommand add_rcMySqlCommands = MySQL.getSqlCommand("DELETE FROM reg_c WHERE rc_address = '" + address + "'", add_rc);
            add_rc.Open();
            if (add_rcMySqlCommands.ExecuteNonQuery() > 0)
            {
                add_rc.Close();
                return "1";
            }
            else
            {
                add_rc.Close();
                return "0";
            }
        }

        // GET: api/DeleteAddress/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/DeleteAddress
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/DeleteAddress/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/DeleteAddress/5
        public void Delete(int id)
        {
        }
    }
}

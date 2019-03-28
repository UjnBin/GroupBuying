using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

/// <summary>
/// 添加海鲜产品列表
/// 2018.11.13
/// @author:hanrubing, liyongshu
/// </summary>

namespace group_buy.Controllers
{
    public class AddSeaFoodController : ApiController
    {
        // GET: api/AddSeaFood
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/AddSeaFood/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/AddSeaFood
        public string Post()
        {
            string product_name = HttpContext.Current.Request.Form["product_name"].ToString();
            string product_id = HttpContext.Current.Request.Form["product_id"].ToString();
            string product_price = HttpContext.Current.Request.Form["product_price"].ToString();
            string product_intro = HttpContext.Current.Request.Form["product_intro"].ToString();
            string product_picture = "";
            try
            {
                HttpFileCollection file = HttpContext.Current.Request.Files;
                if (file.Count > 0)
                {
                    //文件名  
                    string name = file[0].FileName;
                    //保存文件到指定路径   
                    string path = "C:\\inetpub\\wwwroot\\GROUP_BUY_PRODUCT\\" + name;
                    product_picture = "http://47.93.220.57//GROUP_BUY_PRODUCT//" + name;
                    file[0].SaveAs(path);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            MySqlConnection mysql = MySQL.getMySqlConnection();
            MySqlCommand mySqlCommand = MySQL.getSqlCommand(("INSERT INTO p_table_1 VALUES('"
                + product_name + "','" +product_id + "'," + product_price +",'" 
                + product_picture + "','" +product_intro + "')" ), mysql);
            mysql.Open();
            if (mySqlCommand.ExecuteNonQuery() > 0)
            {
                mysql.Close();
                return "1";
            }
            mysql.Close();
            return "0";
        }

        // PUT: api/AddSeaFood/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/AddSeaFood/5
        public void Delete(int id)
        {
        }
    }
}

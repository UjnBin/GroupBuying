using group_buy.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

/// <summary>
/// 通过特定的用户id和开团次数返回购物清单
/// 2018.11.13
/// @author:hanrubing, liyongshu
/// </summary>
namespace group_buy.Controllers
{
    public class GetShopListController : ApiController
    {
        // GET: api/GetShopList
        public IEnumerable<ShopLists> Get(string order_number)
        {
            HttpContext.Current.Response.AddHeader("Access-Control-Allow-Credentials", "true");
            HttpContext.Current.Response.AppendHeader("Access-Control-Allow-Origin", "http://xmhome.xyz");//实现跨域
            order_number = order_number.Trim();
            List<ShopLists> shop_list = new List<ShopLists>();
            MySqlConnection mysql = MySQL.getMySqlConnection();
            MySqlCommand mySqlCommand = MySQL.getSqlCommand("SELECT * FROM orderform_1 WHERE order_number = '"
                + order_number + "'", mysql);
            mysql.Open();//开启数据库连接
            MySqlDataReader reader = mySqlCommand.ExecuteReader();//执行sql语句
            try
            {
                while (reader.Read())
                {
                    if (reader.HasRows)//用shop封装读取会的数据并添加到shop_list
                    {
                        ShopLists shop = new ShopLists();
                        shop.open_id = reader.GetString(0);
                        shop.client_name = reader.GetString(1);
                        shop.group_number = reader.GetInt32(2);
                        shop.order_number = reader.GetString(3);
                        shop.product_name = reader.GetString(4);
                        shop.product_number = reader.GetInt32(5);
                        shop.product_Id = reader.GetString(6);
                        shop.product_price = reader.GetFloat(7);
                        shop.rc_id = reader.GetString(8);
                        shop.p_single = reader.GetInt16(9);
                        shop_list.Add(shop);
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
            return shop_list;
        }

        // GET: api/GetShopList/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/GetShopList
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/GetShopList/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/GetShopList/5
        public void Delete(int id)
        {
        }
    }
}

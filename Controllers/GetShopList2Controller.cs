using group_buy.Models;
using group_buy.MYSQL;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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
    public class GetShopList2Controller : ApiController
    {
        // GET: api/GetShopList2
        public JObject Get(string rc_id)
        {
            HttpContext.Current.Response.AddHeader("Access-Control-Allow-Credentials", "true");
            HttpContext.Current.Response.AppendHeader("Access-Control-Allow-Origin", "http://xmhome.xyz");//实现跨域
            rc_id = rc_id.Trim();
            HashSet<string> hs = new HashSet<string>();
            int number = NUMBER.NUMBER_SELECT();//获取当前期数
            MySqlConnection mysql = MySQL.getMySqlConnection();
            MySqlCommand mySqlCommand = MySQL.getSqlCommand("SELECT order_number FROM orderform_1 WHERE group_number = " + number.ToString() + " and rc_id = '" + rc_id + "'", mysql);
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
            string jsons = "{'product':[";
            int j = 0;
            MySqlConnection oss = MySQL.getMySqlConnection();
            MySqlCommand ossCommands;
            foreach ( string s in hs )
            {
                int i = 0;
                ossCommands = MySQL.getSqlCommand("SELECT * FROM orderform_1 WHERE order_number = '" + s.Trim() + "'", oss);
                oss.Open();//开启数据库连接
                MySqlDataReader readers = ossCommands.ExecuteReader();//执行sql语句
                try
                {
                    while (readers.Read())
                    {
                        if (readers.HasRows)//用shop封装读取会的数据并添加到shop_list
                        {
                            ShopLists shop = new ShopLists();
                            shop.open_id = readers.GetString(0);
                            shop.client_name = readers.GetString(1);
                            shop.group_number = readers.GetInt32(2);
                            shop.order_number = readers.GetString(3);
                            shop.product_name = readers.GetString(4);
                            shop.product_number = readers.GetInt32(5);
                            shop.product_Id = readers.GetString(6);
                            shop.product_price = readers.GetFloat(7);
                            shop.rc_id = readers.GetString(8);
                            shop.p_single = readers.GetInt32(9);
                            if (j == 0)
                            {
                                if (i == 0)
                                {
                                    jsons += "{ 'open_id':'" + shop.open_id + "','client_name':'" + shop.client_name + "','group_number':'" + shop.group_number + "','order_number':'" + shop.order_number + "','rc_id':'" + rc_id + "',";
                                    i++;
                                    jsons += "'products':[{'product_name':'" + shop.product_name + "','product_price':'" + shop.product_price + "','product_number':'" + shop.product_number + "'}";
                                }
                                else
                                {
                                    jsons += ",{'product_name':'" + shop.product_name + "','product_price':'" + shop.product_price + "','product_number':'" + shop.product_number + "'}";
                                }
                                j++;
                            }
                            else
                            {
                                if (i == 0)
                                {
                                    jsons += ",{ 'open_id':'" + shop.open_id + "','client_name':'" + shop.client_name + "','group_number':'" + shop.group_number + "','order_number':'" + shop.order_number + "','rc_id':'" + rc_id + "',";
                                    i++;
                                    jsons += "'products':[{'product_name':'" + shop.product_name + "','product_price':'" + shop.product_price + "','product_number':'" + shop.product_number + "'}";
                                }
                                else
                                {
                                    jsons += ",{'product_name':'" + shop.product_name + "','product_price':'" + shop.product_price + "','product_number':'" + shop.product_number + "'}";
                                }
                            }
                        }
                    }
                    jsons += "]}";
                }
                catch (Exception e)
                {
                    throw e;
                }
                finally
                {
                    oss.Close();
                }
            }
            jsons += "]}";
            //return jsons;
            return (JObject)JsonConvert.DeserializeObject(jsons.ToString());
        }

        // GET: api/GetShopList2/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/GetShopList2
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/GetShopList2/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/GetShopList2/5
        public void Delete(int id)
        {
        }
    }
}

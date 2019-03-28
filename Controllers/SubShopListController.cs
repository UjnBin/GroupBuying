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
/// 提交消费清单
/// 2018.11.13
/// @author:hanrubing, liyongshu
/// </summary>

namespace group_buy.Controllers
{
    public class SubShopListController : ApiController
    {
        // GET: api/SubShopList
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/SubShopList/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/SubShopList
        public string Post([FromBody]JObject jobject)
        {
            HttpContext.Current.Response.AddHeader("Access-Control-Allow-Credentials", "true");
            HttpContext.Current.Response.AppendHeader("Access-Control-Allow-Origin", "http://xmhome.xyz");//实现跨域
            if ( !ISOPEN.ISOPEN_SELECT() )//当前非在团购时间内，不可插入
            {
                return "-1";
            }
            JToken jtokValue = jobject["value"];//约定键值value,除value外均为单条属性
            JToken jtok_OpenId = jobject["open_id"];
            JToken jtok_name = jobject["client_name"];
            JToken jtok_GroupNumber = jobject["group_number"];
            JToken jtok_OrderNumber = jobject["order_number"];
            JToken jtok_RCId = jobject["rc_id"];
            JToken jtok_PSingle = jobject["p_single"];
            string json_value = jtokValue.ToString().Trim();
            List<Shop_Product> List_Product = JsonConvert.DeserializeObject<List<Shop_Product>>(json_value);
            string open_id = jtok_OpenId.ToString().Trim();//对应键的值转化为字符串
            string client_name = jtok_name.ToString().Trim();
            string group_number = jtok_GroupNumber.ToString().Trim();
            string order_number = jtok_OrderNumber.ToString().Trim();
            string rc_id = jtok_RCId.ToString().Trim();
            string p_single = jtok_PSingle.ToString().Trim();
            MySqlConnection mysql = MySQL.getMySqlConnection();
            MySqlCommand mySqlCommand = null;
            bool flag = false;//插入标志，如果为true代表其中一(多)条数据插入失败
            if (OrderForm_1.OrderForm_1_Existence(open_id, NUMBER.NUMBER_SELECT()) && p_single != null && p_single == "0")
            {//存在当前期数购物记录
                return "2";
            }
            foreach ( Shop_Product Item in List_Product )
            {
                string product_name = "";
                float product_price = 0f;
                MySqlConnection get = MySQL.getMySqlConnection();
                MySqlCommand getCommand = MySQL.getSqlCommand("SELECT product_name,product_price FROM p_table_1 WHERE product_id = " + Item.product_Id, get);
                get.Open();//开启数据库连接
                MySqlDataReader reader = getCommand.ExecuteReader();//执行sql语句
                try
                {
                    while (reader.Read())
                    {
                        if (reader.HasRows)//用seaFood封装读取会的数据并添加到list_sea_product
                        {
                            product_name = reader.GetString(0);
                            product_price = reader.GetFloat(1);
                        }
                    }
                }
                catch
                {
                    throw new HttpResponseException(HttpStatusCode.NotFound);
                }
                finally
                {
                    get.Close();
                }
                mySqlCommand = MySQL.getSqlCommand(("INSERT INTO orderform_1 VALUES('" +
                    open_id + "','" + client_name + "'," + group_number +
                    ",'" + order_number + "','" + product_name + "'," + Item.product_number.ToString() + ",'" + Item.product_Id + 
                    "'," + product_price.ToString() + ",'" + rc_id +"'," + p_single.ToString()+
                    ")"), mysql);
                mysql.Open();
                if (mySqlCommand.ExecuteNonQuery() > 0 ){}
                else
                {
                    flag = true;
                }
                mysql.Close();
            }
            if ( flag )
            {
                return "0";
            }
            else
            {
                return "1";
            }
        }

        // PUT: api/SubShopList/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/SubShopList/5
        public void Delete(int id)
        {
        }
    }
}

using group_buy.Models;
using group_buy.MYSQL;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Web;
using System.Web.Http;

/// <summary>
/// 返回所有产品信息
/// 2018.11.3
/// @author:Han Rubing, Li Yongshu
/// </summary>

namespace group_buy.Controllers
{

    public class Get_Product_1Controller : ApiController
    {
        // GET: api/Get_Product_1
        public IEnumerable<sea_product> Get()
        {
            HttpContext.Current.Response.AddHeader("Access-Control-Allow-Credentials", "true");
            HttpContext.Current.Response.AppendHeader("Access-Control-Allow-Origin", "http://xmhome.xyz");//实现跨域
            List<sea_product> list_sea_product = new List<sea_product>();
            MySqlConnection mysql = MySQL.getMySqlConnection();
            MySqlCommand mySqlCommand = MySQL.getSqlCommand("SELECT * FROM p_table_1", mysql);
            mysql.Open();//开启数据库连接
            MySqlDataReader reader = mySqlCommand.ExecuteReader();//执行sql语句
            try
            {
                while (reader.Read())
                {
                    if (reader.HasRows)//用seaFood封装读取会的数据并添加到list_sea_product
                    {
                        sea_product seaFood = new sea_product();
                        seaFood.product_name = reader.GetString(0);
                        seaFood.product_id = reader.GetString(1);
                        seaFood.product_price = reader.GetFloat(2);
                        seaFood.product_picture_url = reader.GetString(3);
                        seaFood.product_intro = reader.GetString(4);
                        list_sea_product.Add(seaFood);
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

        // GET: api/Get_Product_1/5
        public JObject Get(string code)
        {
            HttpContext.Current.Response.AddHeader("Access-Control-Allow-Credentials", "true");
            HttpContext.Current.Response.AppendHeader("Access-Control-Allow-Origin", "http://xmhome.xyz");//实现跨域
            string openId = "123";//用户openid
            string access_token = "";//口令
            string client_name = "123456";//客户微信名
            if (HttpContext.Current.Session["client"] == null)
            {
                code = code.Trim();
                var request = (HttpWebRequest)WebRequest.Create("https://api.weixin.qq.com/sns/oauth2/access_token?appid=wx4acf59cd381f3e28&secret=04c36604ecb6165c4133eaaf71f4835b&code=" + code + "&grant_type=authorization_code");
                var response = (HttpWebResponse)request.GetResponse();
                var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
                string json = responseString.ToString();
                JObject jo = (JObject)JsonConvert.DeserializeObject(json);
                if (jo.Property("openid") != null)
                {
                    openId = jo["openid"].ToString();//获取用户openid
                    HttpCookie cookie = new HttpCookie("client2");
                    cookie.Value = openId;
                    HttpContext.Current.Response.Cookies.Add(cookie);
                    HttpContext.Current.Session["client"] = openId;
                }//获取用户openid
                if (jo.Property("access_token") != null)
                {
                    access_token = jo["access_token"].ToString();
                    if (access_token != null && access_token != "" && openId != null && openId != "")
                    {
                        var request_name = (HttpWebRequest)WebRequest.Create("https://api.weixin.qq.com/sns/userinfo?access_token=" + access_token + "&openid=" + openId + "&lang=zh_CN");
                        var response_name = (HttpWebResponse)request_name.GetResponse();
                        var responseString_name = new StreamReader(response_name.GetResponseStream()).ReadToEnd();
                        string json_name = responseString_name.ToString();
                        JObject jo_name = (JObject)JsonConvert.DeserializeObject(json_name);
                        if (jo_name.Property("nickname") != null)
                        {
                            client_name = jo_name["nickname"].ToString();
                            HttpCookie cookie = new HttpCookie("nickname2");
                            cookie.Value = client_name;
                            HttpContext.Current.Response.Cookies.Add(cookie);
                            HttpContext.Current.Session["nickname"] = client_name;
                        }
                    }
                }
            }
            else
            {
                openId = HttpContext.Current.Session["client"].ToString();
            }

            if (HttpContext.Current.Request.Cookies["client2"] != null)
            {
                openId = HttpContext.Current.Request.Cookies["client2"].Value.ToString();
            }

            if (HttpContext.Current.Request.Cookies["nickname2"] != null)
            {
                client_name = HttpContext.Current.Request.Cookies["nickname2"].Value.ToString();
            }

            if (HttpContext.Current.Session["nickname"] != null)
            {
                client_name = HttpContext.Current.Session["nickname"].ToString();
            }

            string isrc = "0";

            if (string.IsNullOrWhiteSpace(openId))
            {
                return (JObject)JsonConvert.DeserializeObject("");
            }

            if (openId == "")
            {
                return (JObject)JsonConvert.DeserializeObject("");
            }

            if (REG_C.REG_C_SELECT(openId))
            {
                isrc = "1";
            }

            string ISREG = "0";

            if (USER.USER_SELECT(openId))
            {
                ISREG = "1";
            }

            int number = NUMBER.NUMBER_SELECT();//获取当前期数

            string address_rc_id = "";
            MySqlConnection mysql_id = MySQL.getMySqlConnection();
            MySqlCommand mySqlCommand_id = MySQL.getSqlCommand("SELECT user_address FROM user where user_id = '" + openId + "'", mysql_id);
            mysql_id.Open();//开启数据库连接
            MySqlDataReader reader_id = mySqlCommand_id.ExecuteReader();//执行sql语句
            try
            {
                while (reader_id.Read())
                {
                    if (reader_id.HasRows)//用seaFood封装读取会的数据并添加到list_sea_product
                    {
                        address_rc_id = reader_id.GetString(0);
                    }
                }
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            finally
            {
                mysql_id.Close();
            }

            MySqlConnection mysql_rcid = MySQL.getMySqlConnection();
            MySqlCommand mySqlCommand_rcid = MySQL.getSqlCommand("SELECT rc_id FROM reg_c where rc_address = '" + address_rc_id + "'", mysql_rcid);
            mysql_rcid.Open();//开启数据库连接
            MySqlDataReader reader_rcid = mySqlCommand_rcid.ExecuteReader();//执行sql语句
            try
            {
                while (reader_rcid.Read())
                {
                    if (reader_rcid.HasRows)//用seaFood封装读取会的数据并添加到list_sea_product
                    {
                        address_rc_id = reader_rcid.GetString(0);
                    }
                }
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            finally
            {
                mysql_rcid.Close();
            }

            string jsons = "";//最终的模式字符串
            string tableName = "group_ptable";//默认是当前购物期表
            string sqls = "";
            bool flag = false;
            flag = OrderForm_1.OrderForm_1_Existence(openId, number);//用户清单标志
            int x = 0;
            if (flag)//true 存在当前期数购物记录
            {
                x = 1;
                tableName = "orderform_1";
                sqls = " and client_id = '" + openId + "'" + " and p_single = 0";
            }
            List<sea_product_group> list_sea_product = new List<sea_product_group>();
            MySqlConnection mysql = MySQL.getMySqlConnection();
            MySqlCommand mySqlCommand = MySQL.getSqlCommand("SELECT * FROM " + tableName + " WHERE group_number = " + number.ToString() + sqls, mysql);
            mysql.Open();//开启数据库连接
            int i = 0;
            string order_number = "";
            MySqlDataReader reader = mySqlCommand.ExecuteReader();//执行sql语句
            try
            {
                while (reader.Read())
                {
                    if (reader.HasRows)//用seaFood封装读取会的数据并添加到list_sea_product
                    {
                        sea_product_group seaFood = new sea_product_group();
                        if (flag)
                        {
                            order_number = reader.GetString(3);
                            seaFood.product_name = reader.GetString(4);
                            seaFood.product_id = reader.GetString(6);
                            seaFood.product_price = reader.GetFloat(7);
                            int product_number = reader.GetInt16(5);
                            MySqlConnection order = MySQL.getMySqlConnection();
                            MySqlCommand orderCommand = MySQL.getSqlCommand("SELECT * FROM p_table_1 WHERE product_id = " + seaFood.product_id, order);
                            order.Open();//开启数据库连接
                            MySqlDataReader OrderReader = orderCommand.ExecuteReader();
                            try
                            {
                                while (OrderReader.Read())
                                {
                                    if (OrderReader.HasRows)
                                    {
                                        seaFood.product_picture_url = OrderReader.GetString(3);
                                        seaFood.product_intro = OrderReader.GetString(4);
                                    }
                                }
                            }
                            catch
                            {
                                throw new HttpResponseException(HttpStatusCode.NotFound);
                            }
                            finally
                            {
                                order.Close();
                            }
                            if (i == 0)
                            {
                                jsons += "{" + "'product_name':'" + seaFood.product_name + "','product_id':'" + seaFood.product_id + "','product_price':" + seaFood.product_price.ToString() + ",'product_picture_url':'" + seaFood.product_picture_url + "','product_intro':'" + seaFood.product_intro + "', 'product_number':" + product_number.ToString() + "}";
                                i++;
                            }
                            else
                            {
                                jsons += ",{" + "'product_name':'" + seaFood.product_name + "','product_id':'" + seaFood.product_id + "','product_price':" + seaFood.product_price.ToString() + ",'product_picture_url':'" + seaFood.product_picture_url + "','product_intro':'" + seaFood.product_intro + "', 'product_number':" + product_number.ToString() + "}";
                            }
                        }
                        else
                        {
                            seaFood.product_name = reader.GetString(0);
                            seaFood.product_id = reader.GetString(1);
                            seaFood.product_price = reader.GetFloat(2);
                            seaFood.product_picture_url = reader.GetString(3);
                            seaFood.product_intro = reader.GetString(4);
                            if (i == 0)
                            {
                                jsons += "{" + "'product_name':'" + seaFood.product_name + "','product_id':'" + seaFood.product_id + "','product_price':" + seaFood.product_price.ToString() + ",'product_picture_url':'" + seaFood.product_picture_url + "','product_intro':'" + seaFood.product_intro + "'}";
                                i++;
                            }
                            else
                            {
                                jsons += ",{" + "'product_name':'" + seaFood.product_name + "','product_id':'" + seaFood.product_id + "','product_price':" + seaFood.product_price.ToString() + ",'product_picture_url':'" + seaFood.product_picture_url + "','product_intro':'" + seaFood.product_intro + "'}";
                            }
                        }
                        list_sea_product.Add(seaFood);
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
            string pw1 = "'open_id':'" + openId + "'", pw2 = "'group_number':" + number.ToString() + "", pw3 = "'bought':" + x.ToString();
            string pw4 = "'isrc':'" + isrc + "', 'rc_id':'" + address_rc_id + "'", pw5 = "'client_name':'" + client_name + "'";
            string pw6 = "";
            string pw7 = ", 'isreg':'" + ISREG + "'";
            if (flag)
            {
                pw6 = ", 'order_number':'" + order_number + "'";
            }
            jsons = "{ 'product' : [" + jsons + "]," + pw1 + "," + pw2 + ", " + pw3 + "," + pw4 + ", " + pw5 + pw6 + pw7 + "}";
            return (JObject)JsonConvert.DeserializeObject(jsons.ToString());
        }

        // POST: api/Get_Product_1
        public string Post([FromBody] JObject jobject)
        {
            return jobject.ToString();
        }

        // PUT: api/Get_Product_1/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Get_Product_1/5
        public void Delete(int id)
        {
        }
    }
}

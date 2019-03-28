using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Http;
using System.Timers;
using group_buy.MYSQL;

/// <summary>
/// 进行提交开团
//// 2018.11.21
/// @author:hanrubing, liyongshu
/// </summary>

namespace group_buy.Controllers
{
    public class StartOfGroupController : ApiController
    {
        // GET: api/StartOfGroup
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/StartOfGroup/5
        public string Get(int id)
        {
            return "value";
        }

        /*public Timer timer;


        private void Time(object source, ElapsedEventArgs e)//定时器关团任务
        {
            //if ( !ISOPEN.ISOPEN_SURE() )
            //{
                ISOPEN.ISOPEN_CLOSE();//规定时间后结束团购
            //}
            //ISOPEN.ISOPEN_SURE_0();
        }*/

        // POST: api/StartOfGroup
        public string Post()
        {
            HttpContext.Current.Response.AddHeader("Access-Control-Allow-Credentials", "true");
            HttpContext.Current.Response.AppendHeader("Access-Control-Allow-Origin", "http://xmhome.xyz");//实现跨域
            if ( ISOPEN.ISOPEN_SELECT() )
            {//当前处于开团时期，不可重复开团
                return "-1";
            }
            int number = NUMBER.NUMBER_SELECT();
            NUMBER.NUMBER_UPDATE(number);//团购此时自增
            /*if ( HttpContext.Current.Request.Form["start_date"] == null )
            {
                return "-2";
            }
            if (HttpContext.Current.Request.Form["start_time"] == null)
            {
                return "-2";
            }*/
            if (HttpContext.Current.Request.Form["end_date"] == null)
            {
                return "-2";
            }
            if (HttpContext.Current.Request.Form["end_time"] == null)
            {
                return "-2";
            }
            if (HttpContext.Current.Request.Form["seafood"] == null)
            {
                return "-2";
            }
            //string start_date = HttpContext.Current.Request.Form["start_date"].ToString().Trim();
            //string start_time = HttpContext.Current.Request.Form["start_time"].ToString().Trim();
            string end_date = HttpContext.Current.Request.Form["end_date"].ToString().Trim();
            string end_time = HttpContext.Current.Request.Form["end_time"].ToString().Trim();
            string seafoodID = HttpContext.Current.Request.Form["seafood"].ToString().Trim();
            string[] IDS = Regex.Split(seafoodID, ",", RegexOptions.IgnoreCase);
            foreach ( string s in IDS )
            {
                if ( s == null || s == "")
                {
                    return "-2";
                }
            }
            //start_time.Replace("%3a", ":");
            end_time.Replace("%3a", ":");
            //DateTime START = Convert.ToDateTime(start_date + " " + start_time + ":00");
            DateTime END = Convert.ToDateTime(end_date + " " + end_time + ":00");
            DateTime today = DateTime.Now;
            //TimeSpan span_s_e = (TimeSpan)(END - START);//START应早于END
            TimeSpan span_t_e = (TimeSpan)(END - today);//END应晚于today
            //TimeSpan span_t_s = (TimeSpan)(START - today);//START应晚于today
            /*if ( span_s_e.TotalMilliseconds <= 0 || span_t_e.TotalMilliseconds <= 0 || span_t_s.TotalMilliseconds < 0)
            {
                return "-3";
            }*/
            if ( span_t_e.TotalMilliseconds <= 0 )
            {
                return "-3";
            }
            MySqlConnection mysql = MySQL.getMySqlConnection();
            MySqlCommand mySqlCommand = null;
            bool flag = false;
            int group_number = NUMBER.NUMBER_SELECT();
            foreach (string ID in IDS)
            {
                string product_name = "";
                string product_id = "";
                float product_price = 0f;
                string product_intro = "";
                string product_picture = "";
                MySqlConnection get = MySQL.getMySqlConnection();
                MySqlCommand getCommand = MySQL.getSqlCommand("SELECT * FROM p_table_1 WHERE product_id = " + ID, get);
                get.Open();//开启数据库连接
                MySqlDataReader reader = getCommand.ExecuteReader();//执行sql语句
                try
                {
                    while (reader.Read())
                    {
                        if (reader.HasRows)//用seaFood封装读取会的数据并添加到list_sea_product
                        {
                            product_name = reader.GetString(0);
                            product_id = reader.GetString(1);
                            product_price = reader.GetFloat(2);
                            product_picture = reader.GetString(3);
                            product_intro = reader.GetString(4);
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
                mySqlCommand = MySQL.getSqlCommand(("INSERT INTO group_ptable VALUES('" +
                    product_name + "','" + product_id + "'," + product_price.ToString() + ",'" + product_picture +
                    "','" + product_intro + "'," +  group_number.ToString() +
                    ")"), mysql);
                mysql.Open();
                if (mySqlCommand.ExecuteNonQuery() > 0) { }
                else
                {
                    flag = true;
                }
                mysql.Close();
            }
            if (flag)
            {
                NUMBER.NUMBER_UPDATE_2(NUMBER.NUMBER_SELECT());
                return "0";
            }
            else
            {
                ISOPEN.ISOPEN_OPEN();//团购进行时
                //START = Convert.ToDateTime(start_date + " " + start_time + ":00");
                today = DateTime.Now;
                END = Convert.ToDateTime(end_date + " " + end_time + ":00");
                TimeSpan span = (TimeSpan)(END - today);
                TIME.EASY(span.TotalMilliseconds);
                /*timer = new Timer();
                timer.Enabled = true;
                if (span.TotalMilliseconds > 0)
                {
                    timer.Interval = span.TotalMilliseconds;
                }
                else
                {
                    timer.Interval = 1;
                }
                timer.Start();
                timer.Elapsed += new System.Timers.ElapsedEventHandler(Time);*/
                return "1";
            }
            
        }

        // PUT: api/StartOfGroup/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/StartOfGroup/5
        public void Delete(int id)
        {
        }
    }

    public class TIME
    {
        public static Timer timer = null;

        private static void Time(object source, ElapsedEventArgs e)//定时器关团任务
        {
            ISOPEN.ISOPEN_CLOSE();//规定时间后结束团购
            timer.Stop();
        }

        public static void EASY(double span)
        {
            timer = new Timer();
            timer.Enabled = true;
            if (span > 0)
            {
                timer.Interval = span;
            }
            else
            {
                timer.Interval = 1;
            }
            timer.Start();
            timer.Elapsed += new System.Timers.ElapsedEventHandler(Time);
        }

        public static void CLOSE()
        {
            if ( timer != null)
            {
                timer.Stop();
            }
        }
        
    }
}

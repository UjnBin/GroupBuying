using group_buy.Controllers;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Http;

/// <summary>
/// 对groupbuying数据库-isopen表操作的封装
/// 2018.11.23
/// @author:hanrubing, liyongshu
/// </summary>

namespace group_buy.MYSQL
{
    public class ISOPEN
    {

        public static bool ISOPEN_SURE()
        {
            /**
             * return true 当前定时任务不能正常执行
             * return false 当前定时任务能正常执行
             */
            MySqlConnection isopen_select = MySQL.getMySqlConnection();
            MySqlCommand isopen_selectMySqlCommandss = MySQL.getSqlCommand("SELECT sure FROM isopen", isopen_select);
            isopen_select.Open();//开启数据库连接
            MySqlDataReader isopen_selectReader = isopen_selectMySqlCommandss.ExecuteReader();//执行sql语句
            try
            {
                while (isopen_selectReader.Read())
                {
                    if (isopen_selectReader.HasRows)
                    {
                        int open = isopen_selectReader.GetInt16(0);
                        if (open == 0)
                        {
                            return false;
                        }
                    }
                }
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            finally
            {
                isopen_select.Close();
            }
            return true;
        }

        public static bool ISOPEN_SURE_0()
        {
            /**
             * sure为1表示关团任务不能开启
             * return true 成功
             * return false 失败
             */
            MySqlConnection isopen_open = MySQL.getMySqlConnection();
            MySqlCommand isopen_openMySqlCommands = MySQL.getSqlCommand("UPDATE isopen SET sure = 0", isopen_open);
            isopen_open.Open();
            if (isopen_openMySqlCommands.ExecuteNonQuery() > 0)
            {
                isopen_open.Close();
                return true;//修改成功
            }
            isopen_open.Close();
            return false;//修改失败
        }

        public static bool ISOPEN_SURE_1()
        {
            /**
             * sure为1表示关团任务不能开启
             * return true 成功
             * return false 失败
             */
            if ( ISOPEN_SURE() )
            {
                return false;
            }
            MySqlConnection isopen_open = MySQL.getMySqlConnection();
            MySqlCommand isopen_openMySqlCommands = MySQL.getSqlCommand("UPDATE isopen SET sure = 1", isopen_open);
            isopen_open.Open();
            if (isopen_openMySqlCommands.ExecuteNonQuery() > 0)
            {
                isopen_open.Close();
                return true;//修改成功
            }
            isopen_open.Close();
            return false;//修改失败
        }

        public static bool ISOPEN_SELECT()
        {
            /**
             * return true 当前表是可插入的
             * return false 当前表是不可插入的
             */
            MySqlConnection isopen_select = MySQL.getMySqlConnection();
            MySqlCommand isopen_selectMySqlCommandss = MySQL.getSqlCommand("SELECT open FROM isopen", isopen_select);
            isopen_select.Open();//开启数据库连接
            MySqlDataReader isopen_selectReader = isopen_selectMySqlCommandss.ExecuteReader();//执行sql语句
            try
            {
                while (isopen_selectReader.Read())
                {
                    if (isopen_selectReader.HasRows)
                    {
                        int open = isopen_selectReader.GetInt16(0);
                        if (open == 0)
                        {
                            return false;
                        }
                    }
                }
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            finally
            {
                isopen_select.Close();
            }
            return true;
        }

        public static bool ISOPEN_OPEN()
        {
            /**
             * 修改isopen - open为可插入
             * return true 成功
             * return false 失败
             */
            MySqlConnection isopen_open = MySQL.getMySqlConnection();
            MySqlCommand isopen_openMySqlCommands = MySQL.getSqlCommand("UPDATE isopen SET open = 1", isopen_open);
            isopen_open.Open();
            if (isopen_openMySqlCommands.ExecuteNonQuery() > 0)
            {
                isopen_open.Close();
                return true;//修改成功
            }
            isopen_open.Close();
            return false;//修改失败
        }

        public static bool ISOPEN_CLOSE()
        {
            /**
             * 修改isopen - open为不可插入
             * return true 成功
             * return false 失败
             */
            if ( !ISOPEN_SELECT())
            {
                return false;
            }
            MySqlConnection isopen_close = MySQL.getMySqlConnection();
            MySqlCommand isopen_closeMySqlCommands = MySQL.getSqlCommand("UPDATE isopen SET open = 0", isopen_close);
            isopen_close.Open();
            if (isopen_closeMySqlCommands.ExecuteNonQuery() > 0)
            {
                isopen_close.Close();
                return true;
            }
            isopen_close.Close();
            return false;
        }
    }
}
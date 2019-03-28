using group_buy.Controllers;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Http;

namespace group_buy.MYSQL
{
    public class USER
    {
        public static bool USER_SELECT(string open_id)
        {
            MySqlConnection add_user = MySQL.getMySqlConnection();
            MySqlCommand add_rcMySqlCommands = MySQL.getSqlCommand("SELECT count(*) FROM user WHERE user_id = '" + open_id + "'", add_user);
            add_user.Open();
            MySqlDataReader reg_c_selectReader = add_rcMySqlCommands.ExecuteReader();//执行sql语句
            try
            {
                while (reg_c_selectReader.Read())
                {
                    if (reg_c_selectReader.HasRows)
                    {
                        int open = reg_c_selectReader.GetInt16(0);
                        if (open == 0)
                        {//不存在该用户信息
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
                add_user.Close();
            }
            return true;
        }
    }
}
using group_buy.Controllers;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Http;

/// <summary>
/// 对groupbuying数据库-reg_c表操作的封装
/// 2018.11.23
/// @author:hanrubing, liyongshu
/// </summary>

namespace group_buy.MYSQL
{
    public class REG_C
    {
        public static bool REG_C_SELECT(string openid)
        {
            MySqlConnection reg_c_select = MySQL.getMySqlConnection();
            MySqlCommand reg_c_selectMySqlCommandss = MySQL.getSqlCommand("SELECT count(*) FROM reg_c WHERE rc_id = '" + openid + "'", reg_c_select);
            reg_c_select.Open();//开启数据库连接
            MySqlDataReader reg_c_selectReader = reg_c_selectMySqlCommandss.ExecuteReader();//执行sql语句
            try
            {
                while (reg_c_selectReader.Read())
                {
                    if (reg_c_selectReader.HasRows)
                    {
                        int open = reg_c_selectReader.GetInt16(0);
                        if (open == 0)
                        {//不存在该团长信息
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
                reg_c_select.Close();
            }
            return true;
        } 
    }
}
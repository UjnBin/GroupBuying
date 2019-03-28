using group_buy.Controllers;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// 对groupbuying数据库-housingestate表操作的封装
/// 2018.11.23
/// @author:hanrubing, liyongshu
/// </summary>

namespace group_buy.MYSQL
{
    public class HE
    {

        public static bool HE_INSERT(HashSet<string> hs)
        {
            MySqlConnection mysql = MySQL.getMySqlConnection();
            string sql1 = "INSERT INTO housingestate VALUES ";
            string sql2 = "";
            int i = 0;
            foreach ( string s in hs)
            {
                if ( i == 0)
                {
                    sql2 += "('" + s + "')";
                    i += 1;
                }
                else
                {
                    sql2 += ",('" + s + "')";
                }
            }
            string sql = sql1 + sql2;
            MySqlCommand mySqlCommand = MySQL.getSqlCommand(sql, mysql);
            mysql.Open();
            try
            {
                if (mySqlCommand.ExecuteNonQuery() > 0)
                {
                    mysql.Close();
                    return true;
                }
            } catch (MySql.Data.MySqlClient.MySqlException)
            {

            }
            finally
            {
                mysql.Close();
            }
            return false;
        }

        public static void HE_SELECT()
        {

        }

    }
}
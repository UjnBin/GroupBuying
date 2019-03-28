using group_buy.Controllers;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Http;

/// <summary>
/// 对groupbuying数据库-number表操作的封装
/// 2018.11.23
/// @author:hanrubing, liyongshu
/// </summary>

namespace group_buy.MYSQL
{
    public class NUMBER
    {
        public static int NUMBER_SELECT()
        {
            /**
             * @return 返回当前团购期数
             */
            int number = 0;
            MySqlConnection number_select = MySQL.getMySqlConnection();
            MySqlCommand number_selectMySqlCommand1 = MySQL.getSqlCommand("SELECT * FROM number", number_select);
            number_select.Open();//开启数据库连接
            MySqlDataReader reader = number_selectMySqlCommand1.ExecuteReader();//执行sql语句
            try
            {
                while (reader.Read())
                {
                    if (reader.HasRows)//用seaFood封装读取会的数据并添加到list_sea_product
                    {
                        number = reader.GetInt16(0);
                    }
                }
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            finally
            {
                number_select.Close();
            }
            if ( number >= 0 )
            {
                return number;
            }
            else
            {
                return 0;
            }
        }

        public static bool NUMBER_UPDATE(int number)
        {
            /**
             * @return 
             * true - 成功
             * false - 失败
             */
            if ( number < 0 )
            {
                number = 1;
            }
            else
            {
                number++;
            }
            MySqlConnection number_update = MySQL.getMySqlConnection();
            MySqlCommand number_updateMySqlCommands = MySQL.getSqlCommand("UPDATE number SET group_number = " + number.ToString(), number_update);
            number_update.Open();
            if (number_updateMySqlCommands.ExecuteNonQuery() > 0)
            {
                number_update.Close();
                return true;
            }
            number_update.Close();
            return false;
        }

        public static bool NUMBER_UPDATE_2(int number)
        {
            /**
             * number--
             * @return 
             * true - 成功
             * false - 失败
             */
            if (number < 1)
            {
                number = 0;
            }
            else
            {
                number--;
            }
            MySqlConnection number_update = MySQL.getMySqlConnection();
            MySqlCommand number_updateMySqlCommands = MySQL.getSqlCommand("UPDATE number SET group_number = " + number.ToString(), number_update);
            number_update.Open();
            if (number_updateMySqlCommands.ExecuteNonQuery() > 0)
            {
                number_update.Close();
                return true;
            }
            number_update.Close();
            return false;
        }
    }
}
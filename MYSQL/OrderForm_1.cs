using group_buy.Controllers;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Http;

/// <summary>
/// 对groupbuying数据库-order_form_1表操作的封装
/// 2018.11.23
/// @author:hanrubing, liyongshu
/// </summary>
/// 
namespace group_buy.MYSQL
{
    public class OrderForm_1
    {

        public static bool OrderForm_1_Existence(string client_id , int group_number)
        {
            /**
             * client_id用户id，group_number团购期数
             * 本期购物清单表中是否存在对应购物信息
             * @return true 存在用户清单信息
             * @return false 不存在用户清单信息
             */
            int number = 0;
            MySqlConnection order_from_1_existence = MySQL.getMySqlConnection();
            MySqlCommand order_from_1_existenceMySqlCommand = MySQL.getSqlCommand("SELECT count(*) FROM orderform_1 WHERE client_id = '" + client_id + "' and group_number = " + group_number.ToString() + " and p_single = 0", order_from_1_existence);
            order_from_1_existence.Open();//开启数据库连接
            MySqlDataReader reader = order_from_1_existenceMySqlCommand.ExecuteReader();//执行sql语句
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
                order_from_1_existence.Close();
            }
            if ( number > 0 )
            {
                return true;
            }
            return false;
        }

    }
}
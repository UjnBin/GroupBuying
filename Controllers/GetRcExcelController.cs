using group_buy.MYSQL;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace group_buy.Controllers
{
    public class GetRcExcelController : ApiController
    {
        // GET: api/GetRcExcel
        public string Get(string rc_id)
        {
            HttpContext.Current.Response.AddHeader("Access-Control-Allow-Credentials", "true");
            HttpContext.Current.Response.AppendHeader("Access-Control-Allow-Origin", "http://xmhome.xyz");//实现跨域
            if(!REG_C.REG_C_SELECT(rc_id))
            {
                return "0";
            }
            int number = NUMBER.NUMBER_SELECT();
            string FileName = rc_id + "团购" + number.ToString() + "期.xls";
            System.Data.DataTable table = new System.Data.DataTable();
            MySqlConnection mysql = MySQL.getMySqlConnection();
            MySqlCommand mySqlCommand = MySQL.getSqlCommand("SELECT * FROM orderform_1 WHERE rc_id = '" + rc_id + "'", mysql);
            mysql.Open();//开启数据库连接
            MySqlDataReader reader = mySqlCommand.ExecuteReader();//执行sql语句
            table.Load(reader);
            reader.Close();
            mysql.Close();
            GetEXCELS.getexcel(table, FileName);
            return "http://xmhome.xyz/GROUP_BUY_PRODUCT/EXCEL/" + FileName;
        }

        // GET: api/GetRcExcel/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/GetRcExcel
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/GetRcExcel/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/GetRcExcel/5
        public void Delete(int id)
        {
        }
    }

    public class GetEXCELS
    {
        public static void getexcel(System.Data.DataTable dt, string filename)
        {
            NPOI.HSSF.UserModel.HSSFWorkbook book = new NPOI.HSSF.UserModel.HSSFWorkbook();
            NPOI.SS.UserModel.ISheet sheet = book.CreateSheet("test_01");
            NPOI.SS.UserModel.IRow row = sheet.CreateRow(0);
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                row.CreateCell(i).SetCellValue(dt.Columns[i].ColumnName);
            }

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                NPOI.SS.UserModel.IRow row2 = sheet.CreateRow(i + 1);
                for (int j = 0; j < dt.Columns.Count; j++)
                    row2.CreateCell(j).SetCellValue(dt.Rows[i][j].ToString());
            }

            FileStream ms = new FileStream(@"C:\inetpub\wwwroot\GROUP_BUY_PRODUCT\EXCEL\" + filename, FileMode.Create);
            book.Write(ms);
            ms.Close();
            ms.Dispose();
        }
    }

}

using MySql.Data.MySqlClient;
using System.Data;
using System.Web.Http;
using Microsoft.Office.Interop;
using System.Runtime.InteropServices;
using System;
using group_buy.MYSQL;
using System.IO;
using System.Web;

/// <summary>
/// 导出EXCEL表格
/// 2018.11.25
/// @author:hanrubing, liyongshu
/// </summary>

namespace group_buy.Controllers
{
    public class GetExcelController : ApiController
    {
        // GET: api/Excel
        public string Get()
        {
            HttpContext.Current.Response.AddHeader("Access-Control-Allow-Credentials", "true");
            HttpContext.Current.Response.AppendHeader("Access-Control-Allow-Origin", "http://xmhome.xyz");//实现跨域
            int number = NUMBER.NUMBER_SELECT();
            string FileName = "团购" + number.ToString() + "期.xls";
            System.Data.DataTable table = new System.Data.DataTable();
            MySqlConnection mysql = MySQL.getMySqlConnection();
            MySqlCommand mySqlCommand = MySQL.getSqlCommand("SELECT * FROM orderform_1", mysql);
            mysql.Open();//开启数据库连接
            MySqlDataReader reader = mySqlCommand.ExecuteReader();//执行sql语句
            table.Load(reader);
            reader.Close();
            mysql.Close();
            GetEXCEL.getexcel(table,FileName);
            return "http://47.93.220.57/GROUP_BUY_PRODUCT/EXCEL/" + FileName;
        }

        // GET: api/Excel/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Excel
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Excel/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Excel/5
        public void Delete(int id)
        {
        }
    }

    public class GetEXCEL
    {
        public static void getexcel(DataTable dt, string filename)
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

            //写入到客户端  
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            book.Write(ms);
            HttpContext.Current.Response.AddHeader("Content-Disposition", string.Format("attachment; filename=" + filename));
            HttpContext.Current.Response.BinaryWrite(ms.ToArray());
            book = null;
            ms.Close();
            ms.Dispose();
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvailablePC
{

    namespace Database2
    {
        public static class Util
        {

            public static DataSet OpenExcel(string file_path)
            {
                //TODO 处理窗口直接关闭的情况
                //TODO 读取数据后的处理
                ArrayList list = new ArrayList(); 
                string strConn = 
                    "Provider=Microsoft.Ace.OLEDB.12.0;"+
                    $"Data Source={file_path};"+
                    "Extended Properties='Excel 8.0'";
                DataSet ds = new DataSet();
                OleDbConnection conn = new OleDbConnection(strConn);
                conn.Open();
                DataTable sheetNames = conn.GetOleDbSchemaTable
                (OleDbSchemaGuid.Tables, new object[] { null, null, null, "TABLE" });
                conn.Close();
                foreach (DataRow dr in sheetNames.Rows)
                {
                    list.Add(dr[2]);
                }

                OleDbDataAdapter oada = new OleDbDataAdapter($"select * from [{list[0]}]", strConn);
                oada.Fill(ds);
                return ds;
            }


        }
    }
}

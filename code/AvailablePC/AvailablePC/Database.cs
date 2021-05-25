using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using MySql;
using MySqlX;
using MySql.Data;
using SqlSugar;

namespace AvailablePC
{
    namespace Database
    {
        
        public class Util
        {
            public SqlSugarClient db;
            private static Util _util = null;
            private Util()
            {

                var i = new ConnectionConfig()
                {
                    ConnectionString = @"Data Source = 127.0.0.1; port = 3306; Initial Catalog = Available; uid = root; pwd = admin",//必填, 数据库连接字符串
                    DbType = DbType.MySql,         //必填, 数据库类型
                    IsAutoCloseConnection = true,      //默认false, 时候知道关闭数据库连接, 设置为true无需使用using或者Close操作
                    InitKeyType = InitKeyType.Attribute    //默认SystemTable, 字段信息读取, 如：该属性是不是主键，是不是标识列等等信息
                };

                db  = new SqlSugarClient(i);
            }
           
            public static Util GetInstance()
            {
                if (_util == null)
                    _util = new Util();
                return _util;
            }

            public void Add<T>(T obj)where T:class,new()
            {
                db.Insertable(obj).ExecuteCommand();
            }

        }


        [SugarTable("User"),Obsolete]
        public class User
        {
            [SugarColumn(ColumnName = "id", IsPrimaryKey = true, IsIdentity = true)]
            public int ID { get; set; }

            [SugarColumn(ColumnName = "Name", IsNullable = false)]
            public string Name { get; set; }
        }


    }
}

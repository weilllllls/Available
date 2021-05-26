using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Security.Permissions;

using System.Threading.Tasks;
using System.Windows.Forms;
using AvailablePC.EntityAttribute;

using System.Text.Json;
using System.Text.Json.Serialization;

using SqlSugar;
using System.Text;
using System.IO;
using System.Text.Unicode;

namespace AvailablePC
{

    namespace EntityAttribute
    {
        [AttributeUsage(AttributeTargets.Property)]
        class ReflectAttribute : Attribute
        {
        }
    }


    namespace Entity
    {
        public abstract class EntityClass
        {
            [Obsolete]
            public virtual string ToJson2()
            {
                string str = "";
                str += "{\n";
                foreach (var i in GetType().GetProperties())
                {
                    bool hasFlag = false;
                    foreach (var j in i.GetCustomAttributes(false))
                    {
                        if (j is ReflectAttribute)
                        {
                            hasFlag = true;
                            break;
                        }
                    }

                    if (hasFlag)
                    {
                        str += $"\"{i.Name}\":\"{i.GetValue(this)}\",\n";
                    }
                }
                str = str.TrimEnd(',', '\n');
                //str = str.Trim(',',' ');
                str += "\n}";
                return str;
            }


            public virtual string ToJson()
            {
                var options = new JsonSerializerOptions { 
                    Encoder = System.Text.Encodings.Web.JavaScriptEncoder.Create(UnicodeRanges.All),
                    WriteIndented = true };

                //byte[] utf8 = JsonSerializer.SerializeToUtf8Bytes(this, GetType(), options);
                string str = JsonSerializer.Serialize(this, GetType(), options);
                //utf8 = JsonSerializer.SerializeToUtf8Bytes()
                //return Encoding.UTF8.GetString(utf8);
                return str;
            }
            public static EntityClass FromJson(string json, Type type)
            {
                var obj = JsonSerializer.Deserialize(json, type);
                return obj as EntityClass;
            }
        }
        /// <summary>
        /// 请求类型
        /// </summary>
        public enum RequestType
        {
            Update, Query
        }

        public enum RequestState
        {
            Waiting,Finish,Denied
        }

        #region 旧代码

        [SugarTable("CourseInfo")]
        public sealed class CourseInfo : EntityClass
        {
            /// <summary>
            /// 主键
            /// </summary>
            [SugarColumn(IsPrimaryKey = true, IsIdentity = true), Reflect]
            public int index { get; set; }
            /// <summary>
            /// 授课教师号
            /// </summary>
            [SugarColumn, Reflect]
            public string teacher_id { get; set; }
            /// <summary>
            /// 课程号
            /// </summary>
            [SugarColumn, Reflect]
            public string course_number { get; set; }
            /// <summary>
            /// 课序号
            /// </summary>
            [SugarColumn, Reflect]
            public int course_index { get; set; }
            /// <summary>
            /// 考场
            /// </summary>
            [SugarColumn, Reflect]
            public string test_room { get; set; }
            /// <summary>
            /// 考试人数
            /// </summary>
            [SugarColumn, Reflect]
            public int test_number { get; set; }
            /// <summary>
            /// 考试时间
            /// </summary>
            [SugarColumn, Reflect]
            public DateTime test_time { get; set; }

            public static CourseInfo FromJson(string json)
            {
                var obj = JsonSerializer.Deserialize<CourseInfo>(json);
                return obj;
            }

        }

        [SugarTable("TestInfo")]
        public sealed class TestInfo : EntityClass
        {
            [SugarColumn(IsPrimaryKey = true), Reflect]
            ///<summary>
            ///主键
            ///</summary>
            public int index { get; set; }

            /// <summary>
            /// 授课教师号
            /// </summary>
            [SugarColumn, Reflect]
            public string teacher_id { get; set; }
            /// <summary>
            /// 考生学号
            /// </summary>
            [SugarColumn, Reflect]
            public string student_id { get; set; }
            /// <summary>
            /// 课程号
            /// </summary>
            [SugarColumn, Reflect]
            public string course_number { get; set; }
            /// <summary>
            /// 课序号
            /// </summary>
            [SugarColumn, Reflect]
            public int course_index { get; set; }
            /// <summary>
            /// 试卷图片存放的路径
            /// </summary>
            [SugarColumn, Reflect]
            public string uri { get; set; }

            public static TestInfo FromJson(string json)
            {
                var obj = JsonSerializer.Deserialize<TestInfo>(json);
                return obj;
            }
        }

        [SugarTable("RequestInfo")]
        public sealed class RequestInfo : EntityClass
        {
            /// <summary>
            /// 教师号
            /// </summary>
            [SugarColumn, Reflect]
            public string teacher_id { get; set; }
            /// <summary>
            /// 考生学号
            /// </summary>
            [SugarColumn, Reflect]
            public string student_id { get; set; }
            /// <summary>
            /// 信息概述
            /// </summary>
            [SugarColumn, Reflect]
            public string summary { get; set; }
            /// <summary>
            /// 课程号
            /// </summary>
            [SugarColumn, Reflect]
            public string course_number { get; set; }
            /// <summary>
            /// 课序号
            /// </summary>
            [SugarColumn, Reflect]
            public int course_index { get; set; }
            /// <summary>
            /// 请求类型
            /// </summary>
            [SugarColumn]
            public RequestType request_type { get; set; }

            public static RequestInfo FromJson(string json)
            {
                var obj = JsonSerializer.Deserialize<RequestInfo>(json);
                return obj;
            }

            public override string ToString()
            {
                string str =
                    $"教师号:{teacher_id}," +
                    $"学生学号:{student_id}," +
                    $"课程号:{course_number}," +
                    $"课序号:{course_index}," +
                    $"请求类型:{request_type}," +
                    $"请求内容:{summary}\n";
                return str;
            }
        }

        [Obsolete]
        public static class JsonToEntity
        {
            public static EntityClass FromJson(string json)
            {
                throw new NotImplementedException();
                //return null;
            }

            public static string ToJson(EntityClass entity)
            {
                string str = "";
                str += "{\n";
                foreach (var i in entity.GetType().GetProperties())
                {
                    bool hasFlag = false;
                    foreach (var j in i.GetCustomAttributes(false))
                    {
                        if (j is ReflectAttribute)
                        {
                            hasFlag = true;
                            break;
                        }
                    }

                    if (hasFlag)
                    {
                        str += $"\"{i.Name}\":\"{i.GetValue(entity)}\",\n";
                    }
                }
                str = str.TrimEnd(',', '\n');
                //str = str.Trim(',',' ');
                str += "\n}";
                return str;
            }
        }

        #endregion


        public struct TestRoom
        {
            public string ClassRoom;
            public string Building;
            public override string ToString()
            {
                return Building + ClassRoom;
            }
        }

        public enum TestType
        {
            Normal, Supplementary
        }

        [SugarTable("Test")]
        public sealed class Test : EntityClass
        {
            [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
            public int Index { get; set; }
            [SugarColumn]
            public string Course_Number { get; set; }
            [SugarColumn]
            public int Course_Index { get; set; }

            [SugarColumn]
            public string Student_Number { get; set; }
            [SugarColumn]
            public string Url_Up { get; set; }

            [SugarColumn]
            public string Url_Down { get; set; }
            //[SugarColumn(IsNullable =true,IsIgnore =true)]
            [SugarColumn(IsIgnore =true),JsonIgnore]
            public TestRoom Test_Room 
            {

                get => new TestRoom
                {
                    Building = Test_Room_Building,
                    ClassRoom = Test_Class_Room
                };

                set
                {
                    Test_Class_Room = value.ClassRoom;
                    Test_Room_Building = value.Building;
                }
            }


            [SugarColumn(IsNullable = true)]
            public string Test_Class_Room{ get; set; }

            [SugarColumn(IsNullable = true)]
            public string Test_Room_Building { get; set; }

            [SugarColumn]
            public DateTime Time { get; set; }
            [SugarColumn(IsNullable = true)]
            public decimal Score { get; set; }
            [SugarColumn(IsNullable = true)]
            public TestType Type { get; set; }

            public override string ToString()
            {
                return $"考试地点：{Test_Room};" +
                    $"考试类型：{Type};" +
                    $"考试时间：{Time};" +
                    $"考试成绩：{Score};" +
                    $"课程号：{Course_Number};" +
                    $"课序号：{Course_Index};" +
                    $"学号：{Student_Number};" +
                    $"URL_Up：{Url_Up};" +
                    $"URL_Down：{Url_Down}";

            }
        }


        [SugarTable("Request")]
        public sealed class Request : EntityClass
        {
            [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
            public int Index { get; set; }
            [SugarColumn]
            public string Teacher_ID { get; set; }
            [SugarColumn]
            public RequestState Request_State{get;set;}
            [SugarColumn]
            public string Course_Number { get; set; }
            [SugarColumn]
            public int Course_Index { get; set; }
            [SugarColumn]
            public string Student_Number { get; set; }
            [SugarColumn]
            public DateTime Time { get; set; }

            [SugarColumn]
            public string Reason { get; set; }

            [SugarColumn]
            public RequestType Request_Type { get; set; }


            [SugarColumn]
            public DateTime Request_Time { get; set; }
            [SugarColumn(IsNullable = true)]
            public TestType Type { get; set; }


            public string Summary()
            {

                switch (Request_Type)
                {
                    case RequestType.Query:
                        return $"学生:{Student_Number};课程号:{Course_Number},教师:{Teacher_ID},请求:查询;";
                    case RequestType.Update:
                        return $"学生:{Student_Number};课程号:{Course_Number},教师:{Teacher_ID},请求:更改;";
                    default:
                        throw new Exception();
                }
            }

            public override string ToString()
            {
                return ToJson();
            }
        }

        //用户账号
        //excel读取
        //根据字段？

        [SugarTable("User")]
        public sealed class User : EntityClass
        {

            [SugarColumn]
            public string Name { get; set; }
            [SugarColumn(IsPrimaryKey =true)]
            public string ID { get; set; }
            [SugarColumn]
            public string Password { get; set; }
        }


        public sealed class Reply:EntityClass
        {

            public string Teacher_ID { get; set; }
            public string Course_Number { get; set; }
            public string Student_Number { get; set; }
            public int Course_Index { get; set; }
            public DateTime Time { get; set; }

            public string Content { get; set; }

            public string File { get; set; }

            /*public Reply(string Course_Number, int Course_Index,
                string Student_Number, DateTime Time, FileStream File)
            {
                this.Course_Index = Course_Index;
                this.Course_Number = Course_Number;
                this.Student_Number = Student_Number;
                this.Time = Time;
                byte[] file = new byte[File.Length];
                File.Read(file, 0, Convert.ToInt32(File.Length));
                this.File = file;
            }*/

            public Reply() { }

            public Reply(string Course_Number, int Course_Index,
                string Student_Number, DateTime Time, string File)
            {
                /*if (!System.IO.File.Exists(File))
                    throw new Exception();*/
                //FileStream stream = System.IO.File.OpenRead(File);
                this.Course_Index = Course_Index;
                this.Course_Number = Course_Number;
                this.Student_Number = Student_Number;
                this.Time = Time;
                /*byte[] file = new byte[File.Length];
                stream.Read(file, 0, Convert.ToInt32(File.Length));*/
                //this.File = file;
                this.File = File;
            }

        }



    }
}

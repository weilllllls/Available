using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using AvailablePC.Database;
using AvailablePC.Entity;
using System.Collections.Specialized;
using AvailablePC;
using System.IO;
using System.Threading.Tasks;
using System.Threading;

namespace AvailableTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            Util u = Util.GetInstance();
            //u.Add(1, "ASD");
            u.db.DbMaintenance.CreateDatabase();
            u.db.CodeFirst.InitTables<Test, Request,AvailablePC.Entity.User>();



        }

        [TestMethod]
        public void TestMethod2()
        {
            AvailablePC.Database2.Util.OpenExcel("c:/Test/excel.xlsx");

        }

        [TestMethod]
        public void TestMethod3()
        {
            var i = new TestInfo()
            {
                teacher_id = "1234",
                course_index = 9,
                course_number = "12345",
                student_id = "2018",
                uri = @"C://中文路径/JPEG.jpeg"
            };
            var str = i.ToJson();

            var obj = EntityClass.FromJson(str,typeof(TestInfo));
            Console.WriteLine(obj);

        }


        [TestMethod]
        public void TestMethod4()
        {
            asyncTask();
            Thread.Sleep(60 * 1000);

        }

        public async void asyncTask()
        {
            FileManager manager = FileManager.GetInstance();
            var task = manager.LoadFile(new DirectoryInfo(@"C:\Users\PC\Desktop\Resources"));
            Console.WriteLine("正在拷贝文件，请稍后");

            await task;
            Console.WriteLine("拷贝文件完成！");
        }


        [TestMethod]
        public void TestMethod5()
        {
            Http http = new Http("http://localhost:8080/AvailableService_war_exploded/ServletReply");

            //Http http = new Http("http://47.98.202.193:8080/AvailableService/ServletReply");
            /*http.Post(http.uri, new AvailablePC.Entity.User
            {
                ID = "ASCII",
                Name = "周鸿",
                Password = "pswd"
            },new FileInfo(@"C:\Users\PC\Pictures\FLAMING MOUNTAIN.JPG"));*/
            //http.Post(http.uri, new AvailablePC.Entity.Reply("CourseNumber",0,"StudentNumber",new DateTime(),""));
            http.Post(http.uri, new Reply
            {
                Content = "Content",
                Student_Number = "studentNumber",
                Course_Index = 33,
                Course_Number = "CourseNumber...",
                File = "null",
                Teacher_ID = "TeacherID",
                Time = DateTime.Now
            });
            Thread.Sleep(1000 * 20);
        }


    }
}

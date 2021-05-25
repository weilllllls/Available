using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace AvailablePC
{
    static class Program
    {

        
        private static readonly string directory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"/Papers";
        public static readonly string INFO_FILE = directory+@"/receive_info.txt";
        //public static readonly Backgroud Backgroud = new Backgroud();

        public const string UploadPath = "http://47.98.202.193:8080/AvailableService/UploadInfo";
        public const string ReplyPath = "http://47.98.202.193:8080/AvailableService/ServletReply";


        public static FunctionWindow FunctionWindow;

        public static Http http;
        
        static void Init()
        {
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }
            if (!File.Exists(INFO_FILE))
            {
                var stream = File.Create(INFO_FILE);
                stream.Close();
            }
            http =  new Http("http://47.98.202.193:8080/AvailableService/ServletPC");
        }
        
        
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Init();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            FunctionWindow = new FunctionWindow();
            Application.Run(FunctionWindow);
            
            /*Backgroud.GetNetString += str => {
                Util.AppendFile(INFO_FILE, str);
            };*/
        }
    }
}

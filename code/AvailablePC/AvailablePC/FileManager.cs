using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AvailablePC
{
    public delegate void CopyProgressHandler(double progress);

    public class FileManager
    {
        public static readonly string pictureDir = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "/Papers/";

        private static FileManager _fileManager = null;

        //需要绑定该事件来显示处理进度
        public event CopyProgressHandler CopyProgress;

        private FileManager()
        {
            if (!Directory.Exists(pictureDir))
            {
                Directory.CreateDirectory(pictureDir);
            }
        }

        public static FileManager GetInstance()
        {
            if (_fileManager == null)
                _fileManager = new FileManager();
            return _fileManager;
        }

        //这个函数可以被等待
        public Task<List<string>> LoadFile(DirectoryInfo directory)
        {



            string[] type = { "*.jpg", "*.jpeg", "*.png", "*.pdf" };

            IEnumerable<FileInfo> list = new List<FileInfo>();
            foreach (var mode in type)
            {
                list = list.Union(directory.EnumerateFiles(mode));
            }

            Console.WriteLine("正在拷贝文件，请稍后...");
            int count = list.Count();
            int complete = 0;

            List<string> func()
            {

                List<string> string_list = new List<string>();
                foreach (var i in list)
                {
                    //TODO 这个拷贝地址可能需要修改为用户能够自由指定
                    string path = pictureDir + i.Name;
                    i.CopyTo(path);
                    ++complete;
                    CopyProgress?.Invoke((double)complete / count);
                    string_list.Add(path);
                }
                Console.WriteLine("拷贝完成！");
                return string_list;
            }


            Task<List<string>> task = new Task<List<string>>(func);
            task.Start();
            return task;
        }


    }

}


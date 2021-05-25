using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

using AvailablePC.Database2;

namespace AvailablePC
{
    static class Util
    {

        public static string LoadExcel(out DataSet dataSet)
        {
            var dialog = new OpenFileDialog
            {
                Filter = "Excel文件(*.xls;*.xlsx)|*.xls;*.xlsx"
            };
            label:
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                string file_name = dialog.FileName;
                if (!File.Exists(file_name))
                    goto label;

                dataSet = Database2.Util.OpenExcel(file_name);
                return file_name;
            }
            else goto label;
        }

        public static void JumpToWindow(Form from,Type to)
        {
            if (!to.IsSubclassOf(typeof(Form)))
                throw new Exception("跳转目标不是窗体");
            MethodBase method = new System.Diagnostics.StackTrace().GetFrame(1).GetMethod();



            if (!method.ReflectedType.IsSubclassOf(typeof(Form)))
                throw new Exception("跳转来源不是窗体，请使用this获取来源");
            
            new Thread(() => {
                if (to == typeof(FunctionWindow))
                {
                    Program.FunctionWindow.Show();
                    return;
                }
                Form des = (Form)Activator.CreateInstance(to);
                des.ShowDialog();
            }).Start();
            from.Close();
        }

/*        public static Action NewForm(Type form)
        {
            return new Action(() =>
            {
                new Thread(() =>
                {
                    if (!form.IsSubclassOf(typeof(Form)))
                        throw new Exception("目标不是窗体");
                    Form f = (Form)Activator.CreateInstance(typeof(Form));
                    f.Show(null);
                }).Start();
            });
        }*/

        public static void JumpToWindow(Form from, Type to, EventArgs args)
        {
            if (!to.IsSubclassOf(typeof(Form)))
                throw new Exception("跳转目标不是窗体");
            MethodBase method = new System.Diagnostics.StackTrace().GetFrame(1).GetMethod();

            if (!method.ReflectedType.IsSubclassOf(typeof(Form)))
                throw new Exception("跳转来源不是窗体，请使用this获取来源");

            new Thread(() => {
                Form des = (Form)Activator.CreateInstance(to,args);
                des.ShowDialog();
            }).Start();
            from.Close();
        }


        public static IEnumerable<string> ReadFile(string fileName)
        {
            if (!File.Exists(fileName))
                yield break;
            FileStream stream = File.OpenRead(fileName);
            StreamReader reader = new StreamReader(stream);
            string str = reader.ReadLine();
            while (str!=null)
            {
                yield return str;
                str = reader.ReadLine();
            }
            stream.Close();
            reader.Close();
        }


        public static Task<IEnumerable<string>> ChooseFiles(Form form)
        {
            IEnumerable<string> func()
            {
                var dialog = new OpenFileDialog
                {
                    Filter = "图片文件(*.png;*.jpg;*.jpeg)|*.pdf;*.jpg;*.jpeg",
                    Multiselect = true
                };
                label:
                dialog.ShowDialog(form);
                string[] file_name = dialog.FileNames;
                if (file_name.Length!=2)
                    goto label;

                foreach (var str in file_name)
                    yield return str;
            }
            return new Task<IEnumerable<string>>(func);
        }

        public static Task<string> ChooseFile(Form form)
        {

            string func()
            {
                var dialog = new OpenFileDialog
                {
                    Filter = "PDF文件(*.pdf)|*.pdf"
                };
                label:
                dialog.ShowDialog(form);
                string file_name = dialog.FileName;
                if (!File.Exists(file_name))
                    goto label;

                return file_name;
            }

            Task<string> task = new Task<string>(func);

            task.Start();

            return task;
        }

        public static void AppendFile(string file,string context)
        {
            if (!File.Exists(file))
                throw new Exception("文件不存在！");
            var stream = File.Open(file, FileMode.Append);
            StreamWriter writer = new StreamWriter(stream);
            writer.Write(context);
            writer.Flush();
            //writer.WriteLine(context);
            writer.Close();
            stream.Close();
        }

        public static void DeleteRow(string file,string context)
        {
            if (!File.Exists(file))
                throw new Exception("文件不存在！");
            //var stream = File.Open(file,FileMode.Open);
            //var reader = new StreamReader(stream);
            //string str;
            var lines = File.ReadAllLines(file);

            int row = -1;
            for(int i=0;i<lines.Length;++i)
            {
                if (lines[i] == context)
                {
                    //lines[i] = "";
                    row = i;
                    break;
                }
            }
            Stream stream = File.OpenWrite(file);
            stream.Seek(0, SeekOrigin.Begin);
            stream.SetLength(0);
            StreamWriter writer = new StreamWriter(stream);
            for (int i = 0; i < lines.Length; ++i) {
                if (i == row)
                    continue;
                writer.WriteLine(lines[i]);
            }
            //File.WriteAllLines(file, lines);
            writer.Flush();
            writer.Close();
            stream.Close();

        }

        public static void DeleteRow(string file,DateTime date)
        {
            if (!File.Exists(file))
                throw new Exception("文件不存在！");
            //var stream = File.Open(file,FileMode.Open);
            //var reader = new StreamReader(stream);
            //string str;
            var lines = File.ReadAllLines(file);

            int row = -1;
            for (int i = 0; i < lines.Length; ++i)
            {
                if (DateTime.Parse(lines[i]) == date)
                {
                    //lines[i] = "";
                    row = i;
                    break;
                }
            }
            Stream stream = File.OpenWrite(file);
            stream.Seek(0, SeekOrigin.Begin);
            stream.SetLength(0);
            StreamWriter writer = new StreamWriter(stream);
            for (int i = 0; i < lines.Length; ++i)
            {
                if (i == row)
                    continue;
                writer.WriteLine(lines[i]);
            }
            //File.WriteAllLines(file, lines);
            writer.Flush();
            writer.Close();
            stream.Close();
        }



    }
}

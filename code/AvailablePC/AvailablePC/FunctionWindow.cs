using AvailablePC.Entity;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AvailablePC
{
    public partial class FunctionWindow : Form
    {

        //NetStringHandler handler;
        readonly Action<Request> handler;
        public FunctionWindow()
        {

            FormClosing += (obj, e) =>
            {
                Program.http.HeartBeat.Abort();
            };
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;

            foreach (var i in Util.ReadFile(Program.INFO_FILE))
            {
                listBox1.Items.Add(i);
            }
            handler = request =>
            {
                var time = request.Request_Time;

                listBox1.Items.Add(time.ToString());
            };
            //Program.Backgroud.GetNetString += handler;
            Program.http.ReceiveMessage += handler;

        }

        public void RemoveItem(DateTime str)
        {
            for(int i = 0; i < listBox1.Items.Count; ++i)
            {
                if ((string)listBox1.Items[i] == str.ToString())
                {
                    listBox1.Items.RemoveAt(i);
                    break;
                }
            }
            Console.WriteLine("不存在的删除项");
        }

        private void button_Register_Click(object sender, EventArgs e)
        {
            Visible = false;
            new FormRegisterData().Show();
            //Util.JumpToWindow(this, typeof(FormRegisterData));
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            object content = listBox1.SelectedItem;
            if (content == null)
                return;

            var db = Database.Util.GetInstance().db;


            DateTime d = DateTime.Parse((string)content);
           /* Request request =
                db.Queryable<Request>()
               .First(iterator => iterator.Request_Time.Subtract(d)<=new TimeSpan(2000));*/
            var re =  db.Queryable<Request>();
            var r = re.ToList();

            foreach(var x in r)
            {
                Console.WriteLine(x.Request_Time == d);
            }
            var request =  r.First(i => d.Subtract(i.Request_Time)<=new TimeSpan(1000));
            //listBox1.SelectedItem = null;       
            
            //Util.JumpToWindow(this, typeof(PictureViewForm), new RequestEventArgs { Request = request });

            Visible = false;
            new PictureViewForm(new RequestEventArgs { Request = request }).Show();

        }

        private void listBox1_DoubleClick(object sender, EventArgs e)
        {

        }

        private void button_ManualManage_Click(object sender, EventArgs e)
        {

            Visible = false;

            //Util.JumpToWindow(this, typeof(FormManualManage));
            new FormManualManage().Show();
        }

        private async void button_PaperCollect_Click(object sender, EventArgs e)
        {
            Label:
            if (!(folderBrowserDialog1.ShowDialog() == DialogResult.OK))
                return;
            if (folderBrowserDialog1.SelectedPath == null)
                goto Label;
            //string mode=null;
            string path = folderBrowserDialog1.SelectedPath;
            DirectoryInfo directory = new DirectoryInfo(path);
            //var list = directory.EnumerateFiles(mode);
            FileManager manager = FileManager.GetInstance();
            ProgressForm progress = new ProgressForm();
            manager.CopyProgress += progress.progressBarChange;
            var res = manager.LoadFile(directory);
            progress.ShowDialog(this);




            var list = await res;
            MessageBox.Show("文件拷贝完成！");
            new FormInformRegister(new IEnumerableEventArgs<string>{ List =list}).Show();
            //progress.Close();
        }



        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            //Program.Backgroud.GetNetString -= handler;
            Program.http.ReceiveMessage -= handler;
        }
    }

}

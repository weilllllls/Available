using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

using AvailablePC.Entity;

namespace AvailablePC
{
    public partial class FormTestList : Form
    {
        readonly List<Test> list;

        public FormTestList(EventArgs args)
        {
            InitializeComponent();

            FormClosed += (obj, e) => {
                if (e.CloseReason == CloseReason.UserClosing)
                    new FunctionWindow().Show();
            };

            var arg = args as QueryEventArgs<Test>;
            list = arg.List;

            listView1.BeginUpdate();

            foreach(var i in list)
            {
                ListViewItem item = new ListViewItem();
                item.Text = i.Student_Number;
                item.SubItems.Add(i.Score.ToString());
                item.SubItems.Add(i.Test_Room.ToString());
                item.SubItems.Add(i.Time.ToString());
                item.SubItems.Add(i.Course_Number);
                item.SubItems.Add(i.Course_Index.ToString());
                item.SubItems.Add(i.Type.ToString());
                item.SubItems.Add(i.Url_Up+";"+i.Url_Down);
                listView1.Items.Add(item);
            }

            listView1.EndUpdate();
            

        }

        private void active(object sender, EventArgs e)
        {
            var db = Database.Util.GetInstance().db;
            /*var x = db.Queryable<Test>();
            foreach (var xx in x.ToList())
            {
                Console.WriteLine(xx.Student_Number == listView1.SelectedItems[0].Text);
                Console.WriteLine(xx.Time.ToString() == listView1.SelectedItems[0].SubItems[3].Text);
                Console.WriteLine(xx.Type.ToString() == listView1.SelectedItems[0].SubItems[6].Text);
                Console.WriteLine(xx.Url == listView1.SelectedItems[0].SubItems[7].Text);
                Console.WriteLine(xx.Course_Number == listView1.SelectedItems[0].SubItems[4].Text);
                Console.WriteLine(xx.Course_Index.ToString() == listView1.SelectedItems[0].SubItems[5].Text);
                Console.WriteLine();
            }*/

            //未知的查询错误？
            var t = db.Queryable<Test>().Where(
                obj =>
               /* obj.Student_Number == listView1.SelectedItems[0].Text &&
                obj.Time.ToString() == listView1.SelectedItems[0].SubItems[3].Text &&
                obj.Type.ToString() == listView1.SelectedItems[0].SubItems[6].Text &&*/
                obj.Url_Up+";"+obj.Url_Down == listView1.SelectedItems[0].SubItems[7].Text )//&&
                //obj.TestRoom.ToString() == listView1.SelectedItems[0].SubItems[1].Text &&
                /*obj.Course_Number == listView1.SelectedItems[0].SubItems[4].Text &&
                obj.Course_Index.ToString() == listView1.SelectedItems[0].SubItems[5].Text)*/.First();
            new Thread(() =>
            {
                new PictureView(t).ShowDialog(null);
            }).Start();
                
                Util.JumpToWindow(this, typeof(FormPaper), new TestEventArgs { Test = t, ScoreOnly = false });
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
           
        }
    }
}

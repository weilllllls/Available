using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using AvailablePC.Entity;

namespace AvailablePC
{
    public partial class FormUserList : Form
    {
        List<User> list;

        public FormUserList(EventArgs args)
        {
            InitializeComponent();

            FormClosed += (obj, e) => {
                if (e.CloseReason == CloseReason.UserClosing)
                    new FunctionWindow().Show();
            };

            var arg = args as QueryEventArgs<User>;
            list = arg.List;

            listView1.BeginUpdate();

            foreach(var i in list)
            {
                ListViewItem item = new ListViewItem();
                item.Text = i.ID;
                item.SubItems.Add(i.Name);
                item.SubItems.Add(i.Password);
                listView1.Items.Add(item);
            }

            listView1.EndUpdate();
            

        }

        private void active(object sender, EventArgs e)
        {
            var db = Database.Util.GetInstance().db;
            var t = db.Queryable<User>().First(
                obj =>obj.ID == listView1.SelectedItems[0].Text);
                Util.JumpToWindow(this, typeof(FormTeacher), new UserEventArgs { User = t });
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
           
        }
    }
}

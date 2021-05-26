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
    public partial class FormTeacher : Form
    {
        readonly User u;

        public FormTeacher(EventArgs args)
        {
            InitializeComponent();
            UserEventArgs arg = args as UserEventArgs;

            if(arg ==null)
            {

                //TODO

            }
            //TODO避免空属性出错，查询结果不存在，没有完成？
            u = arg.User;

            FormClosed += (obj, e) => {
                if (e.CloseReason == CloseReason.UserClosing)
                    new FunctionWindow().Show();
            };

            textBox_ID.Text = u.ID;
            textBox_Name.Text = u.Name;
            textBox_Password.Text = u.Password;

        }

        private void button_OK_Click(object sender, EventArgs e)
        {
            var db = Database.Util.GetInstance().db;

            u.Name = textBox_Name.Text;
            u.Password = textBox_Password.Text;

            db.Updateable(u).ExecuteCommand();
            MessageBox.Show("更新成功");
            Util.JumpToWindow(this, typeof(FunctionWindow));
        }
    }
}

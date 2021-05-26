using AvailablePC.Entity;
using Microsoft.Office.Interop.Excel;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AvailablePC
{
    public partial class FormQueryTeacher : Form
    {
        public FormQueryTeacher()
        {
            InitializeComponent();

            FormClosed += (obj,e) => {
                if(e.CloseReason ==  CloseReason.UserClosing)
                new FunctionWindow().Show(); };
        }

        private void button_OK_Click(object sender, EventArgs e)
        {
            bool hasName = false, hasID = false;
            var db = Database.Util.GetInstance().db;
            var res = db.Queryable<User>();

            if (textBox_Id.Text != null)
                hasID = true;
            if (textBox_Name.Text != null)
                hasName = true;

            ISugarQueryable<User> result;
            if(hasName)
            {
                if (hasID)
                {
                    result = 
                        from user in res 
                        where user.Name.Contains(textBox_Name.Text) 
                        && user.ID.Contains(textBox_Id.Text) select user;
                }
                else
                {
                    result =
                        from user in res
                        where user.Name.Contains(textBox_Name.Text)
                        select user;
                }
            }
            else
            {
                if (hasID)
                {
                    result = from user in res
                             where user.ID.Contains(textBox_Id.Text)
                             select user;
                }
                else
                    result = res;
            }

            Util.JumpToWindow(this, typeof(FormUserList), new QueryEventArgs<User> { List = result.ToList() });

           
        }

        private void button_New_Click(object sender, EventArgs e)
        {
            try
            {
                string psw = textBox_Id.Text;
                int lenth = psw.Length;
                psw = psw.Substring(lenth - 6);

                User u = new User
                {
                    ID = textBox_Id.Text,
                    Name = textBox_Name.Text,
                    Password = psw
                };

                var db = Database.Util.GetInstance().db;
                db.Insertable(u).ExecuteCommand();

                MessageBox.Show("新增数据成功，密码默认为工号后6位");

                Close();

                //Util.JumpToWindow(this, typeof(FunctionWindow));
            }
            catch(SqlException exception)
            {
                MessageBox.Show($"新增数据出错，请检查是否是工号重复");
                Console.WriteLine(exception.Message);
            }        
        }
    }
}

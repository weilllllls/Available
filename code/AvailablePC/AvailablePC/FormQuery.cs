using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using AvailablePC.Entity;
using SqlSugar;

namespace AvailablePC
{
    

    public partial class FormQuery : Form
    {
        public FormQuery()
        {
            InitializeComponent();

            FormClosed += (obj, e) => {
                if (e.CloseReason == CloseReason.UserClosing)
                    new FunctionWindow().Show();
            };
        }

        private void button_OK_Click(object sender, EventArgs e)
        {
            var db = Database.Util.GetInstance().db;
            //var result = db.Queryable<Test>();
            
            bool hasBuilding=false,
                hasClassRoom=false,
                hasStudentNumber=false,
                hasCourseIndex=false,
                hasCourseNumber=false,
                hasType=false;

            //ISugarQueryable<Test> res;

            if (textBox_building.Text != "")
                hasBuilding = true;
            if (textBox_courseIndex.Text != "")
                hasCourseIndex = true;
            if (textBox_courseNumber.Text != "")
                hasCourseNumber = true;
            if (textBox_roomNumber.Text != "")
                hasClassRoom = true;
            if (comboBox1.SelectedItem != null)
                hasType = true;
            if (textBox_StudentNumber.Text != "")
                hasStudentNumber = true;


            //拼凑SQL或者改用db.ado?
            string sql_part = " select * from Test where ";
            sql_part += $"`time` >=\"{dateTimePicker_Begin.Value.Date}\" and `time` <= \"{dateTimePicker_End.Value.Date}\" ";
            sql_part += $"and `Score` >= {numericUpDown_StudentScore_Min.Value} and `Score` <= {numericUpDown_StudentScore_Max.Value} ";
            if (hasBuilding)
                sql_part += $"and `Test_Room_Building` like '%{textBox_building.Text}%' ";
            if (hasClassRoom)
                sql_part += $"and `Test_Class_Room` like '%{textBox_roomNumber.Text}%' ";
            if (hasCourseIndex)
                sql_part += $"and `Course_Index` = {textBox_courseIndex.Text} ";
            if (hasCourseNumber)
                sql_part += $"and `Course_Number` like '%{textBox_courseNumber.Text}%' ";
            if (hasStudentNumber)
                sql_part += $"and `Student_Number` like '%{textBox_StudentNumber.Text}%' ";
            if (hasType)
                sql_part += $"and `Type` = {comboBox1.SelectedIndex} ";
            //查询结果的IEnumerable，需要被用来处理显示
            var list = db.SqlQueryable<Test>(sql_part);
            var l = list.ToList();
            Util.JumpToWindow(this, typeof(FormTestList),new QueryEventArgs<Test> {List = l});
        }
    }
}

using AvailablePC.Entity;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AvailablePC
{
    public partial class FormPaper : Form
    {
        readonly Test test;
        readonly TestEventArgs arg;
        public FormPaper(EventArgs args)
        {
            InitializeComponent();
            arg = args as TestEventArgs;
            test = arg.Test;

            //TODO 添加确定后返回

            FormClosed += (obj, e) => {
                if (e.CloseReason == CloseReason.UserClosing)
                    Program.FunctionWindow.Show();
            };

            if (arg != null)
            {
                textBox_building.Text = test.Test_Room_Building;
                textBox_courseIndex.Text = test.Course_Index.ToString();
                textBox_courseNumber.Text = test.Course_Number;
                textBox_roomNumber.Text = test.Test_Class_Room;
                textBox_StudentNumber.Text = test.Student_Number;
                textBox_StudentScore.Text = test.Score.ToString();
                comboBox1.SelectedIndex = test.Type.ObjToInt();
                dateTimePicker.Value = test.Time;


                if (arg.ScoreOnly == true)
                {
                    textBox_building.ReadOnly = true;
                    textBox_courseIndex.ReadOnly = true;
                    textBox_courseNumber.ReadOnly = true;
                    textBox_roomNumber.ReadOnly = true;
                    textBox_StudentNumber.ReadOnly = true;
                    comboBox1.Enabled = false;
                    dateTimePicker.Enabled = false;
                }

                if(arg.ScoreOnly == false)
                {
                    textBox_building.Text = test.Test_Room_Building;
                    textBox_courseIndex.Text = test.Course_Index.ToString();
                    textBox_courseNumber.Text = test.Course_Number;
                    textBox_roomNumber.Text = test.Test_Class_Room;
                    textBox_StudentNumber.Text = test.Student_Number;
                    textBox_StudentScore.Text = test.Score.ToString();
                    comboBox1.SelectedIndex = test.Type.ObjToInt();
                    dateTimePicker.Value = test.Time;
                    textBox_building.ReadOnly = true;
                        textBox_courseIndex.ReadOnly = true;
                        textBox_courseNumber.ReadOnly = true;
                        textBox_roomNumber.ReadOnly = true;
                        textBox_StudentNumber.ReadOnly = true;
                        textBox_StudentScore.ReadOnly = true;
                        comboBox1.Enabled = false;
                        dateTimePicker.Enabled = false;
                    
                }
            }
        }

        private async void button_OK_Click(object sender, EventArgs e)
        {
            var db = Database.Util.GetInstance().db;
            if (arg == null)
            {
                //arg为空代表手工添加数据，而非修改数据
                
                //var obj = db.Queryable<Entity.Test>().First(obj => obj.Index == test.Index);
                //obj.Score = test.Score;
                test.Score = decimal.Parse(textBox_StudentScore.Text);
                db.Updateable(test).ExecuteCommand();
                MessageBox.Show("更新成功！");
                Close();
            }
            else if(arg.ScoreOnly != null)
            {
                Close();
            }
            else
            {
                var paths = await Util.ChooseFiles(this);
                FileInfo file1 = new FileInfo(paths.ElementAt(0));
                FileInfo file2 = new FileInfo(paths.ElementAt(1));
                file1.CopyTo(FileManager.pictureDir);
                file2.CopyTo(FileManager.pictureDir);
                string url1 = $"{FileManager.pictureDir}/{file1.Name}";
                string url2 = $"{FileManager.pictureDir}/{file2.Name}";

                Test t = new Test
                {
                    Test_Class_Room = textBox_roomNumber.Text,
                    Test_Room_Building = textBox_building.Text,
                    Student_Number = textBox_StudentNumber.Text,
                    Score = decimal.Parse(textBox_StudentScore.Text),
                    Course_Index = int.Parse(textBox_courseIndex.Text),
                    Course_Number = textBox_courseNumber.Text,
                    Time = dateTimePicker.Value,
                    Url_Up = url1,
                    Url_Down = url2
                };
                db.Insertable(t).ExecuteCommand();
                MessageBox.Show("添加成功");
                Close();

            }
            
        }
    }
}

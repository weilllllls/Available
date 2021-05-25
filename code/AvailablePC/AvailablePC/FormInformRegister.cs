using AvailablePC.Entity;
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
    public partial class FormInformRegister : Form
    {
        private readonly Database.Util util = Database.Util.GetInstance();
        private DataSet dataSet = null;

        private IEnumerable<string> pictureList;
        public FormInformRegister()
        {
            InitializeComponent();
            FormClosed += (obj, e) => {
                if (e.CloseReason == CloseReason.UserClosing)
                {
                    FunctionWindow f = Program.FunctionWindow;
                    f.Show();
                }
            };
        }

        public FormInformRegister(EventArgs e)
        {
            var eventArgs = e as IEnumerableEventArgs<string>;
            pictureList = eventArgs.List;

            InitializeComponent();
            FormClosed += (obj, e) => {
                if (e.CloseReason == CloseReason.UserClosing)
                {
                    FunctionWindow f = Program.FunctionWindow;
                    f.Show();
                }
            };
        }

        private void button_OK_Click(object sender, EventArgs e)
        {
            if (dataSet == null)
            {
                MessageBox.Show("请选择Excel文件");
                return;
            }
            //IEnumerable<string> pictureList = Directory.EnumerateFiles(FileManager.pictureDir);

            int score_column=-1, student_number_column=-1;

            for( int i = 0;i<dataSet.Tables[0].Columns.Count;++i)
            {
                DataColumn column = dataSet.Tables[0].Columns[i];
                string str = column.ColumnName;
                if (str == "学号" || str == "学生学号"||str == "考生学号" || str == "考号")
                {
                    student_number_column = i;
                    continue;
                }   
                if(str == "成绩"||str == "学生成绩"||str == "考生成绩")
                {
                    score_column=i;
                }
            }

            var dataRow = dataSet.Tables[0].Rows;

            int total = pictureList.Count();

            ProgressForm progress = new ProgressForm();
            progress.Show();
            
            int index = 0;
            try
            {
                for (int ix = 0; ix < pictureList.Count(); ix += 2)
                {
                    (string up, string down) = (pictureList.ElementAt(ix), pictureList.ElementAt(ix + 1));
                    var i = dataRow[index];

                    //
                    //TODO 通过处理第一行标题来得到每一列的类型，然后根据特定的类型名称来处理行。
                    //标题（表头）需要是严格匹配的。
                    //学工号，姓名，电话（教师账号注册，账号=学工号，密码设置默认，学工号后6位/或者用Scu+后n位）


                    /*TestInfo info = new TestInfo
                    {
                        teacher_id = textBox_roomNumber.Text,
                        course_number = textBox_courseNumber.Text,
                        course_index = 1,
                        student_id = i[1].ToString(),
                        uri =uri
                    };*/

                    Test test = new Test
                    {
                        //输入
                        Test_Class_Room = textBox_roomNumber.Text,
                        //输入
                        Test_Room_Building = textBox_building.Text,
                        //excel
                        Student_Number = i[student_number_column].ToString(),
                        //输入
                        Course_Number = textBox_courseNumber.Text,
                        //输入
                        Time = dateTimePicker.Value.Date,
                        //excel
                        Score = decimal.Parse(i[score_column].ToString()),
                        //输入
                        //Type = TestType.Normal,
                        //输入
                        Course_Index = int.Parse(textBox_courseIndex.Text),
                        //自动处理
                        Url_Up = up,
                        Url_Down = down
                    };
                    test.Type = comboBox1.SelectedIndex switch
                    {
                        0 => TestType.Normal,
                        1 => TestType.Supplementary,
                        _ => throw new Exception(),
                    };

                    //util.Add(info);
                    util.Add(test);
                    ++index;
                    progress.progressBarChange((double)index*2 / total);

                }
                MessageBox.Show("试卷归档完成！");
                Close();
                
            }catch(IndexOutOfRangeException)
            {
                MessageBox.Show("试卷数量和Excel中信息条数不匹配，请检查是否选择错误。");
            }
        }

        private void buttonChoose_Click(object sender, EventArgs e)
        {
            string fileName = Util.LoadExcel(out dataSet);
            label_excelChoosen.Text = fileName;
        }

    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

using Microsoft.Office.Interop.Excel;

using AvailablePC.Database2;
using AvailablePC.Entity;

namespace AvailablePC
{
    
    public partial class FormRegisterData : Form
    {
        public FormRegisterData()
        {
            InitializeComponent();
            FormClosed += (obj, e) => {
                if (e.CloseReason == CloseReason.UserClosing)
                    Program.FunctionWindow.Show();
            };
        }

        private void buttonChooseExcel_Click(object sender, EventArgs e)
        {

            Thread thread = new Thread(() =>
            {

                if (openFileDialog.ShowDialog().HasFlag(DialogResult.Abort | DialogResult.Cancel | DialogResult.No | DialogResult))
                {
                    throw new Exception("Error");
                }
            });
            thread.SetApartmentState(ApartmentState.STA);
            thread.Start();
            thread.Join();

            string path = openFileDialog.FileName;
            #region 弃用代码
            /*var app = new Microsoft.Office.Interop.Excel.Application();
            Workbook workbook = null;
            object oMissing = Missing.Value;
            try
            {
                workbook = app.Workbooks.Open(path,
                    oMissing, oMissing, oMissing, oMissing,
                    oMissing, oMissing, oMissing, oMissing, 
                    oMissing, oMissing, oMissing, oMissing, 
                    oMissing,oMissing);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                workbook.Close();
                return;
            }
            Worksheet worksheet = workbook.Sheets.Item[1];

            int column = worksheet.UsedRange.Cells.Column;
            int row = worksheet.UsedRange.Cells.Row;

            dataSetExcel.Tables.Add();
            
            for (int i = 1; i <= column; ++i)
            {
                

                dataSetExcel.Tables[0].Columns.Add(worksheet.Cells[1,i].Value2.ToString());
            }
            System.Data.DataTable table = dataSetExcel.Tables[0];
            for(int i = 1; i <= row; ++i)
            {
                DataRow dataRow = table.NewRow();
                for(int j = 1; j <= column; ++j)
                {
                    dataRow.ItemArray[j - 1] = worksheet.UsedRange.Cells[i,j];
                }
                dataSetExcel.Tables[0].Rows.Add(dataRow);
            }
            //需要手动杀死进程
            //需要将读到的数据添加到显示
            dataGridView1.DataSource = dataSetExcel;
            dataGridView1.Refresh();
*/
            #endregion
            DataSet ds = Database2.Util.OpenExcel(path);


            var bindingSource = new BindingSource{DataSource = ds};
            dataGridView1.AutoGenerateColumns = true;
            dataGridView1.DataSource = bindingSource;
            dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCellsExceptHeaders;
            dataGridView1.DataMember = ds.Tables[0].TableName;
            
            dataGridView1.Refresh();

            dataSetExcel = ds;

            buttonChooseExcel.Visible = false;
            buttonChooseExcel.Enabled = false;
            var b = new System.Windows.Forms.Button
            {
                Dock = DockStyle.Bottom,
                Text = "确定",
                Font = new System.Drawing.Font("宋体", 18F),
                Location = new System.Drawing.Point(0,500),
                Margin = new Padding(4, 4, 4, 4),
                Name = "buttonChooseExcel",
                Size = new Size(1067, 62)
        };
            Controls.Add(b);
            b.Click += (o, e) =>
            {
                var table = dataSetExcel.Tables[0];

                int ID_column=-1, Name_column=-1;

                for(int i = 0; i < table.Columns.Count; ++i)
                {
                    string str = table.Columns[i].ToString();
                    //string str = table.Rows[0][i].ToString();
                    if(str=="姓名"||str == "教师姓名")
                    {
                        Name_column = i;
                        continue;
                    }
                    else if (str.ToUpper() == "ID" || str == "教师工号" || str == "教师ID")
                    {
                        ID_column = i;
                        continue;
                    }
                }

                foreach(var obj in table.Rows)
                {
                    var x = obj as DataRow;
                    string id = x[ID_column].ToString();
                    User u = new User
                    {
                        ID = id,
                        Name = x[Name_column].ToString(),
                        Password = id.Substring(id.Length - 6, 6)
                    };
                    var db = Database.Util.GetInstance().db;
                    db.Insertable(u).ExecuteCommand();
                }

                MessageBox.Show("添加成功,默认密码ID后6位");
                Program.FunctionWindow.Show();
                Close();
            };

        }
    }


}

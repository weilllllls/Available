using AvailablePC.Entity;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace AvailablePC
{
    public partial class PictureViewForm : Form
    {
        /**
         *TODO 显示完成后，点击确定或否决，确定就传输数据，否定则回传决定结果。 
         */

        private readonly Request request;
        private readonly SqlSugarClient db = Database.Util.GetInstance().db;

        private Stream up, down;

        public PictureViewForm(EventArgs e)
        {
            var args = e as RequestEventArgs;
            request = args.Request;

            FormClosed += (obj, e) => {
                if (e.CloseReason == CloseReason.UserClosing)
                    Program.FunctionWindow.Show();
            };

            CheckForIllegalCrossThreadCalls = false;
            InitializeComponent();
            pictureBox1.Dock = DockStyle.None;

            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;

            textBox.Text = request.Summary();
            //textBox.Refresh();
            //不显示进度条
            /*pdfPageView1.RenderingProgressDisplay = O2S.Components.PDFView4NET.PDFRenderingProgressDisplayMode.None;


            HandleCreated += (e, obj) =>
            {
                var test = db.Queryable<Test>().First(obj =>
                request.Course_Index == obj.Course_Index &&
                request.Course_Number == obj.Course_Number &&
                request.Student_Number == obj.Student_Number &&
                request.Time == obj.Time
                    );


                //string file = await Util.ChooseFile(this);
                string file = test.Url;
                pdfDocument1.Load(file);
            };*/

            HandleCreated += (e, obj) =>
            {
               var test = db.Queryable<Test>().First(obj =>
               request.Course_Index == obj.Course_Index &&
               request.Course_Number == obj.Course_Number &&
               request.Student_Number == obj.Student_Number &&
               request.Time == obj.Time
                   );
                try
                {
                    up = File.OpenRead(test.Url_Up);
                    down = File.OpenRead(test.Url_Down);

                    pictureBox1.Image = Image.FromStream(up);
                }
                catch (FileNotFoundException)
                {
                    MessageBox.Show("目标请求考试图片缺失，可能是请求信息有误");
                }
            };

        }


        public PictureViewForm()
        {
            CheckForIllegalCrossThreadCalls = false;
            InitializeComponent();

            //不显示进度条
            //pdfPageView1.RenderingProgressDisplay = O2S.Components.PDFView4NET.PDFRenderingProgressDisplayMode.None;

           /* HandleCreated += async (e, obj) =>
            {
                string file =await Util.ChooseFile(this);
                //pdfDocument1.Load(file);
            };*/



        }

        private void PictureViewForm_Scroll(object sender, ScrollEventArgs e)
        {

        }

        private void pdfPageView1_Scroll(object sender, ScrollEventArgs e)
        {
            //pdfPageView1.Scale()
            //TODO 这个触发不了？
            //pdfPageView1.Scale(new SizeF(Width * (e.NewValue - e.OldValue), Height * (e.NewValue - e.OldValue)));
        }

        private void pdfView1_MouseWheel(object sender,MouseEventArgs e)
        {
            //pdfPageView1.Zoom += e.Delta;
        }

        private void ToolStripMenuItem_Preview_Click(object sender, EventArgs e)
        {
            //pdfPageView1.GoToPrevPage();
            pictureBox1.Image = Image.FromStream(up);

        }

        private void ToolStripMenuItem_Next_Click(object sender, EventArgs e)
        {
            //pdfPageView1.GoToNextPage();
            pictureBox1.Image = Image.FromStream(down);
        }

        private void ToolStripMenuItem_Rotate_Click(object sender, EventArgs e)
        {
            //pdfPageView1.R
        }

        private void ToolStripMenuItem_Bigger_Click(object sender, EventArgs e)
        {
            //pdfPageView1.Zoom += 5;
            //pictureBox1.Scale(new SizeF(0.9F, 0.9F));
            pictureBox1.Width = (int)(pictureBox1.Width*1.1F);
            pictureBox1.Height = (int)(pictureBox1.Height * 1.1F);
        }

        private void ToolStripMenuItem_Smaller_Click(object sender, EventArgs e)
        {
            /*if (pdfPageView1.Zoom > 5)
                pdfPageView1.Zoom -= 5;*/
            /* pictureBox1.Scale(new SizeF(1.1F, 1.1F));*/
            pictureBox1.Width = (int)(pictureBox1.Width * 0.9F);
            pictureBox1.Height = (int)(pictureBox1.Height * 0.9F);
        }

        private void buttonYes_Click(object sender, EventArgs e)
        {
            var db = Database.Util.GetInstance().db;
            var test = db.Queryable<Test>().First(obj =>
                    obj.Course_Index ==request.Course_Index &&
                    request.Course_Number == obj.Course_Number&&
                    obj.Student_Number ==request.Student_Number&&
                    obj.Time == request.Time);

            Program.FunctionWindow.RemoveItem(request.Request_Time);
            switch (request.Request_Type)
            {

                case RequestType.Update:
                    {
                        request.Request_State = RequestState.Finish;
                        db.Updateable(request).ExecuteCommand();
                        Util.DeleteRow(Program.INFO_FILE, request.Request_Time);
                        //TODO ？
                        //Util.JumpToWindow(this, typeof(FormPaper), new TestEventArgs { Test = test ,ScoreOnly = true});

                        new FormPaper(new TestEventArgs { Test = test, ScoreOnly = true }).ShowDialog();
                        Reply reply = new Reply()
                        {
                            Course_Index = request.Course_Index,
                            Course_Number = request.Course_Number,
                            Student_Number = request.Student_Number,
                            Teacher_ID = request.Teacher_ID,
                            Time = request.Time,
                            Content = "您的修改请求已通过，教务处已修改成绩！"
                        };
                        try
                        {
                            Program.http.Post(Program.ReplyPath, reply);
                        }
                        catch (Exception)
                        {
                            //TODO 网络异常需要处理，无法连接到服务器
                        }
                        MessageBox.Show("回执已发送");
                        Close();
                        break;
                    }
                case RequestType.Query:
                    {
                        request.Request_State = RequestState.Finish;
                        db.Updateable(request).ExecuteCommand();
                        Util.DeleteRow(Program.INFO_FILE, request.Request_Time);

                        Reply reply = new Reply()
                        {
                            Course_Index = request.Course_Index,
                            Course_Number = request.Course_Number,
                            Student_Number = request.Student_Number,
                            Teacher_ID = request.Teacher_ID,
                            Time = request.Time,
                            Content = "您的查询请求已通过！"
                        };
                        try
                        {
                            Program.http.Post(Program.UploadPath, reply, new FileInfo(test.Url_Up), new FileInfo(test.Url_Down));
                        }
                        catch (Exception)
                        {
                            //TODO 网络异常需要处理，无法连接到服务器
                        }
                        MessageBox.Show("已发送数据！");
                        Close();
                        break;
                    }
                default:
                    throw new Exception();
            }

            //TODO 调用HTTP或者kafka回传数据
        }

        private void buttonNo_Click(object sender, EventArgs e)
        {
            Program.FunctionWindow.RemoveItem(request.Request_Time);
            var db = Database.Util.GetInstance().db;
            request.Request_State = RequestState.Denied;
            db.Updateable(request).ExecuteCommand();
            Util.DeleteRow(Program.INFO_FILE, request.Request_Time);
            MessageBox.Show("回执已发送！");
            Util.JumpToWindow(this, typeof(FunctionWindow));
            //TODO 调用HTTP或者kafka回传拒绝结果
        }

        private void pictureBox1_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            /*switch (e.KeyCode)
            {
                case Keys.Up:
                    pictureBox1.Location.Offset(-5,0) ;
                    break;
                case Keys.Down:
                    pictureBox1.Location.Offset(5, 0);
                    break;
                case Keys.Left:
                    pictureBox1.Location.Offset(0, -5);
                    break;
                case Keys.Right:
                    pictureBox1.Location.Offset(0, 5);
                    break;
            }       */     
        }

        private void PictureViewForm_SizeChanged(object sender, EventArgs e)
        {
            textBox.Size=new Size(Width,textBox.Height);
            pictureBox1.Width = Width;
            pictureBox1.Height = splitContainer1.Panel2.Height;
            pictureBox1.Location = new Point(0, 0);
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AvailablePC
{
    public partial class FormManualManage : Form
    {
        public FormManualManage()
        {
            InitializeComponent();
            FormClosed += (obj, e) => {
                if (e.CloseReason == CloseReason.UserClosing)
                {
                    Program.FunctionWindow.Visible = true;
                }
            };
        }

        private void Form1_SizeChanged(object sender, EventArgs e)
        {

        }

        private void button_teacher_Click(object sender, EventArgs e)
        {
            //TODO
            Util.JumpToWindow(this, typeof(FormQueryTeacher));
        }

        private void button_preview_Click(object sender, EventArgs e)
        {
            /*Util.JumpToWindow(this, typeof(FunctionWindow));*/
            Program.FunctionWindow.Visible = true;
            Close();
        }

        private void button_paper_Click(object sender, EventArgs e)
        {
            Util.JumpToWindow(this, typeof(FormQuery));
        }
    }
}

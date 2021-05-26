using AvailablePC.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AvailablePC
{
    public partial class PictureView : Form
    {
        private readonly Test test;
        public PictureView(Test t)
        {
            test = t;
            InitializeComponent();
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.Image = Image.FromFile(test.Url_Up);
            pictureBox2.Image = Image.FromFile(test.Url_Down);
            splitContainer1.SplitterDistance = Width / 2;
        }

        private void PictureView_SizeChanged(object sender, EventArgs e)
        {
            splitContainer1.SplitterDistance = Width / 2;
        }
    }
}

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
    public partial class ProgressForm : Form
    {
        public ProgressForm()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
        }

        public void progressBarChange(double value)
        {
            progressBar1.Value = (int)(value * 100);
            if (progressBar1.Value == 100)
                Close();
            //Refresh();
        }

       /* protected override void WndProc(ref Message m)
        {
            const int WM_SYSCOMMAND = 0x0112;

            const int SC_CLOSE = 0xF060;


            if (m.Msg == WM_SYSCOMMAND && (int)m.WParam == SC_CLOSE)
            {
                return;
            }
            base.WndProc(ref m);
        }*/
    }
}

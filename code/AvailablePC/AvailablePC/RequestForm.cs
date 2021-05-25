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
    public partial class RequestForm : Form
    {
        public RequestForm()
        {
            InitializeComponent();

            FormClosed += (obj, e) => {
                if (e.CloseReason == CloseReason.UserClosing)
                    new FunctionWindow().Show();
            };
        }
    }
}

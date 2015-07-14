using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Qz.Winform
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }

        private void btnSZLJ_Click(object sender, EventArgs e)
        {
            ExportDataLJ frm = new ExportDataLJ();
            frm.Show();

        }

        private void btnGZ_Click(object sender, EventArgs e)
        {
            ExportDataGZ frm = new ExportDataGZ();
            frm.Show();

        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ReadyToRead
{
    public partial class FrmEsplora : Form
    {
        public FrmEsplora()
        {
            InitializeComponent();
        }

        private void flpnlLibro_Click(object sender, EventArgs e)
        {
            this.Close();
            FrmLibro frmLibro = new FrmLibro();
            //frmLibro.MdiParent = frmMain;
            frmLibro.Show();

           
        }
    }
}

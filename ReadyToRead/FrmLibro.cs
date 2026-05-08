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
    public partial class FrmLibro : Form
    {
        public FrmLibro()
        {
            InitializeComponent();
        }

        private void btnIndietro_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

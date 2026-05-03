using System;
using MySqlConnector;
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
    public partial class FrmMain : Form
    {
        //Program.frmMain=this;
        public FrmMain()
        {
            InitializeComponent();
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            btnDashboard_Click(sender, e);
            pbUtente.Image = Program._fotoProfilo;
        }

        private Form frmCorrente = null;
        private Button btnPrecedente = null;
        private void SelezionaBottone(Button btn)
        {
            if (btnPrecedente != null && btnPrecedente != btn)
                btnPrecedente.BackColor = Color.FromArgb(241, 242, 229);//bianco

            btn.BackColor = Color.FromArgb(223, 84, 97);//Rosso
            btnPrecedente = btn;
        }
        private void AprireFormMDI(Form nuovaForm, Button btn, string titoloForm)
        {
            if (frmCorrente != null)
            {
                frmCorrente.Close();
                frmCorrente.Dispose();//libero risorse non usate
            }

            nuovaForm.MdiParent = this;
            nuovaForm.WindowState = FormWindowState.Maximized;

            nuovaForm.Show();

            lblTitolo.Text = titoloForm;

            frmCorrente = nuovaForm;

            SelezionaBottone(btn);
        }
        private void btnDashboard_Click(object sender, EventArgs e)
        {
            FrmDashboard frmDashboard = new FrmDashboard();
            AprireFormMDI(frmDashboard, btnDashboard, "Dashboard");
        }

        private void btnEsplora_Click(object sender, EventArgs e)
        {
            FrmEsplora frmEsplora = new FrmEsplora();
            AprireFormMDI(frmEsplora, btnEsplora, "Esplora");
        }

        private void btnMenuProfilo_Click(object sender, EventArgs e)
        {
            if (flpContentUtente.Visible == true)
            {
                flpContentUtente.Visible = false;
                btnMenuProfilo.BackColor = Color.FromArgb(241, 242, 229);
            }
            else
            {
                flpCarrello.Visible = false;
                flpContentUtente.Visible = true;
                btnCarrello.BackColor = Color.FromArgb(241, 242, 229);
                btnMenuProfilo.BackColor = Color.FromArgb(223, 84, 97);
            }
        }

        private void btnCarrello_Click(object sender, EventArgs e)
        {
            if (flpCarrello.Visible == true)
            {
                flpCarrello.Visible = false;
                btnCarrello.BackColor = Color.FromArgb(241, 242, 229);
            }
            else
            {
                flpContentUtente.Visible = false;
                flpCarrello.Visible = true;
                btnMenuProfilo.BackColor = Color.FromArgb(241, 242, 229);
                btnCarrello.BackColor = Color.FromArgb(223, 84, 97);
            }
        }

        private void btnProfilo_Click(object sender, EventArgs e)
        {
            FrmProfilo frmProfilo = new FrmProfilo();
            AprireFormMDI(frmProfilo, btnMenuProfilo, "Area Personale");
            flpContentUtente.Visible = false;
        }

        private void btnDisconnetti_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

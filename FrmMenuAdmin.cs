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
    public partial class FrmMenuAdmin : Form
    {
        public FrmMenuAdmin()
        {
            InitializeComponent();
        }
        private void FrmMenuAdmin_Load(object sender, EventArgs e)
        {
            btnCasaEditrice_Click(sender, e);
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

            lblTitolo.Text = "Gestisci "+ titoloForm;

            frmCorrente = nuovaForm;

            SelezionaBottone(btn);
        }
        private void btnCasaEditrice_Click(object sender, EventArgs e)
        {
            FrmCaseEditrici frmCasaEditrici = new FrmCaseEditrici();
            AprireFormMDI(frmCasaEditrici, btnCasaEditrice, "Case Editrici");
        }

        private void btnAutori_Click(object sender, EventArgs e)
        {
            FrmAutori frmAutori = new FrmAutori();
            AprireFormMDI(frmAutori, btnAutori, "Autori");
        }

        private void btnGeneri_Click(object sender, EventArgs e)
        {
            FrmGeneri frmGeneri = new FrmGeneri();
            AprireFormMDI(frmGeneri, btnGeneri, "Generi");
        }

        private void btnProdotti_Click(object sender, EventArgs e)
        {
            FrmLibri frmLibri = new FrmLibri();
            AprireFormMDI(frmLibri, btnProdotti, "Libri");
        }

        private void btnOrdini_Click(object sender, EventArgs e)
        {
            FrmOrdini frmOrdini = new FrmOrdini();
            AprireFormMDI(frmOrdini, btnOrdini, "Ordini");
        }
    }
}

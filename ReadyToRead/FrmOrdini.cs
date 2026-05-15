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
    public partial class FrmOrdini : Form //Amir
    {
        private List<ClsOrdinare> _ordini = new List<ClsOrdinare>();
        enum eTipoOrdine
        {
            Normale,
            Crescente,
            Descrescente,
            Più_Recente,
            Meno_Recente
        }
        public FrmOrdini()
        {
            InitializeComponent();
        }
        private void FrmOrdini_Load(object sender, EventArgs e)
        {
            PopolaComboBox();
            CaricaOrdini();
        }
        private void PopolaComboBox()
        {
            cbStatoOrdine.Items.Clear();
            cbStatoOrdine.DataSource = Enum.GetValues(typeof(ClsOrdinare.eStatoOrdine));
            cbStatoOrdine.SelectedIndex = -1;

            cbOrdina.Items.Clear();
            cbOrdina.DataSource= Enum.GetValues(typeof(eTipoOrdine));
            cbOrdina.SelectedIndex = -1;
        }

        private void CaricaOrdini()
        {
            string errore;
            _ordini = ClsOrdinareBL.GetAll(ref Program.conn, out errore);

            if (!string.IsNullOrEmpty(errore))
                MessageBox.Show("Errore: " + errore, "Errore", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
                PopolaListView(_ordini);
        }

        private void PopolaListView(List<ClsOrdinare> ordini)
        {
            lvOrdini.Items.Clear();
            for (int i = 0;  i < ordini.Count;i++)
            {
                ClsOrdinare o = ordini[i];
                ListViewItem lvi = new ListViewItem(o.ID.ToString());
                lvi.SubItems.Add(o.ClienteID.ToString()); //recupera il nome
                lvi.SubItems.Add("");               // recupera il cliente e dunque poi il contatto
                lvi.SubItems.Add(o.StatoOrdine.ToString());           
                lvi.SubItems.Add(o.Data.ToString());         
                lvi.SubItems.Add(o.Totale.ToString()+"€");
                lvi.Tag = o;
                lvOrdini.Items.Add(lvi);
            }
        }


        private void cbOrdina_SelectedIndexChanged(object sender, EventArgs e)
        {
            eTipoOrdine tipo=eTipoOrdine.Normale;
            if (cbOrdina.SelectedIndex!=-1)
                tipo = (eTipoOrdine)cbOrdina.SelectedItem;

            if (tipo == eTipoOrdine.Crescente)
                PopolaListView(_ordini.OrderBy(o => o.Totale).ToList());
            else if (tipo == eTipoOrdine.Descrescente)
                PopolaListView(_ordini.OrderByDescending(o => o.Totale).ToList());
            else if (tipo == eTipoOrdine.Più_Recente)
                PopolaListView(_ordini.OrderByDescending(o => o.Data).ToList());
            else if (tipo == eTipoOrdine.Meno_Recente)
                PopolaListView(_ordini.OrderBy(o => o.Data).ToList());
            else
                PopolaListView(_ordini);
        }

        private void tbFiltroCliente_TextChanged(object sender, EventArgs e)
        {
            string testo = tbFiltroCliente.Text.Trim();
            if (string.IsNullOrEmpty(testo))
                PopolaListView(_ordini);
            else
            {
                List<ClsOrdinare> filtrati = new List<ClsOrdinare>();
                for (int i = 0; i < _ordini.Count; i++)
                {
                    if (_ordini[i].Destinazione != null && _ordini[i].Destinazione.ToLower().Contains(testo.ToLower()))
                        filtrati.Add(_ordini[i]);
                }
                PopolaListView(filtrati);
            }
        }
        
        private void btnApplicaFiltri_Click(object sender, EventArgs e)
        {
            List<ClsOrdinare> filtrati = new List<ClsOrdinare>(_ordini);

            if (cbStatoOrdine.SelectedIndex >= 0)
            {
                string statoScelto = cbStatoOrdine.SelectedItem.ToString();
                string errore;
                filtrati = ClsOrdinareBL.GetByStato(ref Program.conn, statoScelto, out errore);
                if (!string.IsNullOrEmpty(errore))
                {
                    MessageBox.Show("Errore: " + errore, "Errore", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    filtrati = new List<ClsOrdinare>(_ordini);
                }
            }

            decimal minP = nudMinPrezzo.Value;
            decimal maxP = nudMaxPrezzo.Value;
            if (maxP > 0)
            {
                List<ClsOrdinare> risultato = new List<ClsOrdinare>();

                for (int i = 0; i < filtrati.Count; i++)
                {
                    if (filtrati[i].Totale >= minP && filtrati[i].Totale <= maxP)
                        risultato.Add(filtrati[i]);
                }

                filtrati = risultato;
            }

            PopolaListView(filtrati);
        }


        private void btnReset_Click(object sender, EventArgs e)
        {
            cbStatoOrdine.SelectedIndex = -1;
            tbFiltroCliente.Clear();
            nudMinPrezzo.Value = 0;
            nudMaxPrezzo.Value = 15;
            dtpData.Value = DateTime.Now;
            CaricaOrdini();
        }
    }
}

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
    public partial class FrmGeneri : Form //Amir
    {
        private List<ClsGenere> _generi = new List<ClsGenere>();
        private bool _modalitaModifica = false;
        private ClsGenere _genereSelezionato = null;
        private long _idSelezionato = -1;

        public FrmGeneri()
        {
            InitializeComponent();
        }

        private void FrmGeneri_Load(object sender, EventArgs e)
        {
            CaricaGeneri();
        }

        private void CaricaGeneri()
        {
            string errore;
            _generi = ClsGenereBL.GetAll(ref Program.conn, out errore);

            if (!string.IsNullOrEmpty(errore))
                MessageBox.Show("Errore: " + errore, "Errore", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
                PopolaListView(_generi);
        }

        private void PopolaListView(List<ClsGenere> generi)
        {
            lvGeneri.Items.Clear();
            for (int i = 0; i < generi.Count; i++)
            {
                ClsGenere g = generi[i];
                ListViewItem lvi = new ListViewItem(g.Nome);
                lvi.SubItems.Add(g.Target);
                lvi.Tag = g;
                lvGeneri.Items.Add(lvi);
            }
        }

        private void ResetCampi()
        {
            tbNome.Clear();
            tbTarget.Clear();
            rtbDescrizione.Clear();
            _modalitaModifica = false;
            _genereSelezionato = null;
            _idSelezionato = -1;
            lblTitolo.Text = "Crea Genere";
            btnAggiungi.Text = "➕Aggiungi";

            if (Program._chiudiForm)
                this.Close();
        }

        private ClsGenere LeggiCampi()
        {
            ClsGenere genere = new ClsGenere();
            genere.Nome = tbNome.Text.Trim();
            genere.Target = tbTarget.Text.Trim();
            genere.Descrizione = rtbDescrizione.Text.Trim();
            return genere;
        }

        private void tbFiltroNome_TextChanged(object sender, EventArgs e)
        {
            string testo = tbFiltroNome.Text.Trim();
            if (string.IsNullOrEmpty(testo))
                PopolaListView(_generi);
            else
            {
                string errore;
                List<ClsGenere> filtrati = ClsGenereBL.GetByNome(ref Program.conn, testo, out errore);
                if (!string.IsNullOrEmpty(errore))
                    MessageBox.Show("Errore: " + errore, "Errore", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else
                    PopolaListView(filtrati);
            }
        }

        bool modalitàVisualizza = false;
        private void btnVisualizza_Click(object sender, EventArgs e)
        {
            if (lvGeneri.SelectedItems.Count == 0)
                MessageBox.Show("Seleziona un genere da visualizzare.", "Attenzione", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else
                ModificaBtnVisualizza();
        }

        private void ModificaBtnVisualizza()
        {
            modalitàVisualizza = !modalitàVisualizza;
            if (modalitàVisualizza)
            {
                VisualizzaGenere();
                btnAggiungi.Visible = false;
                btnAnnulla.Visible = false;
                btnVisualizza.ForeColor = Color.DodgerBlue;
                btnVisualizza.Text = "👁️Smetti";
                CampiReadOnly(true);
            }
            else
            {
                ResetCampi();
                btnAggiungi.Visible = true;
                btnAnnulla.Visible = true;
                btnVisualizza.ForeColor = Color.Black;
                btnVisualizza.Text = "👁️Visualizza";
                CampiReadOnly(false);
            }
        }
        private void CampiReadOnly(bool rendiReadOnly)
        {
            tbNome.ReadOnly = rendiReadOnly;
            tbTarget.ReadOnly = rendiReadOnly;
            rtbDescrizione.ReadOnly = rendiReadOnly;

            btnElimina.Enabled = !rendiReadOnly;
            btnModifica.Enabled = !rendiReadOnly;
            tbFiltroNome.ReadOnly = rendiReadOnly;
        }
        private void VisualizzaGenere()
        {
            _genereSelezionato = (ClsGenere)lvGeneri.SelectedItems[0].Tag;
            _idSelezionato = _genereSelezionato.ID;
            tbNome.Text = _genereSelezionato.Nome;
            tbTarget.Text = _genereSelezionato.Target;
            rtbDescrizione.Text = _genereSelezionato.Descrizione;
        }

        private void btnModifica_Click(object sender, EventArgs e)
        {
            if (lvGeneri.SelectedItems.Count == 0)
                MessageBox.Show("Seleziona un genere da modificare.", "Attenzione", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else
            {
                VisualizzaGenere();
                _modalitaModifica = true;
                lblTitolo.Text = "Modifica Genere";
                btnAggiungi.Text = "☑️ Salva";
            }
        }

        private void btnElimina_Click(object sender, EventArgs e)
        {
            if (lvGeneri.SelectedItems.Count == 0)
                MessageBox.Show("Seleziona un genere da eliminare.", "Attenzione", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else
            {
                DialogResult dr = MessageBox.Show("Vuoi eliminare il genere selezionato?", "Conferma", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dr == DialogResult.Yes)
                {
                    ClsGenere g = (ClsGenere)lvGeneri.SelectedItems[0].Tag;
                    string errore;
                    long esito = ClsGenereBL.Delete(ref Program.conn, g.ID, out errore);
                    if (!string.IsNullOrEmpty(errore))
                        MessageBox.Show("Errore: " + errore, "Errore", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    else if (esito > 0)
                    {
                        MessageBox.Show("Genere eliminato.", "Successo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ResetCampi();
                        CaricaGeneri();
                    }
                }
            }
        }

        private void btnAggiungi_Click(object sender, EventArgs e)
        {
            GestisciGenere(_modalitaModifica);
        }

        private void GestisciGenere(bool modificaGenere)
        {
            ClsGenere genere = LeggiCampi();
            string errore;

            if (!modificaGenere)
            {
                long id = ClsGenereBL.Create(ref Program.conn, genere, out errore);
                if (!string.IsNullOrEmpty(errore))
                    MessageBox.Show("Errore: " + errore, "Errore", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else if (id > 0)
                {
                    MessageBox.Show("Genere aggiunto con successo!", "Successo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ResetCampi();
                    CaricaGeneri();
                }
            }
            else
            {
                if (_genereSelezionato == null)
                    MessageBox.Show("Seleziona un genere da modificare.", "Attenzione", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                else
                {
                    long esito = ClsGenereBL.Update(ref Program.conn, _idSelezionato, genere, out errore);
                    if (!string.IsNullOrEmpty(errore))
                        MessageBox.Show("Errore: " + errore, "Errore", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    else if (esito > 0)
                    {
                        MessageBox.Show("Genere modificato con successo!", "Successo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ResetCampi();
                        CaricaGeneri();
                    }
                }
            }

            if (Program._chiudiForm)
                this.Close();
        }

        private void btnAnnulla_Click(object sender, EventArgs e)
        {
            ResetCampi();
        }

        private void FrmGeneri_FormClosing(object sender, FormClosingEventArgs e)
        {
            Program._chiudiForm = false;
        }
    }
}

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
    public partial class FrmAutori : Form
    {
        private List<ClsAutore> _autori = new List<ClsAutore>();
        private bool _modalitaModifica = false;
        private ClsAutore _autoreSelezionato = null;
        private long _idSelezionato = -1;

        enum eFILTRO { Tutti, Verificati, Non_Verificati }

        public FrmAutori()
        {
            InitializeComponent();
        }

        private void FrmAutori_Load(object sender, EventArgs e)
        {
            PopolaComboBox();
            CaricaAutori();
            cbVerificato.SelectedIndex = 0;
        }

        private void PopolaComboBox()
        {
            cbVerificato.DataSource = Enum.GetValues(typeof(eFILTRO));
        }

        private void CaricaAutori()
        {
            string errore;
            _autori = ClsAutoreBL.GetAll(ref Program.conn, out errore);

            if (!string.IsNullOrEmpty(errore))
                MessageBox.Show("Errore: " + errore, "Errore", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
                PopolaListView(_autori);
        }

        private void PopolaListView(List<ClsAutore> autori)
        {
            lvAutori.Items.Clear();
            for (int i = 0; i < autori.Count; i++)
            {
                ClsAutore a = autori[i];
                ListViewItem lvi = new ListViewItem(a.ÈVerificato ? "✔" : "✘");
                lvi.SubItems.Add(a.Nome + " " + a.Cognome);
                lvi.SubItems.Add(a.CittaNascita);

                if (!a.DataMorte.HasValue)
                    lvi.SubItems.Add(a.DataDiNascita.Year + " - ora");
                else
                    lvi.SubItems.Add(a.DataDiNascita.Year + " - " + a.DataMorte.Value.Year);

                lvi.Tag = a;
                lvAutori.Items.Add(lvi);
            }
        }

        private void ResetCampi()
        {
            tbNome.Clear();
            tbCognome.Clear();
            tbNomeArte.Clear();
            rbVerificato.Checked = false;
            rbNonVerificato.Checked = false;
            rbM.Checked = false;
            rbF.Checked = false;
            dtpDataDiNascita.Value = DateTime.Now;
            dtpDataMorte.Value = DateTime.Now;
            dtpDataMorte.Visible = false;
            chkAttivaDataMorte.Checked = false;
            tbCittà.Clear();
            _modalitaModifica = false;
            _autoreSelezionato = null;
            _idSelezionato = -1;
            lblTitolo.Text = "Crea Autore";
            btnAggiungi.Text = "➕Aggiungi";

            if (Program._chiudiForm)
                this.Close();
        }

        private ClsAutore LeggiCampi()
        {
            ClsAutore autore = new ClsAutore();
            autore.Nome = tbNome.Text.Trim();
            autore.Cognome = tbCognome.Text.Trim();
            autore.NomeArte = tbNomeArte.Text.Trim();
            autore.Username = "-";
            autore.Password = "-";
            autore.Email = "-";
            autore.ÈVerificato = rbVerificato.Checked;
            autore.DataDiNascita = dtpDataDiNascita.Value;
            autore.Sesso = rbM.Checked ? ClsUtente.eSESSO.m : ClsUtente.eSESSO.f;
            autore.ComuneDiNascita = ClsUtente.eCOMUNE.Nessuno;
            autore.CittaNascita = tbCittà.Text.Trim();
            if (dtpDataMorte.Visible)
                autore.DataMorte = dtpDataMorte.Value;
            else
                autore.DataMorte = null;
            return autore;
        }

        private bool ValidaCampi()
        {
            bool valido = true;

            if (rbNonVerificato.Checked != true && rbVerificato.Checked != true)
                valido = false;
            else if (string.IsNullOrWhiteSpace(tbNome.Text))
                valido = false;
            else if (string.IsNullOrWhiteSpace(tbCognome.Text))
                valido = false;
            else if (dtpDataDiNascita.Value >= DateTime.Now)
                valido = false;
            else if (rbM.Checked != true && rbF.Checked != true)
                valido = false;
            else if (string.IsNullOrWhiteSpace(tbCittà.Text))
                valido = false;

            if (!valido)
                MessageBox.Show("Compilare tutti i campi correttamente", "Errore", MessageBoxButtons.OK, MessageBoxIcon.Error);

            return valido;
        }

        private void GestisciAutore(bool modificaAutore)
        {
            ClsAutore autore = LeggiCampi();
            string errore;

            if (!modificaAutore)
            {
                long id = ClsAutoreBL.Create(ref Program.conn, autore, out errore);
                if (!string.IsNullOrEmpty(errore))
                    MessageBox.Show("Errore: " + errore, "Errore", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else if (id > 0)
                {
                    MessageBox.Show("Autore aggiunto con successo!", "Successo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ResetCampi();
                    CaricaAutori();
                }
            }
            else
            {
                if (_autoreSelezionato == null)
                    MessageBox.Show("Seleziona un autore da modificare.", "Attenzione", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                else
                {
                    long esito = ClsAutoreBL.Update(ref Program.conn, _idSelezionato, autore, out errore);
                    if (!string.IsNullOrEmpty(errore))
                        MessageBox.Show("Errore: " + errore, "Errore", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    else if (esito > 0)
                    {
                        MessageBox.Show("Autore modificato con successo!", "Successo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ResetCampi();
                        CaricaAutori();
                    }
                }
            }

            if (Program._chiudiForm)
                this.Close();
        }

        private void VisualizzaAutore()
        {
            if (lvAutori.SelectedItems.Count == 0)
                MessageBox.Show("Seleziona un autore da visualizzare.", "Attenzione", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else
            {
                _autoreSelezionato = (ClsAutore)lvAutori.SelectedItems[0].Tag;
                _idSelezionato = _autoreSelezionato.ID;
                rbVerificato.Checked = _autoreSelezionato.ÈVerificato;
                rbNonVerificato.Checked = !_autoreSelezionato.ÈVerificato;
                tbNomeArte.Text = _autoreSelezionato.NomeArte;
                tbNome.Text = _autoreSelezionato.Nome;
                tbCognome.Text = _autoreSelezionato.Cognome;
                dtpDataDiNascita.Value = _autoreSelezionato.DataDiNascita;

                if (_autoreSelezionato.DataMorte.HasValue)
                {
                    dtpDataMorte.Value = _autoreSelezionato.DataMorte.Value;
                    chkAttivaDataMorte.Checked = true;
                    dtpDataMorte.Visible = true;
                }
                else
                {
                    dtpDataMorte.Value = DateTime.Now;
                    chkAttivaDataMorte.Checked = false;
                    dtpDataMorte.Visible = false;
                }

                rbM.Checked = _autoreSelezionato.Sesso == ClsUtente.eSESSO.m;
                rbF.Checked = _autoreSelezionato.Sesso == ClsUtente.eSESSO.f;
                tbCittà.Text = _autoreSelezionato.CittaNascita;
            }
        }

        private void btnAggiungi_Click(object sender, EventArgs e)
        {
            if (ValidaCampi())
                GestisciAutore(_modalitaModifica);
        }

        private void btnVisualizza_Click(object sender, EventArgs e)
        {
            VisualizzaAutore();
        }

        private void btnModifica_Click(object sender, EventArgs e)
        {
            if (lvAutori.SelectedItems.Count == 0)
                MessageBox.Show("Seleziona un autore da modificare.", "Attenzione", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else
            {
                VisualizzaAutore();
                _modalitaModifica = true;
                lblTitolo.Text = "Modifica Autore";
                btnAggiungi.Text = "☑️ Salva";
            }
        }

        private void btnElimina_Click(object sender, EventArgs e)
        {
            if (lvAutori.SelectedItems.Count == 0)
                MessageBox.Show("Seleziona almeno un autore da eliminare.", "Attenzione", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else
            {
                DialogResult dr = MessageBox.Show("Vuoi eliminare gli autori selezionati?", "Conferma", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (dr == DialogResult.Yes)
                {
                    string errore = "";
                    bool errMostrato = false;
                    int eliminati = 0;

                    ListViewItem[] lvi = new ListViewItem[lvAutori.SelectedItems.Count];
                    lvAutori.SelectedItems.CopyTo(lvi, 0);

                    for(int i =0;  i < lvi.Length;i++)
                    {
                        if (string.IsNullOrEmpty(errore))
                        {
                            ClsAutore a = (ClsAutore)lvi[i].Tag;
                            long esito = ClsAutoreBL.Delete(ref Program.conn, a.ID, out errore);
                            if (string.IsNullOrEmpty(errore) && esito > 0)
                                eliminati++;
                        }
                        else
                        {
                            if (!errMostrato)
                            {
                                MessageBox.Show("Errore: " + errore, "Errore", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                errMostrato = true;
                            }
                        }
                    }

                    MessageBox.Show(eliminati + " autore/i eliminato/i.", "Successo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    CaricaAutori();
                }
            }
        }

        private void btnAnnulla_Click(object sender, EventArgs e)
        {
            ResetCampi();
        }

        private void tbFiltroNome_TextChanged(object sender, EventArgs e)
        {
            ApplicaFiltri();
        }

        private void ApplicaFiltri()
        {
            List<ClsAutore> risultato = _autori;

            string cognomeFiltro = tbFiltroNome.Text.Trim();
            if (!string.IsNullOrEmpty(cognomeFiltro))
            {
                string errore;
                risultato = ClsAutoreBL.GetByCognome(ref Program.conn, cognomeFiltro, out errore);
            }

            if ((eFILTRO)cbVerificato.SelectedItem == eFILTRO.Verificati)
                risultato = risultato.FindAll(a => a.ÈVerificato);
            else if ((eFILTRO)cbVerificato.SelectedItem == eFILTRO.Non_Verificati)
                risultato = risultato.FindAll(a => !a.ÈVerificato);

            PopolaListView(risultato);
        }

        private void cbVerificato_SelectedIndexChanged(object sender, EventArgs e)
        {
            ApplicaFiltri();
        }

        private void FrmAutori_FormClosing(object sender, FormClosingEventArgs e)
        {
            Program._chiudiForm = false;
        }
        
        private void chkAttivaDataMorte_CheckedChanged(object sender, EventArgs e)
        {
            if (!chkAttivaDataMorte.Checked)
                dtpDataMorte.Visible = false;
            else
                dtpDataMorte.Visible = true;
        }
    }
}
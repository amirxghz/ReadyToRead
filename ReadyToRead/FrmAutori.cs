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
        List<ClsAutore> _autori = new List<ClsAutore>();
        bool _modalitaModifica = false;
        ClsAutore _autoreSelezionato = null;
        enum eFILTRO
        {
            Tutti,
            Verificati,
            Non_Verificati        
        }

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
            cbVerificato.DataSource= Enum.GetValues(typeof(eFILTRO));
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
            for (int i =0; i < autori.Count;i++)
            {
                ClsAutore a = autori[i];
                ListViewItem lvi = new ListViewItem(a.ÈVerificato ? "✔" : "✘");
                lvi.SubItems.Add(a.Nome + " " + a.Cognome);
                lvi.SubItems.Add(a.Username);
                lvi.SubItems.Add(a.Password);
                lvi.Tag = a;
                lvAutori.Items.Add(lvi);
            }
        }
        private void ResetCampi()
        {
            tbNome.Clear();
            tbCognome.Clear();
            rbVerificato.Checked = false;
            rbNonVerificato.Checked = false;
            rbM.Checked = false;
            rbF.Checked = false;
            dtmDataDiNascita.Value = DateTime.Now;
            tbCittà.Clear();
            _modalitaModifica = false;
            _autoreSelezionato = null;
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
            autore.ÈVerificato = rbVerificato.Checked;
            autore.DataDiNascita = dtmDataDiNascita.Value;
            autore.Sesso = rbM.Checked ? ClsUtente.eSESSO.m : ClsUtente.eSESSO.f;
            autore.CittaNascita = tbCittà.Text.Trim();
            return autore;
        }

        private void btnAggiungi_Click(object sender, EventArgs e)
        {
            GestisciAutore(_modalitaModifica);
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
                    long esito = ClsAutoreBL.Update(ref Program.conn, _autoreSelezionato.UtenteID, autore, out errore);
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


        private void btnVisualizza_Click(object sender, EventArgs e)
        {
            if (lvAutori.SelectedItems.Count == 0)
                MessageBox.Show("Seleziona un autore da visualizzare.", "Attenzione", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else
            {
                _autoreSelezionato = (ClsAutore)lvAutori.SelectedItems[0].Tag;
                tbNome.Text = _autoreSelezionato.Nome;
                tbCognome.Text = _autoreSelezionato.Cognome;
                rbVerificato.Checked = _autoreSelezionato.ÈVerificato;
                rbNonVerificato.Checked = !_autoreSelezionato.ÈVerificato;
                dtmDataDiNascita.Value = _autoreSelezionato.DataDiNascita;
                rbM.Checked = _autoreSelezionato.Sesso == ClsUtente.eSESSO.m;
                rbF.Checked = _autoreSelezionato.Sesso == ClsUtente.eSESSO.f;
                tbCittà.Text = _autoreSelezionato.CittaNascita;
            }
        }

        private void btnModifica_Click(object sender, EventArgs e)
        {
            if (lvAutori.SelectedItems.Count == 0)
                MessageBox.Show("Seleziona un autore da modificare.", "Attenzione", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else
            {
                btnVisualizza_Click(sender, e);
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
                DialogResult dr = MessageBox.Show("Vuoi eliminare gli autori selezionati?","Conferma",MessageBoxButtons.YesNo,MessageBoxIcon.Question);

                if (dr == DialogResult.Yes)
                {
                    string errore = "";
                    bool erroreMostrato = false;
                    int eliminati = 0;

                    ListViewItem[] lvi = new ListViewItem[lvAutori.SelectedItems.Count];
                    lvAutori.SelectedItems.CopyTo(lvi, 0);

                    for (int i = 0; i < lvi.Length; i++)
                    {
                        if (string.IsNullOrEmpty(errore))
                        {
                            ClsAutore a = (ClsAutore)lvi[i].Tag;
                            long esito = ClsAutoreBL.Delete(ref Program.conn, a.UtenteID, out errore);
                            if (string.IsNullOrEmpty(errore) && esito > 0)
                                eliminati++;
                        }
                        else
                        {
                            if (!erroreMostrato)
                            {
                                MessageBox.Show("Errore: " + errore, "Errore", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                erroreMostrato = true;
                            }
                        }
                    }

                    MessageBox.Show($"{eliminati} autore/i eliminato/i.", "Successo", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
            string testo = tbFiltroNome.Text.Trim();
            if (string.IsNullOrEmpty(testo))
                PopolaListView(_autori);
            else
            {
                string errore;
                List<ClsAutore> filtrati = ClsAutoreBL.GetByCognome(ref Program.conn, testo, out errore);
                if (!string.IsNullOrEmpty(errore))
                    MessageBox.Show("Errore: " + errore, "Errore", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else
                    PopolaListView(filtrati);
            }
        }

        private void cbVerificato_SelectedIndexChanged(object sender, EventArgs e)
        {
            string errore=String.Empty;
            List<ClsAutore> filtrati = null;
            if ((eFILTRO)cbVerificato.SelectedItem== eFILTRO.Tutti)
                filtrati = _autori;
            else if ((eFILTRO)cbVerificato.SelectedItem == eFILTRO.Verificati)
                filtrati = ClsAutoreBL.GetVerificati(ref Program.conn, out errore);
            else //eFILTRO.Non_Verificati
            {
                List<ClsAutore> verificati = ClsAutoreBL.GetVerificati(ref Program.conn, out errore);

                if (string.IsNullOrEmpty(errore))
                {
                    filtrati = new List<ClsAutore>();
                    for (int i = 0; i < _autori.Count; i++)
                    {
                        bool trovato = false;

                        for (int j = 0; j < verificati.Count && !trovato; j++)
                        {
                            if (_autori[i].UtenteID == verificati[j].UtenteID)
                                trovato = true;
                        }

                        if (!trovato)
                            filtrati.Add(_autori[i]);
                    }
                }
            }

            if (!string.IsNullOrEmpty(errore))
                MessageBox.Show("Errore: " + errore, "Errore", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
                PopolaListView(filtrati);
        }

        private void FrmAutori_FormClosing(object sender, FormClosingEventArgs e)
        {
            Program._chiudiForm = false;
        }

        private void rbF_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}

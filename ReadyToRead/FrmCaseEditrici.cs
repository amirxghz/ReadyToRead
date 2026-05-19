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
    public partial class FrmCaseEditrici : Form //Amir
    {
        private List<ClsCasa> _case = new List<ClsCasa>();
        private bool _modalitaModifica = false;
        private ClsCasa _casaSelezionata = null;
        private long _idSelezionato = -1;

        public FrmCaseEditrici()
        {
            InitializeComponent();
        }

        private void FrmCaseEditrici_Load(object sender, EventArgs e)
        {
            PopolaComboBox();
            CaricaCase();
        }

        private void PopolaComboBox()
        {
            cbTipoAzienda.DataSource = Enum.GetValues(typeof(ClsCasa.eTIPO_AZIENDA));
        }

        private void CaricaCase()
        {
            string errore;
            _case = ClsCasaBL.GetAll(ref Program.conn, out errore);

            if (!string.IsNullOrEmpty(errore))
                MessageBox.Show("Errore: " + errore, "Errore", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
                PopolaListView(_case);
        }

        private void PopolaListView(List<ClsCasa> case_)
        {
            lvCase.Items.Clear();
            int i = 0;
            while (i < case_.Count)
            {
                ClsCasa c = case_[i];
                ListViewItem lvi = new ListViewItem(c.RagioneSociale);
                lvi.SubItems.Add(c.IndirizzoSedeLegale);
                lvi.SubItems.Add(c.IndirizzoSedeOperativa);
                lvi.SubItems.Add(c.TipoAzienda.ToString());
                lvi.SubItems.Add(c.Esclusiva ? "Sì" : "No");
                lvi.Tag = c;
                lvCase.Items.Add(lvi);
                i++;
            }
        }

        private void ResetCampi()
        {
            tbUsername.Clear();
            tbPassword.Clear();
            tbRagioneSociale.Clear();
            tbSedeLegale.Clear();
            tbSedeOperativa.Clear();
            tbEmail.Clear();
            btnSi.Checked = false;
            btnNo.Checked = false;
            cbTipoAzienda.SelectedIndex = -1;
            _modalitaModifica = false;
            _casaSelezionata = null;
            _idSelezionato = -1;
            lblDomanda.Text = "Crea Casa Editrice";
            btnAggiungi.Text = "➕Aggiungi";

            if (Program._chiudiForm)
                this.Close();
        }

        private ClsCasa LeggiCampi()
        {
            ClsCasa casa = new ClsCasa();
            casa.RagioneSociale = tbRagioneSociale.Text.Trim();
            casa.IndirizzoSedeLegale = tbSedeLegale.Text.Trim();
            casa.IndirizzoSedeOperativa = tbSedeOperativa.Text.Trim();
            casa.Esclusiva = btnSi.Checked;
            if (cbTipoAzienda.SelectedItem != null)
                casa.TipoAzienda = (ClsCasa.eTIPO_AZIENDA)cbTipoAzienda.SelectedItem;
            casa.Tipologia = ClsCasa.eTIPO_CASA.editrice;
            casa.Username = tbUsername.Text.Trim();
            casa.Password = tbPassword.Text.Trim();
            casa.Email = tbEmail.Text.Trim();
            return casa;
        }

        private void GestisciCasa(bool modificaCasa)
        {
            ClsCasa casa = LeggiCampi();
            string errore;

            if (!modificaCasa)
            {
                if (string.IsNullOrWhiteSpace(casa.Password) || string.IsNullOrWhiteSpace(casa.Username) || string.IsNullOrWhiteSpace(casa.RagioneSociale))
                    MessageBox.Show("Compilare tutt i campi obbligatori", "Attenzione", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                else
                {
                    long id = ClsCasaBL.Create(ref Program.conn, casa, out errore);
                    if (!string.IsNullOrEmpty(errore))
                        MessageBox.Show("Errore: " + errore, "Errore", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    else if (id > 0)
                    {
                        MessageBox.Show("Casa editrice aggiunta con successo!", "Successo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ResetCampi();
                        CaricaCase();
                    }
                }
            }
            else
            {
                if (_casaSelezionata == null)
                    MessageBox.Show("Seleziona una casa editrice da modificare.", "Attenzione", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                else
                {
                    long esito = ClsCasaBL.Update(ref Program.conn, _idSelezionato, casa, out errore);
                    if (!string.IsNullOrEmpty(errore))
                        MessageBox.Show("Errore: " + errore, "Errore", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    else if (esito > 0)
                    {
                        MessageBox.Show("Casa editrice modificata con successo!", "Successo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ResetCampi();
                        CaricaCase();
                    }
                }
            }

            if (Program._chiudiForm)
                this.Close();
        }
        bool modalitàVisualizza = false;
        private void btnVisualizza_Click(object sender, EventArgs e)
        {
            if (lvCase.SelectedItems.Count == 0)
                MessageBox.Show("Seleziona una casa editrice da visualizzare.", "Attenzione", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else
                ModificaBtnVisualizza();
        }

        private void ModificaBtnVisualizza()
        {
            modalitàVisualizza = !modalitàVisualizza;
            if (modalitàVisualizza)
            {
                VisualizzaCasa();
                btnAggiungi.Visible = false;
                btnAnnulla.Visible = false;
                btnVisualizza.ForeColor = Color.DodgerBlue;
                btnVisualizza.Text = "👁️Smetti";
            }
            else
            {
                ResetCampi();
                btnAggiungi.Visible = true;
                btnAnnulla.Visible = true;
                btnVisualizza.ForeColor = Color.Black;
                btnVisualizza.Text = "👁️Visualizza";
            }
        }
        private void VisualizzaCasa()
        {
            _casaSelezionata = (ClsCasa)lvCase.SelectedItems[0].Tag;
            _idSelezionato = _casaSelezionata.ID;
            tbRagioneSociale.Text = _casaSelezionata.RagioneSociale;
            btnSi.Checked = _casaSelezionata.Esclusiva;
            btnNo.Checked = !_casaSelezionata.Esclusiva;
            cbTipoAzienda.SelectedItem = _casaSelezionata.TipoAzienda;
            tbPassword.Text = _casaSelezionata.Password;
            tbUsername.Text = _casaSelezionata.Username;
            tbEmail.Text = _casaSelezionata.Email;
        }       

        private void btnModifica_Click(object sender, EventArgs e)
        {
            if (lvCase.SelectedItems.Count == 0)
                MessageBox.Show("Seleziona una casa editrice da modificare.", "Attenzione", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else
            {
                VisualizzaCasa();
                _modalitaModifica = true;
                lblDomanda.Text = "Modifica Casa Editrice";
                btnAggiungi.Text = "☑️ Salva";
            }
        }

        private void btnElimina_Click(object sender, EventArgs e)
        {
            if (lvCase.SelectedItems.Count == 0)
                MessageBox.Show("Seleziona una casa editrice da eliminare.", "Attenzione", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else
            {
                DialogResult dr = MessageBox.Show("Vuoi eliminare le case editrici selezionate?", "Conferma", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (dr == DialogResult.Yes)
                {
                    string errore = "";
                    bool errMostrato = false;
                    int eliminate = 0;

                    ListViewItem[] lvi = new ListViewItem[lvCase.SelectedItems.Count];
                    lvCase.SelectedItems.CopyTo(lvi, 0);

                    int i = 0;
                    while (i < lvi.Length)
                    {
                        if (string.IsNullOrEmpty(errore))
                        {
                            ClsCasa c = (ClsCasa)lvi[i].Tag;
                            long esito = ClsCasaBL.Delete(ref Program.conn, c.ID, out errore);
                            if (string.IsNullOrEmpty(errore) && esito > 0)
                                eliminate++;
                        }
                        else
                        {
                            if (!errMostrato)
                            {
                                MessageBox.Show("Errore: " + errore, "Errore", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                errMostrato = true;
                            }
                        }
                        i++;
                    }

                    MessageBox.Show(eliminate + " case editrice/i eliminata/e.", "Successo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    CaricaCase();
                }
            }
        }

        private void tbFiltroNome_TextChanged(object sender, EventArgs e)
        {
            string testo = tbFiltroNome.Text.Trim();
            if (string.IsNullOrEmpty(testo))
                PopolaListView(_case);
            else
            {
                string errore;
                List<ClsCasa> filtrati = ClsCasaBL.GetByRagioneSociale(ref Program.conn, testo, out errore);
                if (!string.IsNullOrEmpty(errore))
                    MessageBox.Show("Errore: " + errore, "Errore", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else
                    PopolaListView(filtrati);
            }
        }

        private void btnSalva_Click(object sender, EventArgs e)
        {
            GestisciCasa(_modalitaModifica);
        }

        private void btnAnnulla_Click(object sender, EventArgs e)
        {
            ResetCampi();
        }

        private void FrmCaseEditrici_FormClosing(object sender, FormClosingEventArgs e)
        {
            Program._chiudiForm = false;
        }
    }
}
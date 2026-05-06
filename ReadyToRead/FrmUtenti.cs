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
    public partial class FrmUtenti : Form
    {
        List<ClsUtente> _utenti = new List<ClsUtente>();
        bool _modalitaModifica = false;
        ClsUtente _utenteSelezionato = null;
        long _idSelezionato = -1;

        public FrmUtenti()
        {
            InitializeComponent();
        }

        private void FrmUtenti_Load(object sender, EventArgs e)
        {
            PopolaComboBox();
            CaricaUtenti();
        }
        private void PopolaComboBox()
        {
            cbComuneNascita.DataSource = Enum.GetValues(typeof(ClsUtente.eCOMUNE));
        }

        private void CaricaUtenti()
        {
            string errore;
            _utenti = ClsUtenteBL.GetAll(ref Program.conn, out errore);

            if (!string.IsNullOrEmpty(errore))
                MessageBox.Show("Errore: " + errore, "Errore", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
                PopolaListView(_utenti);
        }

        private void PopolaListView(List<ClsUtente> utenti)
        {
            lvUtenti.Items.Clear();
            for (int i=0;  i < utenti.Count;i++)
            {
                ClsUtente u = utenti[i];
                ListViewItem lvi = new ListViewItem(rbAdmin.Checked ? "Admin" : "Cliente");
                lvi.SubItems.Add(u.Nome + " " + u.Cognome);
                lvi.SubItems.Add(u.Username);
                lvi.SubItems.Add(u.Email);
                lvi.Tag = u;
                lvUtenti.Items.Add(lvi);
            }
        }

        private void ResetCampi()
        {
            tbNome.Clear();
            tbCognome.Clear();
            tbUsername.Clear();
            tbPassword.Clear();
            tbEmail.Clear();
            tbCF.Clear();
            rbM.Checked = false;
            rbF.Checked = false;
            rbAdmin.Checked = false;
            rbCliente.Checked = false;
            dtpDataDiNascita.Value = DateTime.Now;
            cbComuneNascita.SelectedIndex = -1;
            _modalitaModifica = false;
            _utenteSelezionato = null;
            _idSelezionato = -1;
            lblTitolo.Text = "Crea Utente";
            btnAggiungi.Text = "➕Aggiungi";
        }

        private ClsUtente LeggiCampi()
        {
            ClsUtente utente = new ClsUtente();
            utente.Nome = tbNome.Text.Trim();
            utente.Cognome = tbCognome.Text.Trim();
            utente.Username = tbUsername.Text.Trim();
            utente.Password = tbPassword.Text.Trim();
            utente.Email = tbEmail.Text.Trim();
            utente.DataDiNascita = dtpDataDiNascita.Value;
            utente.Sesso = rbM.Checked ? ClsUtente.eSESSO.m : ClsUtente.eSESSO.f;
            if (cbComuneNascita.SelectedItem != null)
                utente.ComuneDiNascita = (ClsUtente.eCOMUNE)cbComuneNascita.SelectedItem;
            return utente;
        }

        private void btnAggiungi_Click(object sender, EventArgs e)
        {
            GestisciUtente(_modalitaModifica);
        }
        private void GestisciUtente(bool modificaUtente)
        {
            ClsUtente utente = LeggiCampi();
            string errore;
            utente.CodiceFiscale = CalcolaCF();

            if (!modificaUtente)
            {
                long id = ClsUtenteBL.Create(ref Program.conn, utente, out errore);
                if (!string.IsNullOrEmpty(errore))
                    MessageBox.Show("Errore: " + errore, "Errore", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else if (id > 0)
                {
                    MessageBox.Show("Utente aggiunto con successo!", "Successo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ResetCampi();
                    CaricaUtenti();
                }
            }
            else
            {
                if (_utenteSelezionato == null)
                    MessageBox.Show("Seleziona un utente da modificare.", "Attenzione", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                else
                {
                    long esito = ClsUtenteBL.Update(ref Program.conn, _idSelezionato, utente, out errore);
                    if (!string.IsNullOrEmpty(errore))
                        MessageBox.Show("Errore: " + errore, "Errore", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    else if (esito > 0)
                    {
                        MessageBox.Show("Utente modificato con successo!", "Successo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ResetCampi();
                        CaricaUtenti();
                    }
                }
            }
        }
        #region Calcolo_CF
        private string CalcolaCF()
        {
            string CF = String.Empty;
            string _codiceFiscaleParziale =
                                            RestituisciPrimeTreConsonantiCognome(tbCognome.Text) +
                                            RestituisciPrimaTerzaQuartaConsonanteNome(tbNome.Text) +
                                            RestituisciUltimeDueCifreAnnoNascita(dtpDataDiNascita.Value.Year) +
                                            RestituisciLetteraMese(dtpDataDiNascita.Value.Month) +
                                            RestituisciNumeroGiornoNascita(dtpDataDiNascita.Value.Day) +
                                            RestituisciCodiceISTAT((ClsUtente.eCOMUNE)cbComuneNascita.SelectedItem);

            char codiceControllo = RestituisciCodiceControllo(_codiceFiscaleParziale);
            CF= _codiceFiscaleParziale + codiceControllo;
            return CF;
        }
        private char RestituisciCodiceControllo(string _codiceFiscaleParziale)
        {
            int[] caratteriDispari = { 1, 0, 5, 7, 9, 13, 15, 17, 19, 21, 1, 0, 5, 7, 9, 13, 15, 17, 19, 21, 2, 4, 18, 20, 11, 3, 6, 8, 12, 14, 16, 10, 22, 25, 24, 23 };
            int[] caratteriPari = { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25 };
            char[] caratteriResto = { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z' };

            char[] caratteriValidi = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z' };

            int resto;
            int somma = 0;

            for (int i = 0; i < _codiceFiscaleParziale.Length; i++)
            {
                char carattereAssociato = _codiceFiscaleParziale[i];
                int j = Array.IndexOf(caratteriValidi, carattereAssociato);

                if ((i + 1) % 2 == 0)
                    somma += caratteriPari[j];
                else
                    somma += caratteriDispari[j];
            }
            resto = somma % 26;
            return caratteriResto[resto];
        }

        private string RestituisciCodiceISTAT(ClsUtente.eCOMUNE comune)
        {
            string codiceISTAT = "";

            switch (comune)
            {
                case ClsUtente.eCOMUNE.ancona:
                    codiceISTAT = "A271";
                    break;

                case ClsUtente.eCOMUNE.jesi:
                    codiceISTAT = "E388";
                    break;

                case ClsUtente.eCOMUNE.chiaravalle:
                    codiceISTAT = "C615";
                    break;

                case ClsUtente.eCOMUNE.senigallia:
                    codiceISTAT = "I608";
                    break;

                case ClsUtente.eCOMUNE.rotella:
                    codiceISTAT = "H588";
                    break;
            }

            return codiceISTAT;
        }

        private string RestituisciNumeroGiornoNascita(int giorno)
        {
            decimal numero = 0;

            if (rbM.Checked)
                numero = giorno;
            else
                numero = giorno + 40;

            return Convert.ToString(numero);
        }

        private string RestituisciLetteraMese(int mese)
        {
            string lettera = "";
            switch (mese)
            {
                case 1:
                    lettera = "A";
                    break;
                case 2:
                    lettera = "B";
                    break;
                case 3:
                    lettera = "C";
                    break;
                case 4:
                    lettera = "D";
                    break;
                case 5:
                    lettera = "E";
                    break;
                case 6:
                    lettera = "H";
                    break;
                case 7:
                    lettera = "L";
                    break;
                case 8:
                    lettera = "M";
                    break;
                case 9:
                    lettera = "P";
                    break;
                case 10:
                    lettera = "R";
                    break;
                case 11:
                    lettera = "S";
                    break;
                case 12:
                    lettera = "T";
                    break;
            }
            return lettera;
        }

        private string RestituisciUltimeDueCifreAnnoNascita(int cifre)
        {
            int valore = 0;
            valore = cifre;

            string ultimeDueCifre = Convert.ToString(valore);
            ultimeDueCifre = ultimeDueCifre.Substring(2, 2);
            return ultimeDueCifre;
        }

        private string RestituisciPrimeTreConsonantiCognome(string frase)
        {
            string _cognome = "";
            int _contatoreConsonanti = 0;
            frase = frase.ToUpper();
            for (int i = 0; _contatoreConsonanti <= 2 && i < frase.Length; i++)
            {
                char _carattereFrase = frase[i];
                if (!(_carattereFrase == 'A' || _carattereFrase == 'E' || _carattereFrase == 'I' || _carattereFrase == 'O' || _carattereFrase == 'U'))
                {
                    _cognome += Convert.ToString(_carattereFrase);
                    _contatoreConsonanti++;
                }
            }
            return _cognome;
        }

        private string RestituisciPrimaTerzaQuartaConsonanteNome(string frase)
        {
            string _nome = "";
            int _contatoreConsonanti = 0;
            frase = frase.ToUpper();

            char[] _arrayFrase = frase.ToCharArray();

            for (int i = 0; _contatoreConsonanti <= 3 && i < _arrayFrase.Length; i++)
            {
                char _carattereFrase = _arrayFrase[i];
                if (_carattereFrase != ' ')
                {
                    if (!"AEIOU".Contains(_carattereFrase))
                    {
                        _contatoreConsonanti++;
                        if (_contatoreConsonanti != 2)
                        {
                            _nome += Convert.ToString(_carattereFrase);
                            _arrayFrase[i] = ' ';
                        }
                    }
                }
            }

            for (int i = 0; _contatoreConsonanti <= 3 && i < _arrayFrase.Length; i++)
            {
                char _carattereFrase = _arrayFrase[i];
                if (_carattereFrase != ' ')
                {
                    if (!"AEIOU".Contains(_carattereFrase))
                    {
                        _contatoreConsonanti++;
                        _nome += Convert.ToString(_carattereFrase);
                        _arrayFrase[i] = ' ';
                    }
                }
            }

            for (int i = 0; _contatoreConsonanti <= 3 && i < _arrayFrase.Length; i++)
            {
                char _carattereFrase = _arrayFrase[i];
                if (_carattereFrase != ' ')
                {
                    if ("AEIOU".Contains(_carattereFrase))
                    {
                        _contatoreConsonanti++;
                        _nome += Convert.ToString(_carattereFrase);
                        _arrayFrase[i] = ' ';
                    }
                }
            }

            _nome = _nome.PadRight(3, 'X');

            return _nome;
        }
        #endregion
        private void btnAnnulla_Click(object sender, EventArgs e)
        {
            ResetCampi();
        }

        private void btnAutore_Click(object sender, EventArgs e)
        {
            Program._chiudiForm = true;
            FrmAutori frmAutori = new FrmAutori();
            frmAutori.ShowDialog();
        }

        private void btnCasaEditrice_Click(object sender, EventArgs e)
        {
            Program._chiudiForm = true;
            FrmCaseEditrici frmCase = new FrmCaseEditrici();
            frmCase.ShowDialog();
        }

        private void btnVisualizza_Click(object sender, EventArgs e)
        {
            VisualizzaUtente();
        }

        private void VisualizzaUtente()
        {
            if (lvUtenti.SelectedItems.Count == 0)
                MessageBox.Show("Seleziona un utente da visualizzare.", "Attenzione", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else
            {
                _utenteSelezionato = (ClsUtente)lvUtenti.SelectedItems[0].Tag;
                _idSelezionato = ClsUtente.ID;
                tbNome.Text = _utenteSelezionato.Nome;
                tbCognome.Text = _utenteSelezionato.Cognome;
                tbUsername.Text = _utenteSelezionato.Username;
                tbPassword.Text = _password;
                tbEmail.Text = _utenteSelezionato.Email;
                dtpDataDiNascita.Value = _utenteSelezionato.DataDiNascita;
                rbM.Checked = _utenteSelezionato.Sesso == ClsUtente.eSESSO.m;
                rbF.Checked = _utenteSelezionato.Sesso == ClsUtente.eSESSO.f;
                cbComuneNascita.SelectedItem = _utenteSelezionato.ComuneDiNascita;
                tbCF.Text = _utenteSelezionato.CodiceFiscale;
            }
        }

        private void btnModifica_Click(object sender, EventArgs e)
        {
            if (lvUtenti.SelectedItems.Count == 0)
                MessageBox.Show("Seleziona un utente da modificare.", "Attenzione", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else
            {
                VisualizzaUtente();
                _modalitaModifica = true;
                lblTitolo.Text = "Modifica Utente";
                btnAggiungi.Text = "☑️ Salva";
            }
        }

        private void btnElimina_Click(object sender, EventArgs e)
        {
            if (lvUtenti.SelectedItems.Count == 0)
                MessageBox.Show("Seleziona un utente da eliminare.", "Attenzione", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else
            {
                DialogResult dr = MessageBox.Show("Vuoi eliminare l'utente selezionato?", "Conferma", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dr == DialogResult.Yes)
                {
                    ClsUtente u = (ClsUtente)lvUtenti.SelectedItems[0].Tag;
                    string errore;
                    long esito = ClsUtenteBL.Delete(ref Program.conn, _idSelezionato, out errore);
                    if (!string.IsNullOrEmpty(errore))
                        MessageBox.Show("Errore: " + errore, "Errore", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    else if (esito > 0)
                    {
                        MessageBox.Show("Utente eliminato.", "Successo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        CaricaUtenti();
                    }
                }
            }

        }

        private void tbFiltroNome_TextChanged(object sender, EventArgs e)
        {
            string testo = tbFiltroNome.Text.Trim();
            if (string.IsNullOrEmpty(testo))
                PopolaListView(_utenti);
            else
            {
                string errore;
                List<ClsUtente> filtrati = ClsUtenteBL.GetByUsername(ref Program.conn, testo, out errore);
                if (!string.IsNullOrEmpty(errore))
                    MessageBox.Show("Errore: " + errore, "Errore", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else
                    PopolaListView(filtrati);
            }
        }
        
        string _password = String.Empty;
        private void tbPassword_TextChanged(object sender, EventArgs e)
        {
            _password = tbPassword.Text.Trim();
        }

        private void btnVisualizzaPassword_MouseUp(object sender, MouseEventArgs e)
        {
            btnVisualizzaPassword.ForeColor = Color.Black;
            tbPassword.UseSystemPasswordChar = true;
        }

        private void btnVisualizzaPassword_MouseDown(object sender, MouseEventArgs e)
        {
            btnVisualizzaPassword.ForeColor = Color.DodgerBlue;
            tbPassword.UseSystemPasswordChar = false;
        }
    }
}

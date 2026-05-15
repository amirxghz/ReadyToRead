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
    public partial class FrmUtenti : Form //Amir
    {
        private List<ClsUtente> _utenti = new List<ClsUtente>();
        private bool _modalitaModifica = false;
        private ClsUtente _utenteSelezionato = null;
        private long _idSelezionato = -1;  
        private string _password = string.Empty;

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
            cbComuneNascita.Items.Clear();
            cbComuneNascita.DataSource = Enum.GetValues(typeof(ClsUtente.eCOMUNE));
            cbComuneNascita.SelectedIndex = -1;
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
            for  (int i =0;  i < utenti.Count; i++)
            {
                ClsUtente u = utenti[i];
                ListViewItem lvi = new ListViewItem(u is ClsAdmin? "Admin" : "Cliente");
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
            _password = string.Empty;
            lblTitolo.Text = "Crea Utente";
            btnAggiungi.Text = "➕Aggiungi";
        }
        
        private ClsUtente LeggiCampi()
        {
            ClsUtente utente = new ClsUtente();
            if(rbAdmin.Checked)
            {
                ClsAdmin admin = new ClsAdmin();
                admin.Nome = tbNome.Text.Trim();
                admin.Cognome = tbCognome.Text.Trim();
                admin.Username = tbUsername.Text.Trim();
                admin.Password = tbPassword.Text.Trim();
                admin.Email = tbEmail.Text.Trim();
                admin.DataDiNascita = dtpDataDiNascita.Value;
                admin.Sesso = rbM.Checked ? ClsUtente.eSESSO.m : ClsUtente.eSESSO.f;
                if (cbComuneNascita.SelectedItem != null)
                    admin.ComuneDiNascita = (ClsUtente.eCOMUNE)cbComuneNascita.SelectedItem;

                utente = admin;
            }
            else
            {
                ClsCliente cliente = new ClsCliente();
                cliente.Nome = tbNome.Text.Trim();
                cliente.Cognome = tbCognome.Text.Trim();
                cliente.Username = tbUsername.Text.Trim();
                cliente.Password = tbPassword.Text.Trim();
                cliente.Email = tbEmail.Text.Trim();
                cliente.DataDiNascita = dtpDataDiNascita.Value;
                cliente.Sesso = rbM.Checked ? ClsUtente.eSESSO.m : ClsUtente.eSESSO.f;
                if (cbComuneNascita.SelectedItem != null)
                    cliente.ComuneDiNascita = (ClsUtente.eCOMUNE)cbComuneNascita.SelectedItem;

                utente = cliente;
            }
            return utente;
        }

        private void GestisciUtente(bool modificaUtente)
        {
            ClsUtente utente = LeggiCampi();
            string errore;
            if ((ClsUtente.eCOMUNE)cbComuneNascita.SelectedItem != ClsUtente.eCOMUNE.Nessuno)
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
        
        private void VisualizzaUtente()
        {
            if (lvUtenti.SelectedItems.Count == 0)
                MessageBox.Show("Seleziona un utente da visualizzare.", "Attenzione", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else
            {
                _utenteSelezionato = (ClsUtente)lvUtenti.SelectedItems[0].Tag;
                _idSelezionato = _utenteSelezionato.ID; 
                tbNome.Text = _utenteSelezionato.Nome;
                tbCognome.Text = _utenteSelezionato.Cognome;
                tbUsername.Text = _utenteSelezionato.Username;
                tbPassword.Text = _password;               
                tbEmail.Text = _utenteSelezionato.Email;
                dtpDataDiNascita.Value = _utenteSelezionato.DataDiNascita;
                rbM.Checked = _utenteSelezionato.Sesso == ClsUtente.eSESSO.m;
                rbF.Checked = _utenteSelezionato.Sesso == ClsUtente.eSESSO.f;
                cbComuneNascita.SelectedItem = _utenteSelezionato.ComuneDiNascita;
                tbCF.Text = CalcolaCF();
            }
        }
        
        #region Calcolo_CF
        private string CalcolaCF()
        {
            string CF = string.Empty;
            if (cbComuneNascita.SelectedItem == null)
                return CF;

            string _codiceFiscaleParziale =
                RestituisciPrimeTreConsonantiCognome(tbCognome.Text) +
                RestituisciPrimaTerzaQuartaConsonanteNome(tbNome.Text) +
                RestituisciUltimeDueCifreAnnoNascita(dtpDataDiNascita.Value.Year) +
                RestituisciLetteraMese(dtpDataDiNascita.Value.Month) +
                RestituisciNumeroGiornoNascita(dtpDataDiNascita.Value.Day) +
                RestituisciCodiceISTAT((ClsUtente.eCOMUNE)cbComuneNascita.SelectedItem);

            char codiceControllo = RestituisciCodiceControllo(_codiceFiscaleParziale);
            CF = _codiceFiscaleParziale + codiceControllo;
            return CF;
        }

        private char RestituisciCodiceControllo(string parziale)
        {
            int[] dispari = { 1, 0, 5, 7, 9, 13, 15, 17, 19, 21, 1, 0, 5, 7, 9, 13, 15, 17, 19, 21, 2, 4, 18, 20, 11, 3, 6, 8, 12, 14, 16, 10, 22, 25, 24, 23 };
            int[] pari = { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25 };
            char[] resto = { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z' };
            char[] validi = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z' };

            int somma = 0;
            int i = 0;
            while (i < parziale.Length)
            {
                int j = Array.IndexOf(validi, parziale[i]);
                if ((i + 1) % 2 == 0)
                    somma += pari[j];
                else
                    somma += dispari[j];
                i++;
            }
            return resto[somma % 26];
        }

        private string RestituisciCodiceISTAT(ClsUtente.eCOMUNE comune)
        {
            string codice = "";
            if (comune == ClsUtente.eCOMUNE.ancona) codice = "A271";
            else if (comune == ClsUtente.eCOMUNE.jesi) codice = "E388";
            else if (comune == ClsUtente.eCOMUNE.chiaravalle) codice = "C615";
            else if (comune == ClsUtente.eCOMUNE.senigallia) codice = "I608";
            else if (comune == ClsUtente.eCOMUNE.rotella) codice = "H588";
            return codice;
        }

        private string RestituisciNumeroGiornoNascita(int giorno)
        {
            decimal numero = rbM.Checked ? giorno : giorno + 40;
            string s = Convert.ToString(numero);
            return s.PadLeft(2, '0');
        }

        private string RestituisciLetteraMese(int mese)
        {
            string[] lettere = { "", "A", "B", "C", "D", "E", "H", "L", "M", "P", "R", "S", "T" };
            return lettere[mese];
        }

        private string RestituisciUltimeDueCifreAnnoNascita(int anno)
        {
            string s = Convert.ToString(anno);
            return s.Substring(s.Length - 2, 2);
        }

        private string RestituisciPrimeTreConsonantiCognome(string frase)
        {
            string risultato = "";
            int contatore = 0;
            frase = frase.ToUpper();
            int i = 0;
            while (contatore < 3 && i < frase.Length)
            {
                char c = frase[i];
                if (c != 'A' && c != 'E' && c != 'I' && c != 'O' && c != 'U' && char.IsLetter(c))
                {
                    risultato += c;
                    contatore++;
                }
                i++;
            }
            return risultato.PadRight(3, 'X');
        }

        private string RestituisciPrimaTerzaQuartaConsonanteNome(string frase)
        {
            string risultato = "";
            int contatore = 0;
            frase = frase.ToUpper();
            char[] arr = frase.ToCharArray();

            int i = 0;
            while (contatore <= 3 && i < arr.Length)
            {
                char c = arr[i];
                if (c != ' ' && !"AEIOU".Contains(c) && char.IsLetter(c))
                {
                    contatore++;
                    if (contatore != 2)
                    {
                        risultato += c;
                        arr[i] = ' ';
                    }
                }
                i++;
            }

            i = 0;
            while (contatore <= 3 && i < arr.Length)
            {
                char c = arr[i];
                if (c != ' ' && !"AEIOU".Contains(c) && char.IsLetter(c))
                {
                    contatore++;
                    risultato += c;
                    arr[i] = ' ';
                }
                i++;
            }

            i = 0;
            while (contatore <= 3 && i < arr.Length)
            {
                char c = arr[i];
                if (c != ' ' && "AEIOU".Contains(c))
                {
                    contatore++;
                    risultato += c;
                    arr[i] = ' ';
                }
                i++;
            }

            return risultato.PadRight(3, 'X');
        }
        #endregion
        private void btnAggiungi_Click(object sender, EventArgs e)
        {
            GestisciUtente(_modalitaModifica);
        }

        private void btnAnnulla_Click(object sender, EventArgs e)
        {
            ResetCampi();
        }

        private void btnVisualizza_Click(object sender, EventArgs e)
        {
            VisualizzaUtente();
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
                    long esito = ClsUtenteBL.Delete(ref Program.conn, u.ID, out errore);
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
            {
                PopolaListView(_utenti);
            }
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

        private void tbPassword_TextChanged(object sender, EventArgs e)
        {
            _password = tbPassword.Text.Trim();
        }

        private void btnVisualizzaPassword_MouseDown(object sender, MouseEventArgs e)
        {
            btnVisualizzaPassword.ForeColor = Color.DodgerBlue;
            tbPassword.UseSystemPasswordChar = false;
        }

        private void btnVisualizzaPassword_MouseUp(object sender, MouseEventArgs e)
        {
            btnVisualizzaPassword.ForeColor = Color.Black;
            tbPassword.UseSystemPasswordChar = true;
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
    }
}
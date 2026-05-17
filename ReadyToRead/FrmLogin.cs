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
    public partial class FrmLogin : Form
    {
        public FrmLogin()
        {
            InitializeComponent();
        }

        private void FrmLogin_Load(object sender, EventArgs e)
        {
            try
            {
                Program.conn.Open();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Errore connessione DB: " + ex.Message, "Errore", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
        }

        private void llblAccedi_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            pnlRegistrati.Visible = false;
            pnlAccesso.Visible = true;
        }

        private void llblRegistrati_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            pnlRegistrati.Visible = true;
            pnlAccesso.Visible = false;
        }

        private void pbFotoProfilo_MouseHover(object sender, EventArgs e)
        {
            pbFotoProfilo.Image = Properties.Resources.caricaPfp;
        }

        private void pbFotoProfilo_MouseLeave(object sender, EventArgs e)
        {
            pbFotoProfilo.Image = Program._fotoProfilo;
        }

        private void pbFotoProfilo_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "File Immagine|*.jpg;*.jpeg;*.png";
                ofd.Title = "Seleziona una foto profilo(solo jpg, jpeg e png)";

                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        Program._fotoProfilo = Image.FromFile(ofd.FileName);
                        pbFotoProfilo.Image = Program._fotoProfilo;
                        pbFotoProfilo.Tag = ofd.FileName;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Errore nel caricamento dell'immagine: " + ex.Message, "Errore", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void btnRegistrati_Click(object sender, EventArgs e)
        {
            ClsCliente _cliente = new ClsCliente();
            _cliente.Nome = tbNome.Text;
            _cliente.Cognome = tbCognome.Text;
            _cliente.DataDiNascita = dtmDataDiNascita.Value;
            _cliente.Email = tbEmail.Text;
            if(pbFotoProfilo.Tag != null)
                _cliente.Foto_profilo = pbFotoProfilo.Tag.ToString();
            _cliente.Username = tbUsername.Text;
            _cliente.Password = tbPassword.Text;
            string _errore;
            ClsClienteBL.Create(ref Program.conn, _cliente, out _errore);
            MessageBox.Show(_errore);
            FrmMain frmMain = new FrmMain();
            frmMain.ShowDialog();
            this.Close();
        }

        private void btnAccedi_Click(object sender, EventArgs e)
        {
            ClsUtente _utente = new ClsUtente();
            string accesso;
            accesso = ClsUtenteBL.Login(ref Program.conn, tbUsernameLog.Text.Trim(), tbPasswordLog.Text.Trim());
            MessageBox.Show(accesso);
            if (accesso.Contains("garantito"))
            {
                if (accesso.Contains("cliente"))
                {
                    FrmMenuAdmin frmMenuAdmin = new FrmMenuAdmin();
                    frmMenuAdmin.ShowDialog();
                }else if(accesso.Contains("admin"))
                {
                    FrmMain frmMain = new FrmMain();
                    frmMain.ShowDialog();
                }
                this.Close();
            }
            if (tbUsernameLog.Text == "" && tbPassword.Text == "")
            {
                FrmMenuAdmin frmMenuAdmin = new FrmMenuAdmin();
                frmMenuAdmin.ShowDialog();
            }
        }

        private void llblInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MessageBox.Show(@"**Termini e Condizioni d'Uso – Ready to Read**

                            Benvenuto su * Ready to Read *, piattaforma e - commerce dedicata alla vendita online di libri in formato fisico e digitale.L’accesso e l’utilizzo del servizio implicano l’accettazione dei seguenti Termini e Condizioni.

                            * *1.Oggetto del servizio * *
                            Ready to Read offre agli utenti la possibilità di acquistare libri, consultare cataloghi editoriali e accedere a contenuti digitali correlati.Il servizio è disponibile tramite sito web e dispositivi compatibili.

                            * *2.Registrazione e account * *
                            Per effettuare acquisti è richiesta la creazione di un account personale.L’utente è responsabile della veridicità dei dati forniti e della sicurezza delle proprie credenziali.

                            * *3.Acquisti e pagamenti * *
                            Tutti i prezzi sono indicati in euro e includono, ove applicabile, l’IVA.I pagamenti possono essere effettuati tramite i metodi disponibili sulla piattaforma.Ready to Read si riserva il diritto di modificare prezzi e disponibilità senza preavviso.

                            * *4.Spedizioni e consegne * *
                            I tempi di consegna variano in base alla destinazione e alla disponibilità dei prodotti.Eventuali ritardi non imputabili alla piattaforma non comportano responsabilità diretta.

                            * *5.Diritto di recesso * *
                            L’utente può esercitare il diritto di recesso entro 14 giorni dalla ricezione del prodotto, salvo eccezioni previste per contenuti digitali già fruiti.

                            * *6.Proprietà intellettuale * *
                            Tutti i contenuti presenti su Ready to Read(testi, immagini, marchi) sono protetti da diritti di proprietà intellettuale e non possono essere utilizzati senza autorizzazione.

                            * *7.Limitazione di responsabilità * *
                            Ready to Read non è responsabile per eventuali danni derivanti da un uso improprio del servizio o da interruzioni tecniche non prevedibili.

                            * *8.Modifiche ai termini * *
                            La piattaforma si riserva il diritto di aggiornare i presenti Termini in qualsiasi momento.Le modifiche entreranno in vigore dalla loro pubblicazione.

                            * *9.Legge applicabile * *
                            I presenti Termini sono regolati dalla legge italiana.Per eventuali controversie è competente il foro del consumatore.

                            Per ulteriori informazioni o assistenza, è possibile contattare il servizio clienti tramite i canali ufficiali della piattaforma.", "Termini e condizioni d'uso", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySqlConnector;

namespace ReadyToRead
{
    public partial class FrmLibri : Form //Amir
    {
        bool _modalitaModifica = false;
        ClsLibro _libroSelezionato = null;
        long _idSelezionato = -1;

        List<ClsAutore> _autori = new List<ClsAutore>();
        List<ClsCasa> _case = new List<ClsCasa>();
        List<ClsGenere> _generi = new List<ClsGenere>();

        Image _coverLibro = Properties.Resources.cover;
        string _pathCopertinaScelto = string.Empty;

        public FrmLibri()
        {
            InitializeComponent();
        }

        private void FrmLibri_Load(object sender, EventArgs e)
        {
            PopolaComboBox();
            CaricaLibri();
        }

        private void PopolaComboBox()
        {
            string erroreA;
            _autori = ClsAutoreBL.GetAll(ref Program.conn, out erroreA);
            if (!string.IsNullOrEmpty(erroreA))
                MessageBox.Show("Errore caricamento autori: " + erroreA, "Errore", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
            {
                cbAutore.DataSource = new List<ClsAutore>(_autori);
                cbAutore.DisplayMember = "Cognome";
                cbAutore.ValueMember = "ID";
                cbAutore.SelectedIndex = -1;
            }

            string erroreC;
            _case = ClsCasaBL.GetAll(ref Program.conn, out erroreC);
            if (!string.IsNullOrEmpty(erroreC))
                MessageBox.Show("Errore caricamento case editrici: " + erroreC, "Errore", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
            {
                cbCasaEditrice.DataSource = new List<ClsCasa>(_case);
                cbCasaEditrice.DisplayMember = "RagioneSociale";
                cbCasaEditrice.ValueMember = "ID";
                cbCasaEditrice.SelectedIndex = -1;
            }

            string erroreG;
            _generi = ClsGenereBL.GetAll(ref Program.conn, out erroreG);
            if (!string.IsNullOrEmpty(erroreG))
                MessageBox.Show("Errore caricamento generi: " + erroreG, "Errore", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
            {
                clbGenere.Items.Clear();
                for (int i = 0; i < _generi.Count; i++)
                    clbGenere.Items.Add(_generi[i]);
                clbGenere.DisplayMember = "Nome";
            }
        }

        private void CaricaLibri()
        {
            string errore;
            Program._libri = ClsLibroBL.GetAll(ref Program.conn, out errore);

            if (!string.IsNullOrEmpty(errore))
                MessageBox.Show("Errore: " + errore, "Errore", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
                PopolaListView(Program._libri);
        }

        private void PopolaListView(List<ClsLibro> libri)
        {
            lvLibri.Items.Clear();

            for (int i = 0; i < libri.Count; i++)
            {
                ClsLibro l = libri[i];

                string nomeAutore = "";
                try
                {
                    if (Program.conn.State != System.Data.ConnectionState.Open)
                        Program.conn.Open();

                    string sqlAutore = @"SELECT u.nome, u.cognome 
                                         FROM scrivere s
                                         INNER JOIN autori a ON s.autoreID = a.ID
                                         INNER JOIN utenti u ON a.utenteID = u.ID
                                         WHERE s.libroISBN = @isbn
                                         LIMIT 1";
                    MySqlConnector.MySqlCommand cmdA = new MySqlConnector.MySqlCommand(sqlAutore, Program.conn);
                    cmdA.Parameters.AddWithValue("@isbn", l.Isbn);
                    using (MySqlConnector.MySqlDataReader rA = cmdA.ExecuteReader())
                    {
                        if (rA.Read())
                            nomeAutore = rA["nome"].ToString() + " " + rA["cognome"].ToString();
                    }
                }
                catch { }
                finally
                {
                    if (Program.conn.State == System.Data.ConnectionState.Open)
                        Program.conn.Close();
                }

                string nomeCasa = "";
                try
                {
                    if (Program.conn.State != System.Data.ConnectionState.Open)
                        Program.conn.Open();

                    string sqlCasa = @"SELECT h.ragione_sociale 
                                       FROM pubblicare p
                                       INNER JOIN houses h ON p.casa_editriceID = h.ID
                                       WHERE p.libroISBN = @isbn
                                       LIMIT 1";
                    MySqlConnector.MySqlCommand cmdC = new MySqlConnector.MySqlCommand(sqlCasa, Program.conn);
                    cmdC.Parameters.AddWithValue("@isbn", l.Isbn);
                    using (MySqlConnector.MySqlDataReader rC = cmdC.ExecuteReader())
                    {
                        if (rC.Read())
                            nomeCasa = rC["ragione_sociale"].ToString();
                    }
                }
                catch { }
                finally
                {
                    if (Program.conn.State == System.Data.ConnectionState.Open)
                        Program.conn.Close();
                }

                string nomeGenere = "";
                try
                {
                    if (Program.conn.State != System.Data.ConnectionState.Open)
                        Program.conn.Open();

                    string sqlGenere = @"SELECT g.nome 
                                         FROM caratterizzare c
                                         INNER JOIN generi g ON c.genereID = g.ID
                                         WHERE c.libroISBN = @isbn
                                         LIMIT 1";
                    MySqlConnector.MySqlCommand cmdG = new MySqlConnector.MySqlCommand(sqlGenere, Program.conn);
                    cmdG.Parameters.AddWithValue("@isbn", l.Isbn);
                    using (MySqlConnector.MySqlDataReader rG = cmdG.ExecuteReader())
                    {
                        if (rG.Read())
                            nomeGenere = rG["nome"].ToString();
                    }
                }
                catch { }
                finally
                {
                    if (Program.conn.State == System.Data.ConnectionState.Open)
                        Program.conn.Close();
                }

                string quantita = "";
                try
                {
                    if (Program.conn.State != System.Data.ConnectionState.Open)
                        Program.conn.Open();

                    string sqlQta = @"SELECT p.quantita 
                                      FROM prodotti p
                                      INNER JOIN libri lb ON lb.prodottoID = p.ID
                                      WHERE lb.isbn = @isbn
                                      LIMIT 1";
                    MySqlConnector.MySqlCommand cmdQ = new MySqlConnector.MySqlCommand(sqlQta, Program.conn);
                    cmdQ.Parameters.AddWithValue("@isbn", l.Isbn);
                    using (MySqlConnector.MySqlDataReader rQ = cmdQ.ExecuteReader())
                    {
                        if (rQ.Read())
                            quantita = rQ["quantita"].ToString();
                        else
                            quantita = l.Quantita.ToString(); 
                    }
                }
                catch
                {
                    quantita = l.Quantita.ToString();
                }
                finally
                {
                    if (Program.conn.State == System.Data.ConnectionState.Open)
                        Program.conn.Close();
                }

                ListViewItem lvi = new ListViewItem(l.Nome);
                lvi.SubItems.Add(nomeAutore);
                lvi.SubItems.Add(nomeCasa);
                lvi.SubItems.Add(nomeGenere);
                lvi.SubItems.Add(quantita);
                lvi.Tag = l;
                lvLibri.Items.Add(lvi);
            }
        }

        private void ResetCampi()
        {
            tbTitolo.Clear();
            tbEdizione.Clear();
            tbLingua.Clear();
            rtbDescrizione.Clear();
            nudPagine.Value = 0;
            nudPrezzo.Value = 0;
            nudQuantita.Value = 0;
            dtpDataProduzione.MaxDate = DateTime.Today;
            cbAutore.SelectedIndex = -1;
            cbCasaEditrice.SelectedIndex = -1;
            for (int i = 0; i < clbGenere.Items.Count; i++)
                clbGenere.SetItemChecked(i, false);
            _coverLibro = Properties.Resources.cover;
            _pathCopertinaScelto = string.Empty;
            pbCoverLibro.Image = _coverLibro;
            tbISBN.Text = string.Empty;
            _modalitaModifica = false;
            _libroSelezionato = null;
            _idSelezionato = -1;
            lblTitolo.Text = "Crea Libro";
            btnAggiungi.Text = "➕Aggiungi";
            btnAssegnaEbook.Visible = false;
        }

        private ClsLibro LeggiCampi()
        {
            ClsLibro libro = new ClsLibro();
            libro.Nome = tbTitolo.Text.Trim();
            libro.Edizione = tbEdizione.Text.Trim();
            libro.Lingua = tbLingua.Text.Trim();
            libro.Sinossi = rtbDescrizione.Text.Trim();
            libro.NumeroPagine = (int)nudPagine.Value;
            libro.Prezzo = (float)nudPrezzo.Value;
            libro.AnnoPubblicazione = dtpDataProduzione.Value;
            libro.Quantita = (int)nudQuantita.Value;
            libro.Isbn = tbISBN.Text.Trim();

            if (_pathCopertinaScelto != string.Empty)
                libro.ImgCopertina = _pathCopertinaScelto;

            return libro;
        }
        
        private void GestisciLibro(bool modificaLibro)
        {
            if (string.IsNullOrWhiteSpace(tbTitolo.Text) || cbAutore.SelectedIndex == -1 || cbCasaEditrice.SelectedIndex == -1 || clbGenere.CheckedItems.Count == 0)
                MessageBox.Show("Compilare tutti i campi obbligatori (Titolo, ISBN, Autore, Casa Editrice, Genere)", "Errore", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
            {

                ClsLibro libro = LeggiCampi();
                string errore;

                if (!modificaLibro)
                {
                    long id = ClsLibroBL.Create(ref Program.conn, libro, out errore);
                    if (!string.IsNullOrEmpty(errore))
                        MessageBox.Show("Errore: " + errore, "Errore", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    else if (id > 0)
                    {
                        if (cbAutore.SelectedIndex > -1)
                        {
                            ClsScrivere scrivere = new ClsScrivere();
                            scrivere.Data = DateTime.Now;
                            scrivere.AutoreID = (long)cbAutore.SelectedValue;
                            scrivere.LibroISBN = libro.Isbn;
                            ClsScrivereBL.Create(ref Program.conn, scrivere, out errore);
                        }

                        if (cbCasaEditrice.SelectedIndex > -1)
                        {
                            ClsPubblicare pubblicare = new ClsPubblicare();
                            pubblicare.AnnoPubblicazione = dtpDataProduzione.Value;
                            pubblicare.CasaEditriceID = (long)cbCasaEditrice.SelectedValue;
                            pubblicare.LibroISBN = libro.Isbn;
                            ClsPubblicareBL.Create(ref Program.conn, pubblicare, out errore);
                        }

                        for (int i = 0; i < clbGenere.CheckedItems.Count; i++)
                        {
                            ClsGenere g = (ClsGenere)clbGenere.CheckedItems[i];
                            ClsCaratterizzare caratterizzare = new ClsCaratterizzare();
                            caratterizzare.LibroISBN = libro.Isbn;
                            caratterizzare.GenereID = (int)g.ID;
                            ClsCaratterizzareBL.Create(ref Program.conn, caratterizzare, out errore);
                        }

                        MessageBox.Show("Libro aggiunto con successo!", "Successo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ResetCampi();
                        CaricaLibri();
                    }
                }
                else
                {
                    if (_libroSelezionato == null)
                        MessageBox.Show("Seleziona un libro da modificare.", "Attenzione", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    else
                    {
                        long esito = ClsLibroBL.Update(ref Program.conn, _idSelezionato, libro, out errore);
                        if (!string.IsNullOrEmpty(errore))
                            MessageBox.Show("Errore: " + errore, "Errore", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        else if (esito > 0)
                        {
                            if (cbAutore.SelectedIndex > -1)
                            {
                                List<ClsScrivere> scritture = ClsScrivereBL.GetByLibroISBN(ref Program.conn, libro.Isbn, out errore);
                                if (scritture.Count > 0)
                                {
                                    ClsScrivere scrivere = scritture[0];
                                    scrivere.AutoreID = (long)cbAutore.SelectedValue;
                                    scrivere.LibroISBN = libro.Isbn;
                                    ClsScrivereBL.Update(ref Program.conn, scrivere.ID, scrivere, out errore);
                                }
                            }

                            if (cbCasaEditrice.SelectedIndex > -1)
                            {
                                List<ClsPubblicare> pubblicazioni = ClsPubblicareBL.GetByLibroISBN(ref Program.conn, libro.Isbn, out errore);
                                if (pubblicazioni.Count > 0)
                                {
                                    ClsPubblicare pubblicare = pubblicazioni[0];
                                    pubblicare.AnnoPubblicazione = dtpDataProduzione.Value;
                                    pubblicare.CasaEditriceID = (long)cbCasaEditrice.SelectedValue;
                                    pubblicare.LibroISBN = libro.Isbn;
                                    ClsPubblicareBL.Update(ref Program.conn, pubblicare.ID, pubblicare, out errore);
                                }
                            }

                            List<ClsCaratterizzare> caratterizzazioni = ClsCaratterizzareBL.GetByLibroISBN(ref Program.conn, libro.Isbn, out errore);
                            for (int i = 0; i < caratterizzazioni.Count; i++)
                                ClsCaratterizzareBL.Delete(ref Program.conn, caratterizzazioni[i].ID, out errore);

                            for (int i = 0; i < clbGenere.CheckedItems.Count; i++)
                            {
                                ClsGenere g = (ClsGenere)clbGenere.CheckedItems[i];
                                ClsCaratterizzare caratterizzare = new ClsCaratterizzare();
                                caratterizzare.LibroISBN = libro.Isbn;
                                caratterizzare.GenereID = (int)g.ID;
                                ClsCaratterizzareBL.Create(ref Program.conn, caratterizzare, out errore);
                            }

                            MessageBox.Show("Libro modificato con successo!", "Successo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            ResetCampi();
                            CaricaLibri();
                        }
                    }
                }

                if (Program._chiudiForm)
                    this.Close();
            }
        }

        bool modalitàVisualizza = false;
        private void btnVisualizza_Click(object sender, EventArgs e)
        {
            if (lvLibri.SelectedItems.Count == 0)
                MessageBox.Show("Seleziona un libro da visualizzare.", "Attenzione", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else
                ModificaBtnVisualizza();
        }

        private void ModificaBtnVisualizza()
        {
            modalitàVisualizza = !modalitàVisualizza;
            if (modalitàVisualizza)
            {
                VisualizzaLibro();
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
            tbTitolo.ReadOnly = rendiReadOnly;
            tbLingua.ReadOnly = rendiReadOnly;
            nudPagine.Enabled = !rendiReadOnly;
            nudPrezzo.Enabled = !rendiReadOnly;
            nudQuantita.Enabled = !rendiReadOnly;
            cbAutore.Enabled = !rendiReadOnly;
            cbCasaEditrice.Enabled = !rendiReadOnly;
            clbGenere.Enabled = !rendiReadOnly;
            dtpDataProduzione.Enabled = !rendiReadOnly;
            tbEdizione.Enabled = !rendiReadOnly;
            rtbDescrizione.ReadOnly = rendiReadOnly;
            btnElimina.Enabled = !rendiReadOnly;
            btnModifica.Enabled = !rendiReadOnly;
            tbFiltroNome.ReadOnly = rendiReadOnly;
            btnAssegnaEbook.Enabled = !rendiReadOnly;

            btnAggiungiCasa.Enabled = !rendiReadOnly;
            btnAggiungiAutore.Enabled = !rendiReadOnly;
            btnAggiungiGenere.Enabled = !rendiReadOnly;
        }
        private void btnModifica_Click(object sender, EventArgs e)
        {
            if (lvLibri.SelectedItems.Count == 0)
                MessageBox.Show("Seleziona un libro da modificare.", "Attenzione", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else
            {
                VisualizzaLibro();
                _modalitaModifica = true;
                lblTitolo.Text = "Modifica Libro";
                btnAggiungi.Text = "☑️ Salva";
            }
        }

        private void btnElimina_Click(object sender, EventArgs e)
        {
            if (lvLibri.SelectedItems.Count == 0)
                MessageBox.Show("Seleziona un libro da eliminare.", "Attenzione", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else
            {
                DialogResult dr = MessageBox.Show("Vuoi eliminare il libro selezionato?", "Conferma", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dr == DialogResult.Yes)
                {
                    ClsLibro l = (ClsLibro)lvLibri.SelectedItems[0].Tag;
                    string errore;
                    long esito = ClsLibroBL.Delete(ref Program.conn, l.ProdottoID, out errore);
                    if (!string.IsNullOrEmpty(errore))
                        MessageBox.Show("Errore: " + errore, "Errore", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    else if (esito > 0)
                    {
                        MessageBox.Show("Libro eliminato.", "Successo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ResetCampi();
                        CaricaLibri();
                    }
                }
            }
        }

        private void tbFiltroNome_TextChanged(object sender, EventArgs e)
        {
            Ricerca(tbFiltroNome.Text.Trim());
        }

        private void Ricerca(string testo)
        {
            if (string.IsNullOrEmpty(testo))
                PopolaListView(Program._libri);
            else
            {
                string errore;
                List<ClsLibro> filtrati = ClsLibroBL.GetByNome(ref Program.conn, testo, out errore);
                if (!string.IsNullOrEmpty(errore))
                    MessageBox.Show("Errore: " + errore, "Errore", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else
                    PopolaListView(filtrati);
            }
        }

        private void VisualizzaLibro()
        {
            _libroSelezionato = (ClsLibro)lvLibri.SelectedItems[0].Tag;
            _idSelezionato = _libroSelezionato.ProdottoID;
            tbTitolo.Text = _libroSelezionato.Nome;
            tbEdizione.Text = _libroSelezionato.Edizione;
            tbLingua.Text = _libroSelezionato.Lingua;
            rtbDescrizione.Text = _libroSelezionato.Sinossi;
            nudPagine.Value = _libroSelezionato.NumeroPagine;
            nudPrezzo.Value = (decimal)_libroSelezionato.Prezzo;
            nudQuantita.Value = _libroSelezionato.Quantita; 
            dtpDataProduzione.Value = _libroSelezionato.AnnoPubblicazione != default(DateTime) ? _libroSelezionato.AnnoPubblicazione : DateTime.Now;
            tbISBN.Text = _libroSelezionato.Isbn;
            btnAssegnaEbook.Visible = true;

            if (!string.IsNullOrEmpty(_libroSelezionato.ImgCopertina))
            {
                string pathCompleto = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, _libroSelezionato.ImgCopertina);
                if (File.Exists(pathCompleto))
                {
                    try
                    {
                        _coverLibro = Image.FromFile(pathCompleto);
                        pbCoverLibro.Image = _coverLibro;
                    }
                    catch
                    {
                        pbCoverLibro.Image = Properties.Resources.cover;
                    }
                }
            }

            string erroreS;
            List<ClsScrivere> scritture = ClsScrivereBL.GetByLibroISBN(ref Program.conn, _libroSelezionato.Isbn, out erroreS);
            if (string.IsNullOrEmpty(erroreS) && scritture.Count > 0)
            {
                long autoreID = scritture[0].AutoreID;
                cbAutore.SelectedValue = autoreID;
            }

            string erroreP;
            List<ClsPubblicare> pubblicazioni = ClsPubblicareBL.GetByLibroISBN(ref Program.conn, _libroSelezionato.Isbn, out erroreP);
            if (string.IsNullOrEmpty(erroreP) && pubblicazioni.Count > 0)
            {
                long casaID = pubblicazioni[0].CasaEditriceID;
                cbCasaEditrice.SelectedValue = casaID;
            }

            string erroreG;
            List<ClsCaratterizzare> caratterizzazioni = ClsCaratterizzareBL.GetByLibroISBN(ref Program.conn, _libroSelezionato.Isbn, out erroreG);
            if (string.IsNullOrEmpty(erroreG))
            {
                for (int i = 0; i < clbGenere.Items.Count; i++)
                {
                    ClsGenere g = (ClsGenere)clbGenere.Items[i];
                    bool spuntato = false;
                    int j = 0;
                    while (j < caratterizzazioni.Count && !spuntato)
                    {
                        if (caratterizzazioni[j].GenereID == (int)g.ID)
                            spuntato = true;
                        j++;
                    }
                    clbGenere.SetItemChecked(i, spuntato);
                }
            }
        }

        private void btnAssegnaEbook_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "File Testo|*.txt;*.docx;*.doc;*.rtf;*.pdf;*.epub";
                ofd.Title = "Seleziona il file e-book";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        if (_idSelezionato > 0)
                        {
                            ClsLibro libro = Program._libri.FirstOrDefault(l => l.ProdottoID == _idSelezionato);
                            if (libro != null)
                            {
                                string errore;
                                string pathEBook = ClsLibroBL.SalvaEBook(ofd.FileName, libro.Nome, out errore);
                                if (!string.IsNullOrEmpty(errore))
                                    MessageBox.Show("Errore nel salvataggio del file: " + errore, "Errore", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                else
                                {
                                    libro.EBook = pathEBook;
                                    ClsLibroBL.Update(ref Program.conn, _idSelezionato, libro, out errore);
                                    if (!string.IsNullOrEmpty(errore))
                                        MessageBox.Show("Errore: " + errore, "Errore", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    else
                                        MessageBox.Show("E-book assegnato.\nSalvato in: " + pathEBook, "Successo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Errore: " + ex.Message, "Errore", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void btnAggiungiAutore_Click(object sender, EventArgs e)
        {
            Program._chiudiForm = true;
            FrmAutori frmAutori = new FrmAutori();
            frmAutori.ShowDialog();
            PopolaComboBox();
        }

        private void btnAggiungiCasa_Click(object sender, EventArgs e)
        {
            Program._chiudiForm = true;
            FrmCaseEditrici frmCase = new FrmCaseEditrici();
            frmCase.ShowDialog();
            PopolaComboBox();
        }

        private void btnAggiungiGenere_Click(object sender, EventArgs e)
        {
            Program._chiudiForm = true;
            FrmGeneri frmGeneri = new FrmGeneri();
            frmGeneri.ShowDialog();
            PopolaComboBox();
        }

        private void btnAggiungi_Click(object sender, EventArgs e)
        {
            GestisciLibro(_modalitaModifica);
        }

        private void btnAnnulla_Click(object sender, EventArgs e)
        {
            ResetCampi();
        }

        private void FrmLibri_FormClosing(object sender, FormClosingEventArgs e)
        {
            Program._chiudiForm = false;
        }
        private void pbCoverLibro_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "File Immagine|*.jpg;*.jpeg;*.png";
                ofd.Title = "Seleziona copertina (solo jpg, jpeg, png)";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        _coverLibro = Image.FromFile(ofd.FileName);
                        pbCoverLibro.Image = _coverLibro;
                        _pathCopertinaScelto = ofd.FileName;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Errore nel caricamento dell'immagine: " + ex.Message, "Errore", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void pbCoverLibro_MouseHover(object sender, EventArgs e)
        {
            pbCoverLibro.Image = Properties.Resources.caricaCover;
            pbCoverLibro.BorderStyle = BorderStyle.Fixed3D;
        }

        private void pbCoverLibro_MouseLeave(object sender, EventArgs e)
        {
            pbCoverLibro.Image = _coverLibro;
            pbCoverLibro.BorderStyle = BorderStyle.None;
        }
    }
}
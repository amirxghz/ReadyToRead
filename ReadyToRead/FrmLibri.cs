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
                string erroreS;
                List<ClsScrivere> scritture = ClsScrivereBL.GetByLibroISBN(ref Program.conn, l.Isbn, out erroreS);
                if (string.IsNullOrEmpty(erroreS) && scritture.Count > 0)
                {
                    long autoreID = scritture[0].AutoreID;
                    int j = 0;
                    while (j < _autori.Count)
                    {
                        if (_autori[j].ID == autoreID)
                        {
                            nomeAutore = _autori[j].Nome + " " + _autori[j].Cognome;
                            break;
                        }
                        j++;
                    }
                }

                string nomeCasa = "";
                string erroreP;
                List<ClsPubblicare> pubblicazioni = ClsPubblicareBL.GetByLibroISBN(ref Program.conn, l.Isbn, out erroreP);
                if (string.IsNullOrEmpty(erroreP) && pubblicazioni.Count > 0)
                {
                    long casaID = pubblicazioni[0].CasaEditriceID;
                    int j = 0;
                    while (j < _case.Count)
                    {
                        if (_case[j].ID == casaID)
                        {
                            nomeCasa = _case[j].RagioneSociale;
                            break;
                        }
                        j++;
                    }
                }

                string nomeGenere = "";
                string erroreG;
                List<ClsCaratterizzare> caratterizzazioni = ClsCaratterizzareBL.GetByLibroISBN(ref Program.conn, l.Isbn, out erroreG);
                if (string.IsNullOrEmpty(erroreG) && caratterizzazioni.Count > 0)
                {
                    long genereID = caratterizzazioni[0].GenereID;
                    int j = 0;
                    while (j < _generi.Count)
                    {
                        if (_generi[j].ID == genereID)
                        {
                            nomeGenere = _generi[j].Nome;
                            break;
                        }
                        j++;
                    }
                }
                
                ListViewItem lvi = new ListViewItem(l.Nome);
                lvi.SubItems.Add(nomeAutore);
                lvi.SubItems.Add(nomeCasa);
                lvi.SubItems.Add(nomeGenere);
                lvi.SubItems.Add(l.Quantita.ToString());
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
            dtmDataProduzione.MaxDate = DateTime.Today;
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
            libro.Descrizione = rtbDescrizione.Text.Trim();
            libro.NumeroPagine = (int)nudPagine.Value;
            libro.Prezzo = (float)nudPrezzo.Value;
            libro.AnnoPubblicazione = dtmDataProduzione.Value;
            libro.Quantita = (int)nudQuantita.Value;
            return libro;
        }
        
        private string GeneraISBN()
        {
            //generatore temporaneo poi lo cambuioi
            string ts = DateTime.Now.ToString("yyMMddHHmmssfff"); // 15 cifre
            return "978" + ts.Substring(0, 10); // 3 + 10 = 13 cifre
        }


        private void btnAggiungi_Click(object sender, EventArgs e)
        {
            GestisciLibro(false);
        }
        private void GestisciLibro(bool modificaLibro)
        {
            ClsLibro libro = LeggiCampi();

            if (string.IsNullOrWhiteSpace(libro.Nome))
            {
                MessageBox.Show("Il titolo del libro e' obbligatorio.", "Attenzione", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string errore;

            if (!string.IsNullOrEmpty(_pathCopertinaScelto))
            {
                string pathCopertina = ClsLibroBL.SalvaCopertina(_pathCopertinaScelto, libro.Nome, out errore);
                if (!string.IsNullOrEmpty(errore))
                {
                    MessageBox.Show("Errore nel salvataggio della copertina: " + errore, "Errore", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                libro.ImgCopertina = pathCopertina;
            }

            if (!modificaLibro)
            {
                int quantita = (int)nudQuantita.Value;
                if (quantita <= 0) quantita = 1;
                
                string isbn = tbISBN.Text.Trim();
                if (string.IsNullOrEmpty(isbn))
                    isbn = GeneraISBN();
                libro.Isbn = isbn;

                long primoID = 0;
                bool errore_ciclo = false;

                for (int q = 0; q < quantita; q++)
                {
                    long id = ClsLibroBL.Create(ref Program.conn, libro, out errore);

                    if (!string.IsNullOrEmpty(errore))
                    {
                        MessageBox.Show("Errore alla copia " + (q + 1) + ": " + errore, "Errore", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        errore_ciclo = true;
                        break;
                    }

                    if (q == 0)
                        primoID = id;
                }

                if (primoID > 0 && !errore_ciclo)
                {

                    if (cbAutore.SelectedIndex >= 0)
                    {
                        ClsAutore autoreSelezionato = (ClsAutore)cbAutore.SelectedItem;
                        ClsScrivere scrivere = new ClsScrivere();
                        scrivere.AutoreID = autoreSelezionato.ID;
                        scrivere.LibroISBN = libro.Isbn;
                        scrivere.Data = DateTime.Now;
                        string erroreScr;
                        ClsScrivereBL.Create(ref Program.conn, scrivere, out erroreScr);
                        if (!string.IsNullOrEmpty(erroreScr))
                            MessageBox.Show("Errore associazione autore: " + erroreScr, "Attenzione", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }

                    if (cbCasaEditrice.SelectedIndex >= 0)
                    {
                        ClsCasa casaSelezionata = (ClsCasa)cbCasaEditrice.SelectedItem;
                        ClsPubblicare pubblicare = new ClsPubblicare();
                        pubblicare.CasaEditriceID = casaSelezionata.ID;
                        pubblicare.LibroISBN = libro.Isbn;
                        pubblicare.AnnoPubblicazione = libro.AnnoPubblicazione;
                        pubblicare.Edizione = libro.Edizione;
                        string erroreP;
                        ClsPubblicareBL.Create(ref Program.conn, pubblicare, out erroreP);
                        if (!string.IsNullOrEmpty(erroreP))
                            MessageBox.Show("Errore associazione casa editrice: " + erroreP, "Attenzione", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }

                    for (int i = 0; i < clbGenere.CheckedItems.Count; i++)
                    {
                        ClsGenere genereSelezionato = (ClsGenere)clbGenere.CheckedItems[i];
                        ClsCaratterizzare car = new ClsCaratterizzare();
                        car.LibroISBN = libro.Isbn;
                        car.GenereID = (int)genereSelezionato.ID;
                        string erroreC;
                        ClsCaratterizzareBL.Create(ref Program.conn, car, out erroreC);
                        if (!string.IsNullOrEmpty(erroreC))
                            MessageBox.Show("Errore associazione genere: " + erroreC, "Attenzione", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }

                    MessageBox.Show(quantita + " copie del libro aggiunte con successo!", "Successo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ResetCampi();
                    CaricaLibri();
                }
            }
            else
            {
                if (_libroSelezionato == null)
                {
                    MessageBox.Show("Seleziona un libro da modificare.", "Attenzione", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                libro.Isbn = _libroSelezionato.Isbn;
                long esito = ClsLibroBL.Update(ref Program.conn, _idSelezionato, libro, out errore);
                if (!string.IsNullOrEmpty(errore))
                    MessageBox.Show("Errore: " + errore, "Errore", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else if (esito > 0)
                {
                    AggiornaGeneriLibro(_libroSelezionato.Isbn);
                    MessageBox.Show("Libro modificato con successo!", "Successo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ResetCampi();
                    CaricaLibri();
                }
            }
        }

        private void AggiornaGeneriLibro(string isbn)
        {
            string errore;
            List<ClsCaratterizzare> vecchie = ClsCaratterizzareBL.GetByLibroISBN(ref Program.conn, isbn, out errore);
            if (string.IsNullOrEmpty(errore))
            {
                for (int i = 0; i < vecchie.Count; i++)
                    ClsCaratterizzareBL.Delete(ref Program.conn, vecchie[i].ID, out errore);
            }
            for (int i = 0; i < clbGenere.CheckedItems.Count; i++)
            {
                ClsGenere g = (ClsGenere)clbGenere.CheckedItems[i];
                ClsCaratterizzare car = new ClsCaratterizzare();
                car.LibroISBN = isbn;
                car.GenereID = (int)g.ID;
                ClsCaratterizzareBL.Create(ref Program.conn, car, out errore);
            }
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
        }

        private void pbCoverLibro_MouseLeave(object sender, EventArgs e)
        {
            pbCoverLibro.Image = _coverLibro;
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
            dtmDataProduzione.Value = _libroSelezionato.AnnoPubblicazione != default(DateTime)
                ? _libroSelezionato.AnnoPubblicazione
                : DateTime.Now;
            tbISBN.Text = _libroSelezionato.Isbn;
            btnAssegnaEbook.Visible = true;

            if (!string.IsNullOrEmpty(_libroSelezionato.ImgCopertina))
            {
                string pathCompleto = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, _libroSelezionato.ImgCopertina);
                if (File.Exists(pathCompleto))
                {
                    try { _coverLibro = Image.FromFile(pathCompleto); pbCoverLibro.Image = _coverLibro; }
                    catch { pbCoverLibro.Image = Properties.Resources.cover; }
                }
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

        private void btnAnnulla_Click(object sender, EventArgs e)
        {
            ResetCampi();
        }

        private void FrmLibri_FormClosing(object sender, FormClosingEventArgs e)
        {
            Program._chiudiForm = false;
        }
    }
}

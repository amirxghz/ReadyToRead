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
    public partial class FrmLibri : Form
    {
        bool _inModifica = false;
        ClsLibro _libroSelezionato = null;
        long _idSelezionato = -1;

        List<ClsAutore> _autori = new List<ClsAutore>();
        List<ClsCasa> _case = new List<ClsCasa>();
        List<ClsGenere> _generi = new List<ClsGenere>();


        Image _coverLibro = Properties.Resources.cover;

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
                for (int i=0;  i < _generi.Count;i++)
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
            for (int i=0; i < libri.Count;i++)
            {
                ClsLibro l = libri[i];

                string nomeAutore = "";
                string erroreS;
                List<ClsScrivere> scritture = ClsScrivereBL.GetByAutoreID(ref Program.conn, l.ProdottoID, out erroreS);
                if (string.IsNullOrEmpty(erroreS) && scritture.Count > 0)
                {
                    long autoreID = scritture[0].AutoreID;
                    ClsAutore autore = null;
                    int j = 0;
                    while (j < _autori.Count && autore == null)
                    {
                        if (_autori[j].ID == autoreID)
                            autore = _autori[j];
                        j++;
                    }
                    if (autore != null)
                        nomeAutore = autore.Nome + " " + autore.Cognome;
                }

                string nomeCasa = "";
                string erroreP;
                List<ClsPubblicare> pubblicazioni = ClsPubblicareBL.GetByCasaEditriceID(ref Program.conn, l.ProdottoID, out erroreP);
                if (string.IsNullOrEmpty(erroreP) && pubblicazioni.Count > 0)
                {
                    long casaID = pubblicazioni[0].CasaEditriceID;
                    ClsCasa casa = null;
                    int j = 0;
                    while (j < _case.Count && casa == null)
                    {
                        if (_case[j].ID == casaID)
                            casa = _case[j];
                        j++;
                    }
                    if (casa != null)
                        nomeCasa = casa.RagioneSociale;
                }

                string nomeGenere = "";
                string erroreG="";
                List<ClsCaratterizzare> generi = ClsCaratterizzareBL.GetByGenereID(ref Program.conn, l.ProdottoID, out erroreG);
                if (string.IsNullOrEmpty(erroreG) && pubblicazioni.Count > 0)
                {
                    long genereID = generi[0].GenereID;
                    ClsGenere genere = null;
                    int j = 0;
                    while (j < _case.Count && genere == null)
                    {
                        if (_generi[j].ID == genereID)
                            genere = _generi[j];
                        j++;
                    }
                    if (genere != null)
                        nomeGenere = genere.Nome;
                }

                ListViewItem lvi = new ListViewItem(l.Nome);
                lvi.SubItems.Add(nomeAutore);
                lvi.SubItems.Add(nomeCasa);
                lvi.SubItems.Add(nomeGenere);
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
            dtmDataProduzione.Value = DateTime.Now;
            cbAutore.SelectedIndex = -1;
            cbCasaEditrice.SelectedIndex = -1;
            for (int i = 0;  i < clbGenere.Items.Count; i++)
                clbGenere.SetItemChecked(i, false);
            _coverLibro = Properties.Resources.cover;
            pbCoverLibro.Image = _coverLibro;
            _inModifica = false;
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
            libro.Descrizione = rtbDescrizione.Text.Trim();
            libro.NumeroPagine = (int)nudPagine.Value;
            libro.Prezzo = (float)nudPrezzo.Value;
            libro.AnnoPubblicazione = dtmDataProduzione.Value;
            return libro;
        }



        private void pbCoverLibro_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "File Immagine|*.jpg;*.jpeg;*.png";
                ofd.Title = "Seleziona una foto profilo(solo jpg, jpeg e png)";

                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        _coverLibro = Image.FromFile(ofd.FileName);
                        pbCoverLibro.Image = _coverLibro;
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

        private void btnAnnulla_Click(object sender, EventArgs e)
        {
            ResetCampi();
        }

        private void btnAggiungi_Click(object sender, EventArgs e)
        {
            GestisciLibro(_inModifica);
        }

        private void btnVisualizza_Click(object sender, EventArgs e)
        {
            VisualizzaLibro();
        }

        private void btnModifica_Click(object sender, EventArgs e)
        {
            if (lvLibri.SelectedItems.Count == 0)
                MessageBox.Show("Seleziona un libro da modificare.", "Attenzione", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else
            {
                VisualizzaLibro();
                _inModifica = true;
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
        

        private void GestisciLibro(bool modificaLibro)
        {
            ClsLibro libro = LeggiCampi();
            string errore;
            libro.Isbn = GeneraISBN(libro);

            if (!modificaLibro)
            {
                long id = ClsLibroBL.Create(ref Program.conn, libro, out errore);
                if (!string.IsNullOrEmpty(errore))
                    MessageBox.Show("Errore: " + errore, "Errore", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else if (id > 0)
                {
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
                        MessageBox.Show("Libro modificato con successo!", "Successo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ResetCampi();
                        CaricaLibri();
                    }
                }
            }
        }

        private string GeneraISBN(ClsLibro libro)
        {
            string isbn=String.Empty;
            return isbn;
        }

        private void VisualizzaLibro()
        {
            if (lvLibri.SelectedItems.Count == 0)
                MessageBox.Show("Seleziona un libro da visualizzare.", "Attenzione", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else
            {
                _libroSelezionato = (ClsLibro)lvLibri.SelectedItems[0].Tag;
                _idSelezionato = _libroSelezionato.ProdottoID;
                tbTitolo.Text = _libroSelezionato.Nome;
                tbEdizione.Text = _libroSelezionato.Edizione;
                tbLingua.Text = _libroSelezionato.Lingua;
                rtbDescrizione.Text = _libroSelezionato.Sinossi;
                nudPagine.Value = _libroSelezionato.NumeroPagine;
                nudPrezzo.Value = (decimal)_libroSelezionato.Prezzo;
                dtmDataProduzione.Value = _libroSelezionato.AnnoPubblicazione;
                btnAssegnaEbook.Visible = true;
                tbISBN.Text = _libroSelezionato.Isbn;
            }
        }


        private void btnAssegnaEbook_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "File Testo|*.txt;*.docx;*.doc;*.rtf";
                ofd.Title = "Seleziona un file di testo per l'e-book (solo txt, docx, doc e rtf)";

                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        if (_idSelezionato > 0)
                        {
                            string errore;
                            ClsLibro libro = Program._libri.FirstOrDefault(l => l.ProdottoID == _idSelezionato);
                            if (libro != null)
                            {
                                libro.EBook = ofd.FileName;
                                ClsLibroBL.Update(ref Program.conn, _idSelezionato, libro, out errore);
                                if (!string.IsNullOrEmpty(errore))
                                    MessageBox.Show("Errore: " + errore, "Errore", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                else
                                    MessageBox.Show("E-book assegnato con successo.", "Successo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Errore nel caricamento del file: " + ex.Message, "Errore", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
    }
}

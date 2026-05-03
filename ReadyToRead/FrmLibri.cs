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
        public FrmLibri()
        {
            InitializeComponent();
        }

        private void FrmLibri_Load(object sender, EventArgs e)
        {
            PopolaListView(Program._libri);
        }

        private void PopolaListView(List<ClsLibro> libri)
        {
            lvLibri.Items.Clear();
            for(int i = 0; i<libri.Count;i++)
            {
                ListViewItem lvi = new ListViewItem(libri[i].Nome);
                //lvi.SubItems.Add(libri[i].Autore);
                //lvi.SubItems.Add(libri[i].CasaEditrice.Nome);
                //lvi.SubItems.Add(libri[i].Genere);
                lvLibri.Items.Add(lvi);
            }
        }

        Image _coverLibro = Properties.Resources.cover;

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
            GestisciLibro(false);
        }

        private void btnVisualizza_Click(object sender, EventArgs e)
        {
            VisualizzaLibro();
        }

        private void btnModifica_Click(object sender, EventArgs e)
        {
            GestisciLibro(true);
        }

        private void btnElimina_Click(object sender, EventArgs e)
        {
            EliminaLibro();
        }
        private void tbFiltroNome_TextChanged(object sender, EventArgs e)
        {
            Ricerca(tbFiltroNome.Text.Trim());
        }

        private void Ricerca(string text)
        {
        }

        private void GestisciLibro(bool modificaLibro) //Aggiungi e modifica
        {
        }
        private void EliminaLibro()
        {
        }

        private void VisualizzaLibro()
        {
        }

        private void ResetCampi()
        {
        }
    }
}

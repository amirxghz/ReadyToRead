using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//Urbani
namespace ReadyToRead
{
    public class ClsLibro:ClsProdotto
    {
        enum eTIPO
        {
            fisico,
            e_book
        }
        string _isbn;
        DateTime _annoPubblicazione;
        int _numeroPagine;
        string _sinossi;
        string _edizione;
        string _imgCopertina;
        string _eBook;
        eTIPO tipo;
        long prodottoID;

        public string Isbn { get => _isbn; set => _isbn = value; }
        public DateTime AnnoPubblicazione { get => _annoPubblicazione; set => _annoPubblicazione = value; }
        public int NumeroPagine { get => _numeroPagine; set => _numeroPagine = value; }
        public string Sinossi { get => _sinossi; set => _sinossi = value; }
        public string Edizione { get => _edizione; set => _edizione = value; }
        public string ImgCopertina { get => _imgCopertina; set => _imgCopertina = value; }
        public string EBook { get => _eBook; set => _eBook = value; }
        public long ProdottoID1 { get => prodottoID; set => prodottoID = value; }
        private eTIPO Tipo { get => tipo; set => tipo = value; }

        public ClsLibro()
        {

        }
        public ClsLibro(string isbn, ClsAutore autore)
        {
            Isbn = isbn;
        }
    }
}

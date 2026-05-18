using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadyToRead
{
    public class ClsPubblicare //Amir
    {
        long _ID;
        DateTime _annoPubblicazione;
        string _edizione;
        long _casaEditriceID;
        string _libroISBN;

        public long ID { get => _ID; set => _ID = value; }
        public DateTime AnnoPubblicazione { get => _annoPubblicazione; set => _annoPubblicazione = value; }
        public string Edizione { get => _edizione; set => _edizione = value; }
        public long CasaEditriceID { get => _casaEditriceID; set => _casaEditriceID = value; }
        public string LibroISBN { get => _libroISBN; set => _libroISBN = value; }

        public ClsPubblicare()
        {
        }

        public ClsPubblicare(DateTime annoPubblicazione, string edizione, long casaEditriceID, string libroISBN)
        {
            AnnoPubblicazione = annoPubblicazione;
            Edizione = edizione;
            CasaEditriceID = casaEditriceID;
            LibroISBN = libroISBN;
        }
    }
}

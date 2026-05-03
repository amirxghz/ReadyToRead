using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//Urbani
namespace ReadyToRead
{
    public class ClsRecensione
    {
        public enum eStelle
        {
            una,
            due,
            tre,
            quattro,
            cinque
        }
        string _titolo;
        string _descrizione;
        eStelle _valutazione;

        public string Titolo { get => _titolo; set => _titolo = value; }
        public string Descrizione { get => _descrizione; set => _descrizione = value; }
        private eStelle Valutazione { get => _valutazione; set => _valutazione = value; }

        public ClsRecensione()
        {

        }

        public ClsRecensione(string titolo, string descrizione, eStelle valutazione, string username)
        {
            Titolo = titolo;
            Descrizione = descrizione;
            Valutazione = valutazione;
        }
    }
}

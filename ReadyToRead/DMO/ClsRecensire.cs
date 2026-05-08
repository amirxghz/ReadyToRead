using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//Urbani
namespace ReadyToRead
{
    public class ClsRecensire
    {
        public enum eStelle
        {
            una,
            due,
            tre,
            quattro,
            cinque
        }
        long _ID;
        string _titolo;
        string _descrizione;
        eStelle _valutazione;
        DateTime _dataPubblicazione;
        long _clienteID;
        long _prodottoID;

        public string Titolo { get => _titolo; set => _titolo = value; }
        public string Descrizione { get => _descrizione; set => _descrizione = value; }
        private eStelle Valutazione { get => _valutazione; set => _valutazione = value; }
        public long ID { get => _ID; set => _ID = value; }
        public DateTime DataPubblicazione { get => _dataPubblicazione; set => _dataPubblicazione = value; }
        public long ClienteID { get => _clienteID; set => _clienteID = value; }
        public long ProdottoID { get => _prodottoID; set => _prodottoID = value; }

        public ClsRecensire()
        {

        }

        public ClsRecensire(string titolo, string descrizione, eStelle valutazione, string username)
        {
            Titolo = titolo;
            Descrizione = descrizione;
            Valutazione = valutazione;
        }
    }
}

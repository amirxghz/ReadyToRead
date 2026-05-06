using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//Urbani
namespace ReadyToRead
{
    public class ClsOrdinare
    {
        public enum eStatoOrdine
        {
            ARRIVATO,
            SPEDITO,
            NON_SPEDITO
        }
        long _ID;
        eStatoOrdine _statoOrdine;
        decimal _totale;
        string _destinazione;
        DateTime _data;
        long _clienteID;
        long _prodottoID;

        public eStatoOrdine StatoOrdine { get => _statoOrdine; set => _statoOrdine = value; }
        public decimal Totale { get => _totale; set => _totale = value; }
        public string Destinazione { get => _destinazione; set => _destinazione = value; }
        public long ID { get => _ID; set => _ID = value; }
        public DateTime Data { get => _data; set => _data = value; }
        public long ProdottoID { get => _prodottoID; set => _prodottoID = value; }
        public long ClienteID { get => _clienteID; set => _clienteID = value; }

        public ClsOrdinare()
        {

        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//Urbani
namespace ReadyToRead
{
    class ClsOrdine
    {
        enum eStatoOrdine
        {
            ARRIVATO,
            SPEDITO,
            NON_SPEDITO
        }
        int _ID;
        string _ordineID;
        eStatoOrdine _statoOrdine;
        decimal _totale;
        string _destinazione;


        public string OrdineID { get => _ordineID; set => _ordineID = value; }
        private eStatoOrdine StatoOrdine { get => _statoOrdine; set => _statoOrdine = value; }
        public decimal Totale { get => _totale; set => _totale = value; }
        public string Destinazione { get => _destinazione; set => _destinazione = value; }
        public int ID { get => _ID; set => _ID = value; }

        public ClsOrdine()
        {

        }
    }
}

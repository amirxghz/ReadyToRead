using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//Urbani
namespace ReadyToRead
{
    public class ClsCasa:ClsUtente
    {
        public enum eTIPO_CASA
        {
            editrice,
            produttrice
        }

        public enum eTIPO_AZIENDA
        {
            SRL,
            SNC,
            SAS,
            SPA
        }
        string _ragioneSociale;
        string _indirizzoSedeLegale;
        string _indirizzoSedeOperativa;
        eTIPO_AZIENDA _tipoAzienda;
        bool _esclusiva;
        eTIPO_CASA _tipologia;
        long _utenteID;
        public string RagioneSociale { get => _ragioneSociale; set => _ragioneSociale = value; }
        public string IndirizzoSedeLegale { get => _indirizzoSedeLegale; set => _indirizzoSedeLegale = value; }
        public string IndirizzoSedeOperativa { get => _indirizzoSedeOperativa; set => _indirizzoSedeOperativa = value; }
        public eTIPO_AZIENDA TipoAzienda { get => _tipoAzienda; set => _tipoAzienda = value; }
        public bool Esclusiva { get => _esclusiva; set => _esclusiva = value; }
        public eTIPO_CASA Tipologia { get => _tipologia; set => _tipologia = value; }
        public long UtenteID { get => _utenteID; set => _utenteID = value; }

        public ClsCasa()
        {

        }

        public ClsCasa(long id,string ragioneSociale)
        {
            ID = id;
            RagioneSociale = ragioneSociale;
        }
    }
}

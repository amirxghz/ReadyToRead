using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//Urbani
namespace ReadyToRead
{
    public class ClsCasa
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
        int _indirizzoSedeLegaleID;
        int _indirizzoSedeOperativaID;
        eTIPO_AZIENDA _tipoAzienda;
        bool _esclusiva;
        eTIPO_CASA _tipologia;
        public string RagioneSociale { get => _ragioneSociale; set => _ragioneSociale = value; }
        public int IndirizzoSedeLegaleID { get => _indirizzoSedeLegaleID; set => _indirizzoSedeLegaleID = value; }
        public int IndirizzoSedeOperativaID { get => _indirizzoSedeOperativaID; set => _indirizzoSedeOperativaID = value; }
        public eTIPO_AZIENDA TipoAzienda { get => _tipoAzienda; set => _tipoAzienda = value; }
        public bool Esclusiva { get => _esclusiva; set => _esclusiva = value; }
        public eTIPO_CASA Tipologia { get => _tipologia; set => _tipologia = value; }

        public ClsCasa()
        {

        }

        public ClsCasa(string _ragioneSociale)
        {

        }
    }
}

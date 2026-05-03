using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadyToRead
{
    public class ClsAutore: ClsUtente //amir
    {
        bool _èVerificato;
        int _utenteID;
        public bool ÈVerificato { get => _èVerificato; set => _èVerificato = value; }
        public int UtenteID { get => _utenteID; set => _utenteID = value; }

        public ClsAutore()
        {
        }
        public ClsAutore(string nome, string cognome)
        {
            Nome = nome;
            Cognome = cognome;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadyToRead
{
    public class ClsAutore : ClsUtente //Amir
    {
        long _ID;
        bool _èVerificato;
        string _nomeArte;
        DateTime? _dataMorte;
        string _cittaNascita;
        long _utenteID;

        public bool ÈVerificato { get => _èVerificato; set => _èVerificato = value; }

        public long UtenteID { get => _utenteID; set => _utenteID = value; }

        public long ID { get => _ID; set => _ID = value; }

        public string NomeArte
        {
            get => _nomeArte;
            set
            {
                   _nomeArte = value;
            }
        }

        public DateTime? DataMorte
        {
            get => _dataMorte;
            set
            {
                if (value.HasValue && value.Value > DateTime.Now)
                    throw new Exception("Data di morte non può essere nel futuro");
                else
                    _dataMorte = value;
            }
        }

        public string CittaNascita
        {
            get => _cittaNascita;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new Exception("Città di nascita non può essere vuota");
                else
                    _cittaNascita = value;
            }
        }

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
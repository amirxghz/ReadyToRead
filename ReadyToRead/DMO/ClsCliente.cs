using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadyToRead
{
    public class ClsCliente: ClsUtente //amir
    {
        private string _indirizzo;
        private string _cap;
        private int _utenteID;
        public string Indirizzo
        {
            get => _indirizzo;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new Exception("L'indirizzo non può essere vuoto");
                else
                    _indirizzo = value;
            }
        }

        public string CAP
        {
            get => _cap;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new Exception("Il cap non può essere vuoto");
                else
                    _cap = value;
            }
        }

        public int UtenteID { get => _utenteID; set => _utenteID = value; }

        public ClsCliente()
        {
        }
        public ClsCliente(long id, string email, string password)
        {
            ID = id;
            Email = email;
            Password = password;
        }
    }
}

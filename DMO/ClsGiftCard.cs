using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadyToRead
{
    public class ClsGiftCard: ClsProdotto //amir
    {
        private string _nomeDestinatario;

        public string NomeDestinatario
        {
            get => _nomeDestinatario;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new Exception("Nome non valido");
                else
                    _nomeDestinatario = value;
            }
        }
        public ClsGiftCard()
        {
        }
    }
}

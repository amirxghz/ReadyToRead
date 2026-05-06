using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadyToRead
{
    public class ClsGiftCard: ClsProdotto //amir
    {
        long _ID;
        private string _nomeDestinatario;
        string _dedica;
        long _prodottoID;

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

        public long ID { get => _ID; set => _ID = value; }
        public string Dedica { get => _dedica; set => _dedica = value; }
        public long ProdottoID1 { get => _prodottoID; set => _prodottoID = value; }

        public ClsGiftCard()
        {
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//Urbani
namespace ReadyToRead
{
    public class ClsGenere
    {
        public enum eTIPO_GENERE
        {
            narrativo,
            musicale
        }
        long _ID;
        string _nome;
        string _descrizione;
        string _target;
        eTIPO_GENERE _tipologia;

        public string Nome { get => _nome; set => _nome = value; }
        public string Descrizione { get => _descrizione; set => _descrizione = value; }
        public string Target { get => _target; set => _target = value; }
        public eTIPO_GENERE Tipologia { get => _tipologia; set => _tipologia = value; }
        public long ID { get => _ID; set => _ID = value; }

        public ClsGenere()
        {

        }
        public ClsGenere(string nome)
        {
            Nome = nome;
        }
    }
}

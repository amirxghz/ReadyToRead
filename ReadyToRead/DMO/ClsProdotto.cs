using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//Urbani
namespace ReadyToRead
{
    public class ClsProdotto
    {
        public enum eStatoDisponibilita
        {
            DISPONIBILE,
            PREORDINE,
            NON_DISPONIBILE
        }
        int _prodottoID;
        string _nome;
        eStatoDisponibilita _disponibilita;
        float _prezzo;
        string _descrizione;
        string _lingua;
        int _quantita;

        public int ProdottoID { get => _prodottoID; set => _prodottoID = value; }
        public string Nome { get => _nome; set => _nome = value; }
        public eStatoDisponibilita Disponibilita { get => _disponibilita; set => _disponibilita = value; }
        public float Prezzo { get => _prezzo; set => _prezzo = value; }
        public string Descrizione { get => _descrizione; set => _descrizione = value; }
        public string Lingua { get => _lingua; set => _lingua = value; }
        public int Quantita { get => _quantita; set => _quantita = value; }

        public ClsProdotto()
        {

        }

        public ClsProdotto(string nome, float prezzo)
        {
            Nome = nome;
            Prezzo = prezzo;
        }
    }
}

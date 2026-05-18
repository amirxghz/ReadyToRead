using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadyToRead
{
    public class ClsUtente //Amir
    {
        public enum eCOMUNE
        {
            Nessuno,
            jesi,
            ancona,
            chiaravalle,
            senigallia,
            rotella
        }
        public enum eSESSO
        {
            m,
            f
        }

        private long _ID;
        private string _username;
        private string _password;
        private string _nome;
        private string _cognome;
        private string _email;
        private DateTime _dataDiNascita;
        private string _codiceFiscale;
        private eCOMUNE _comuneDiNascita;
        private eSESSO _sesso;
        private string _foto_profilo;

        #region Proprietà
        public long ID { get => _ID; set => _ID = value; }

        public string Username
        {
            get => _username;
            set
            {
                /*if (string.IsNullOrWhiteSpace(value))
                    throw new Exception("Username non può essere vuoto");
                else*/
                    _username = value;
            }
        }

        public string Password
        {
            get => _password;
            set
            {
                /*if (string.IsNullOrWhiteSpace(value))
                    throw new Exception("Password non può essere vuota");
                else*/
                    _password = value;
            }
        }

        public string Nome
        {
            get => _nome;
            set
            {
                /*if (string.IsNullOrWhiteSpace(value))
                    throw new Exception("Nome non può essere vuoto");
                else*/
                    _nome = value;
            }
        }

        public string Cognome
        {
            get => _cognome;
            set
            {
                /*if (string.IsNullOrWhiteSpace(value))
                    throw new Exception("Cognome non può essere vuoto");
                else*/
                    _cognome = value;
            }
        }

        public string Email
        {
            get => _email;
            set
            {
                /*if (string.IsNullOrWhiteSpace(value))
                    throw new Exception("Email non valida");
                else*/
                    _email = value;
            }
        }

        public DateTime DataDiNascita
        {
            get => _dataDiNascita;
            set => _dataDiNascita = value;
        }

        public string CodiceFiscale
        {
            get => _codiceFiscale;
            set => _codiceFiscale = value;
        }

        public eCOMUNE ComuneDiNascita
        {
            get => _comuneDiNascita;
            set => _comuneDiNascita = value;
        }

        public eSESSO Sesso
        {
            get => _sesso;
            set => _sesso = value;
        }

        public string Foto_profilo { get => _foto_profilo; set => _foto_profilo = value; }
        #endregion

        #region Costruttori
        public ClsUtente()
        {
            ComuneDiNascita = eCOMUNE.Nessuno;
        }

        public ClsUtente(string username, string password)
        {
            ComuneDiNascita = eCOMUNE.Nessuno;
            Username = username;
            Password = password;
        }
        #endregion
    }
}
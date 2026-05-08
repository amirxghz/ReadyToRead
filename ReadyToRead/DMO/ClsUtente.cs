using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadyToRead
{
    public class ClsUtente //amir
    {
        #region attributi
        public enum eCOMUNE
        {
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
        private static int _ID;
        private static string _username;
        private static string _password;
        private static string _nome;
        private static string _cognome;
        private static string _email;
        private static DateTime _dataDiNascita;
        private static string _codiceFiscale;
        private static eCOMUNE _comuneDiNascita;
        private static eSESSO _sesso;

        #endregion

        #region proprietà

        public string Username
        {
            get => _username;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new Exception("Username non può essere vuoto");
                else
                    _username = value;
            }
        }

        public string Password
        {
            get => _password;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new Exception("Password non può essere vuota");
                else
                    _password = value;
            }
        }

        public string Nome
        {
            get => _nome;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new Exception("Nome non può essere vuoto");
                else
                    _nome = value;
            }
        }

        public string Cognome
        {
            get => _cognome;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new Exception("Cognome non può essere vuoto");
                else
                    _cognome = value;
            }
        }

        public string Email
        {
            get => _email;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new Exception("Email non valida");
                else
                    _email = value;
            }
        }

        public DateTime DataDiNascita
        {
            get => _dataDiNascita;
            set
            {
                    _dataDiNascita = value;
            }
        }

        public string CodiceFiscale
        {
            get => _codiceFiscale;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new Exception("Codice fiscale non può essere vuoto");
                else
                    _codiceFiscale = value;
            }
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

        public int Età
        {
            get
            {
                int anni = DateTime.Now.Year - _dataDiNascita.Year;
                if (DateTime.Now.Month < _dataDiNascita.Month ||
                    (DateTime.Now.Month == _dataDiNascita.Month && DateTime.Now.Day < _dataDiNascita.Day))
                {
                    anni--;
                }
                return anni;
            }
        }

        public static int ID { get => _ID; set => _ID = value; }

        #endregion

        #region Costruttore
        public ClsUtente()
        {
        }

        public ClsUtente(string username, string password)
        {
            Username = username;
            Password = string.IsNullOrWhiteSpace(password) ? "1234567890xxx" : password;
        }
        #endregion
    }
}

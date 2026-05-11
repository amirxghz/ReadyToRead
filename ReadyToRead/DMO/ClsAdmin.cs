using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadyToRead
{
    public class ClsAdmin : ClsUtente
    {
        private long _adminID;
        private long _utenteID;

        public long AdminID { get => _adminID; set => _adminID = value; }
        public long UtenteID { get => _utenteID; set => _utenteID = value; }

        public ClsAdmin()
        {
        }

        public ClsAdmin(string username, string password, string email)
        {
            Username = username;
            Password = password;
            Email = email;
        }
    }
}
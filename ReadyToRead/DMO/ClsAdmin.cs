using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadyToRead
{
    public class ClsAdmin:ClsUtente //amir
    {
        private long _id;
        private static int _utenteID;
        public long ID
        {
            get => _id;
            set
            {
                _id = value;
            }
        }

        public static int UtenteID { get => _utenteID; set => _utenteID = value; }

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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadyToRead
{
    public class ClsSuperAdmin : ClsAdmin //amir
    {
        private static long _adminID;
        long _utenteID;
        public ClsSuperAdmin()
        { }

        public static long AdminID { get => _adminID; set => _adminID = value; }
        public long UtenteID { get => _utenteID; set => _utenteID = value; }
    }
}

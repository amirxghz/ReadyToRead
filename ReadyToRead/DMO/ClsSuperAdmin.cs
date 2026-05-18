using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadyToRead
{
    public class ClsSuperAdmin : ClsAdmin //amir
    {
        long _ID;
        private static long _adminID;
        long _utenteID;
        public ClsSuperAdmin()
        { }

        public static long AdminID { get => _adminID; set => _adminID = value; }
        public long ID1 { get => _ID; set => _ID = value; }
        public long UtenteID1 { get => _utenteID; set => _utenteID = value; }
    }
}

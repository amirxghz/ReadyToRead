using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadyToRead
{
    public class ClsSuperAdmin: ClsAdmin //amir
    {
        private static int _adminID;
        public ClsSuperAdmin()
        { }

        public static int AdminID { get => _adminID; set => _adminID = value; }
    }
}

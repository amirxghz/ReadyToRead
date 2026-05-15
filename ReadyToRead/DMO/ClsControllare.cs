using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadyToRead
{
    public class ClsControllare //Amir
    {
        long _ID;
        DateTime _data;
        long _adminID;
        long _prodottoID;

        public long ID { get => _ID; set => _ID = value; }
        public DateTime Data { get => _data; set => _data = value; }
        public long AdminID { get => _adminID; set => _adminID = value; }
        public long ProdottoID { get => _prodottoID; set => _prodottoID = value; }

        public ClsControllare()
        {
        }

        public ClsControllare(DateTime data, long adminID, long prodottoID)
        {
            Data = data;
            AdminID = adminID;
            ProdottoID = prodottoID;
        }
    }
}

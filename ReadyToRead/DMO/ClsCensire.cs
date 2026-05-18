using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadyToRead
{
    public class ClsCensire //Amir
    {
        long _ID;
        DateTime _data;
        long _adminID;
        long _genereID;

        public long ID { get => _ID; set => _ID = value; }
        public DateTime Data { get => _data; set => _data = value; }
        public long AdminID { get => _adminID; set => _adminID = value; }
        public long GenereID { get => _genereID; set => _genereID = value; }

        public ClsCensire()
        {
        }

        public ClsCensire(DateTime data, long adminID, long genereID)
        {
            Data = data;
            AdminID = adminID;
            GenereID = genereID;
        }
    }
}
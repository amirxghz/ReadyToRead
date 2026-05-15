using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadyToRead
{
    public class ClsGenerare //Amir
    {
        long _ID;
        DateTime _data;
        long _autoreID;
        long _adminID;

        public long ID { get => _ID; set => _ID = value; }
        public DateTime Data { get => _data; set => _data = value; }
        public long AutoreID { get => _autoreID; set => _autoreID = value; }
        public long AdminID { get => _adminID; set => _adminID = value; }

        public ClsGenerare()
        {
        }

        public ClsGenerare(DateTime data, long autoreID, long adminID)
        {
            Data = data;
            AutoreID = autoreID;
            AdminID = adminID;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadyToRead
{
    public class ClsScrivere //Amir
    {
        long _ID;
        DateTime _data;
        long _autoreID;
        string _libroISBN;

        public long ID { get => _ID; set => _ID = value; }
        public DateTime Data { get => _data; set => _data = value; }
        public long AutoreID { get => _autoreID; set => _autoreID = value; }
        public string LibroISBN { get => _libroISBN; set => _libroISBN = value; }

        public ClsScrivere()
        {
        }

        public ClsScrivere(DateTime data, long autoreID, string libroISBN)
        {
            Data = data;
            AutoreID = autoreID;
            LibroISBN = libroISBN;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadyToRead
{
    public class ClsCaratterizzare
    {
        long _ID;
        string _libroISBN;
        int _genereID;

        public long ID { get => _ID; set => _ID = value; }
        public string LibroISBN { get => _libroISBN; set => _libroISBN = value; }
        public int GenereID { get => _genereID; set => _genereID = value; }
        public ClsCaratterizzare()
        {
        }
    }
}

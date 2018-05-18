using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class Podatak
    {
        public double vrednost { get; set; }
        public string datum { get; set; }

        public Podatak() { }
        public Podatak(double v, string d)
        {
            vrednost = v;
            datum = d;
        }
    }
}

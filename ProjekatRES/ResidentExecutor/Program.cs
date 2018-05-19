using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResidentExecutor
{
    class Program
    {
        static void Main(string[] args)
        {
            IFunkcije funkcija;

            funkcija = new ProsecnaPotrosnja();

            funkcija.IzvrsiFunkciju();
            Console.ReadLine();
        }
    }
}

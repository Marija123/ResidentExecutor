using DataAccess;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ResidentExecutor
{
    
    public class ProsecnaPotrosnja : IFunkcije
    {
       
        public Metode m = new Metode();

        public void IzvrsiFunkciju()
        {
            List<Podrucje> lista = m.IzlistajPodrucja();
            List<Podatak> listaPodataka = m.IscitajIzBaze();
            double[] listaSuma = new double[10];
            Dictionary<string, string> listaPoslednjihVremena = new Dictionary<string, string>();
            Dictionary<string, string> dic = m.IzlistajPoslednjeProsecne();

            int[] count = new int[10];
            List<double> listaRez = new List<double>();

            if (!dic.Any())
            {
                for (int i = 0; i < lista.Count; i++)
                {
                    count[i] = 0;
                    List<string> s = new List<string>();
                    foreach (Podatak pod in listaPodataka)
                    {

                        if (lista[i].Sifra.Equals(pod.sifraPod))
                        {
                            string[] pom = pod.datum.Split(' ');
                            s.Add(pom[1]);
                            count[i]++;
                            listaSuma[i] += pod.vrednost;
                        }
                    }

                    listaPoslednjihVremena.Add(lista[i].Sifra, s.Last());

                }

                for (int i = 0; i < listaSuma.Count(); i++)
                {
                    listaRez.Add(listaSuma[i] / count[i]);
                }
            }
            else
            {

                for (int i = 0; i < lista.Count; i++)
                {
                    //count[i] = 0;
                    List<string> s = new List<string>();
                    foreach (Podatak pod in listaPodataka)
                    {

                        if (lista[i].Sifra.Equals(pod.sifraPod))
                        {
                            string[] pom = pod.datum.Split(' ');
                            s.Add(pom[1]);
                            //count[i]++;
                            //listaSuma[i] += pod.vrednost;
                        }
                    }

                    listaPoslednjihVremena.Add(lista[i].Sifra, s.Last());

                }
                List<string> listaDrz = new List<string>();
                foreach (string sif in dic.Keys)
                {
                    foreach (string l in listaPoslednjihVremena.Keys)
                    {
                        if (sif == l)
                        {
                            if (dic[sif] != listaPoslednjihVremena[l])
                            {
                                listaDrz.Add(sif);
                            }
                        }
                    }
                }



                for (int i = 0; i < listaDrz.Count; i++)
                {
                    count[i] = 0;

                    foreach (Podatak pod in listaPodataka)
                    {

                        if (listaDrz[i].Equals(pod.sifraPod))
                        {

                            count[i]++;
                            listaSuma[i] += pod.vrednost;
                        }
                    }

                    // listaPoslednjihVremena.Add(lista[i].Sifra, s.Last());

                }

                for (int i = 0; i < listaSuma.Count(); i++)
                {
                    listaRez.Add(listaSuma[i] / count[i]);
                }

            }
            m.UcitajUBazu(listaRez, listaPoslednjihVremena, DateTime.Now, 1);
        }

        
    }
}

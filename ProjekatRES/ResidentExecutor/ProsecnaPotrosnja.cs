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
            List<double> listaSuma = new List<double>();
            Dictionary<string, string> listaPoslednjihVremena = new Dictionary<string, string>();
            Dictionary<string, string> dic = m.IzlistajPoslednje(1);

            int[] count = new int[10];
            List<double> listaRez = new List<double>();

            if (!dic.Any())
            {
                for (int i = 0; i < lista.Count; i++)
                {
                    count[i] = 0;
                    listaSuma.Add(0);
                    List<string> s = new List<string>();
                    foreach (Podatak pod in listaPodataka)
                    {

                        if (lista[i].Sifra.Equals(pod.sifraPod))
                        {
                            string[] pom = pod.datum.Split(' ');
                            s.Add(pom[1]);
                            count[i]++;
                            double a = listaSuma.ElementAt(i) + pod.vrednost;
                            listaSuma[i] = a;
                            //listaSuma.Insert(i, a);
                        }
                    }

                    listaPoslednjihVremena.Add(lista[i].Sifra, s.Last());

                }

                for (int i = 0; i < listaSuma.Count(); i++)
                {
                    listaRez.Add(listaSuma.ElementAt(i) / count[i]);
                }


                m.UcitajUBazu(listaRez, listaPoslednjihVremena, DateTime.Now, 1);
            }
            else
            {

                for (int i = 0; i < lista.Count; i++)
                {
                    //count[i] = 0;
                    List<string> s = new List<string>();
                   // listaSuma.Add(0);
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

                Dictionary<string, string> li = new Dictionary<string, string>();
                
                for (int i = 0; i < listaDrz.Count; i++)
                {
                    count[i] = 0;
                    List<string> s = new List<string>();
                    listaSuma.Add(0);
                    int index = i;
                    foreach (Podatak pod in listaPodataka)
                    {

                        if (listaDrz[i].Equals(pod.sifraPod))
                        {

                            count[i]++;
                            
                            double a = listaSuma.ElementAt(index) + pod.vrednost;
                            listaSuma[index] = a;
                            //listaSuma.Insert(index, a);
                            // listaSuma += pod.vrednost;
                            string[] pom = pod.datum.Split(' ');
                            s.Add(pom[1]);
                           
                        }

                    }
                    li.Add(listaDrz[index], s.Last());
                    // listaPoslednjihVremena.Add(lista[i].Sifra, s.Last());

                }

                for (int i = 0; i < listaSuma.Count(); i++)
                {
                    listaRez.Add(listaSuma.ElementAt(i) / count[i]);
                }
                // listaPoslednjihVremena = li;
                m.UcitajUBazu(listaRez, li, DateTime.Now, 1);
            }
            
           
        }

        
    }
}

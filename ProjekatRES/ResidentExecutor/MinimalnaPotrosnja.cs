using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResidentExecutor
{
    public class MinimalnaPotrosnja : IFunkcije
    {
        public Metode m = new Metode();
        public void IzvrsiFunkciju()
        {

            List<Podrucje> lista = m.IzlistajPodrucja();
            List<Podatak> listaPodataka = m.IscitajIzBaze();
            Dictionary<string, string> dic = m.IzlistajPoslednje(2);
            List<double> listaVrednosti;
            Dictionary<string, string> listaPoslednjihVremena = new Dictionary<string, string>();



            List<double> listaRez = new List<double>();

            if (!dic.Any())
            {
                for (int i = 0; i < lista.Count; i++)
                {
                    
                    listaVrednosti = new List<double>();
                    List<string> s = new List<string>();
                    foreach (Podatak pod in listaPodataka)
                    {

                        if (lista[i].Sifra.Equals(pod.sifraPod))
                        {
                            string[] pom = pod.datum.Split(' ');
                            s.Add(pom[1]);


                            listaVrednosti.Add(pod.vrednost);
                        }
                    }
                    listaRez.Add(listaVrednosti.Min());
                    listaPoslednjihVremena.Add(lista[i].Sifra, s.Last());

                }

                m.UcitajUBazu(listaRez, listaPoslednjihVremena, DateTime.Now, 2);
            }
            else
            {

                for (int i = 0; i < lista.Count; i++)
                {
                    List<string> s = new List<string>();
                    foreach (Podatak pod in listaPodataka)
                    {

                        if (lista[i].Sifra.Equals(pod.sifraPod))
                        {
                            string[] pom = pod.datum.Split(' ');
                            s.Add(pom[1]);
                            
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
                    
                    List<string> s = new List<string>();
                    listaVrednosti = new List<double>();
                    foreach (Podatak pod in listaPodataka)
                    {

                        if (listaDrz[i].Equals(pod.sifraPod))
                        {
                            listaVrednosti.Add(pod.vrednost);
                            string[] pom = pod.datum.Split(' ');
                            s.Add(pom[1]);

                        }

                    }
                    listaRez.Add(listaVrednosti.Min());
                    li.Add(listaDrz[i], s.Last());
                    // listaPoslednjihVremena.Add(lista[i].Sifra, s.Last());

                }
                m.UcitajUBazu(listaRez, li, DateTime.Now, 2);
            }


        }
    }
}

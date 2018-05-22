using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class Metode
    {
        private DataIO serializer = new DataIO();
        public static BindingList<Podatak> podaci = new BindingList<Podatak>();
        public static BindingList<Podrucje> podrucja = new BindingList<Podrucje>();
        public static BindingList<PodatakZaProracu> prorPod = new BindingList<PodatakZaProracu>();


        public void UcitajUBazu1(double vrednost, string vreme, string podrucje)
        {
           
            var path = Path.GetDirectoryName(System.IO.Path.GetDirectoryName(Assembly.GetEntryAssembly().Location));
            path = Path.GetDirectoryName(path);
            path = Path.GetDirectoryName(path);
            string path1 = path + @"\Database\Areas.xml";
            podrucja = serializer.DeSerializeObject<BindingList<Podrucje>>(path1);
            string sifra = "";
            foreach(Podrucje p in podrucja)
            {
                if(p.Ime.Equals(podrucje))
                {
                    sifra = p.Sifra;
                }
            }

            string[] pom = vreme.Split(' ');
            string[] pom1 = pom[0].Split('.');
            string nazivXML = pom1[0] + pom1[1] + pom1[2];

            //path = path + @"\Database\EnteredData.xml";
            path = path + @"\Database\" + nazivXML + ".xml";

            if(File.Exists(path))
            {
                podaci = serializer.DeSerializeObject<BindingList<Podatak>>(path);
            }
           
           // string fileName = "EnteredData.xml";
           // string fullPath = Path.GetFullPath(fileName);
            podaci.Add(new Podatak(vrednost,vreme, sifra));
            serializer.SerializeObject<BindingList<Podatak>>(podaci, path);
        }

        public List<Podatak> IscitajIzBaze()
        {
            
            string datum = DateTime.Now.ToString();
            string[] pom = datum.Split(' ');
            string[] pom1 = pom[0].Split('-');
            string trazeniXML = pom1[2] +   pom1[1] +   pom1[0];

            //string trazeniXML = "19052018";

            var path = Path.GetDirectoryName(System.IO.Path.GetDirectoryName(Assembly.GetEntryAssembly().Location));
            path = Path.GetDirectoryName(path);
            path = Path.GetDirectoryName(path);

            path = path + @"\Database\" + trazeniXML + ".xml";
            
            
            if(File.Exists(path))
            {
                podaci = serializer.DeSerializeObject<BindingList<Podatak>>(path);
                return podaci.ToList();
            }
            else
            {
                return new List<Podatak>();
            }
            

        }

        public void UcitajUBazu(List<double> vred, Dictionary<string,string> vremena, DateTime time, int i)
        {
            prorPod = new BindingList<PodatakZaProracu>();
            var path = Path.GetDirectoryName(System.IO.Path.GetDirectoryName(Assembly.GetEntryAssembly().Location));
            path = Path.GetDirectoryName(path);
            path = Path.GetDirectoryName(path);

            if(i == 1)
            {
                path = path + @"\Database\ProsecnaPotrosnja.xml";
                
            }
            else if(i == 2)
            {
                path = path + @"\Database\MinimalnaPotrosnja.xml";
            }
            else if(i == 3)
            {
                path = path + @"\Database\MaksimalnaPotrosnja.xml";
            }


            PodatakZaProracu t = new PodatakZaProracu();
            t.ProracunataVrednost = -1;
            t.sifraPodrucja = "";
            t.VremePoslednjegUnosa = "";
            t.VremeProracuna = "";

            if (File.Exists(path))
            {
                prorPod = serializer.DeSerializeObject<BindingList<PodatakZaProracu>>(path);

            }
            else
            {
                
                prorPod.Add(t);
                serializer.SerializeObject<BindingList<PodatakZaProracu>>(prorPod, path);
                //serializer.SerializeObject<BindingList<PodatakZaProracu>>(prorPod,path);
                //File.Create(path);
            }
            List<string> sifre = new List<string>();
            List<string> vr = new List<string>();
            foreach (string ss in vremena.Keys)
            {
                sifre.Add(ss);
                vr.Add(vremena[ss]);
            }

            for (int j = 0; j < vred.Count; j++)
            {
                PodatakZaProracu pzp = new PodatakZaProracu();
                pzp.ProracunataVrednost = vred[j];
                pzp.sifraPodrucja = sifre[j];
                pzp.VremePoslednjegUnosa = vr[j];

                string dt = time.ToString();
                string[] dt1 = dt.Split(' ');
                string[] dt2 = dt1[0].Split('-');
                string[] dt3 = dt1[1].Split(':');

                pzp.VremeProracuna = dt2[2] +"." + dt2[1] + "." + dt2[0] + ". " + dt3[0] + ":" + dt3[1]; 

                prorPod.Add(pzp);
            }

            prorPod.Remove(t);
            serializer.SerializeObject<BindingList<PodatakZaProracu>>(prorPod, path);


        }

        public List<Podrucje> IzlistajPodrucja()
        {
            var path = Path.GetDirectoryName(System.IO.Path.GetDirectoryName(Assembly.GetEntryAssembly().Location));
            path = Path.GetDirectoryName(path);
            path = Path.GetDirectoryName(path);
            string path1 = path + @"\Database\Areas.xml";
            podrucja = serializer.DeSerializeObject<BindingList<Podrucje>>(path1);

            return podrucja.ToList();
        }
        public Dictionary<string,string> IzlistajPoslednje(int brojFunkcije)
        {
            var path = Path.GetDirectoryName(System.IO.Path.GetDirectoryName(Assembly.GetEntryAssembly().Location));
            path = Path.GetDirectoryName(path);
            path = Path.GetDirectoryName(path);
            string path1 ="";
            if (brojFunkcije == 1)
            {
                 path1 = path + @"\Database\ProsecnaPotrosnja.xml";
            }
            else if(brojFunkcije == 2)
            {
                 path1 = path + @"\Database\MinimalnaPotrosnja.xml";
            }
            else
            {
                 path1 = path + @"\Database\MaksimalnaPotrosnja.xml";
            }
            BindingList<PodatakZaProracu> b = new BindingList<PodatakZaProracu>();

            if (File.Exists(path1))
            {
               b = serializer.DeSerializeObject<BindingList<PodatakZaProracu>>(path1);
            }
            //else
            //{
            //    File.Create(path1);
            //}
            //b = serializer.DeSerializeObject<BindingList<PodatakZaProracu>>(path1);

            List<PodatakZaProracu> k = new List<PodatakZaProracu>();
            Dictionary<string, string> dic = new Dictionary<string, string>();

            b.ToList();

            if(!b.Any())
            {
                return dic;
            }

            DateTime dt = DateTime.Now;
            string dt0 = dt.ToString();
            string[] dt1 = dt0.Split(' ');
            string[] dt2 = dt1[0].Split('-');
            

            string datumVreme = dt2[2] + "." + dt2[1] + "." + dt2[0] + ".";


            Dictionary<string, string> listaVremena ;

            for (int i = 0; i < podrucja.Count; i++)
            {
                listaVremena = new Dictionary<string, string>();

                foreach (PodatakZaProracu p in b)
                {
                    string[] pomocniDatum = p.VremeProracuna.Split(' ');
                    if (pomocniDatum[0].Equals(datumVreme))
                    {
                            if (p.sifraPodrucja.Equals(podrucja[i].Sifra))
                            {
                                if (listaVremena.ContainsKey(p.sifraPodrucja))
                                {
                                    listaVremena[p.sifraPodrucja] = p.VremePoslednjegUnosa;
                                }
                                else
                                {
                                    listaVremena.Add(p.sifraPodrucja, p.VremePoslednjegUnosa);
                                }
                            }
                    }

                }

                if(listaVremena.Any())
                {
                    dic.Add(listaVremena.Last().Key, listaVremena.Last().Value);
                }
                else
                {
                    dic.Add(podrucja[i].Sifra, "-1");
                   
                }
                
            }
            return dic;

        }
    }
}

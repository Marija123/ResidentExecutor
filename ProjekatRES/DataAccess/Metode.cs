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
            DateTime dt = DateTime.Now;
            string datumVreme = String.Format("{0:MM/dd/yyyy}", dt);
            //string datumVreme = DateTime.Now.ToString();
            //string[] pom = datumVreme.Split(' ');
            // string[] pom1 = pom[0].Split('/');


            string[] pom1;
            if(datumVreme.Contains('-'))
            {
                pom1 = datumVreme.Split('-');
            }
            else
            {
               pom1 = datumVreme.Split('/');
            }
            string trazeniXML = pom1[1] + pom1[0] + pom1[2];

            //string trazeniXML = "19052018";

            var path = Path.GetDirectoryName(System.IO.Path.GetDirectoryName(Assembly.GetEntryAssembly().Location));
            path = Path.GetDirectoryName(path);
            path = Path.GetDirectoryName(path);

            path = path + @"\Database\" + trazeniXML + ".xml";
            
            podaci = serializer.DeSerializeObject<BindingList<Podatak>>(path);

            return podaci.ToList();

        }

        public void UcitajUBazu(List<double> vred, Dictionary<string,string> vremena, DateTime time, int i)
        {
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

            if (File.Exists(path))
            {
                prorPod = serializer.DeSerializeObject<BindingList<PodatakZaProracu>>(path);
            }
            List<string> sifre = new List<string>();
            List<string> vr = new List<string>();
            foreach(string ss in vremena.Keys)
            {
                sifre.Add(ss);
                vr.Add(vremena[ss]);
            }

            for(int j = 0; j < vred.Count; j++)
            {
                PodatakZaProracu pzp = new PodatakZaProracu();
                pzp.ProracunataVrednost = vred[j];
                pzp.sifraPodrucja =sifre[j];
                pzp.VremePoslednjegUnosa = vr[j];

                string dt = String.Format("{0:MM/dd/yyyy}", time);
                string dt1 = String.Format("{0: HH}", time);
                string dt2 = String.Format("{0: mm}" , time);

                pzp.VremeProracuna = dt + " " + dt1 + ":" + dt2.Trim();

                prorPod.Add(pzp);
            }
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
        public Dictionary<string,string> IzlistajPoslednjeProsecne()
        {
            var path = Path.GetDirectoryName(System.IO.Path.GetDirectoryName(Assembly.GetEntryAssembly().Location));
            path = Path.GetDirectoryName(path);
            path = Path.GetDirectoryName(path);
            string path1 = path + @"\Database\ProsecnaPotrosnja.xml";
            BindingList<PodatakZaProracu> b = new BindingList<PodatakZaProracu>();
            b = serializer.DeSerializeObject<BindingList<PodatakZaProracu>>(path1);

            List<PodatakZaProracu> k = new List<PodatakZaProracu>();
            Dictionary<string, string> dic = new Dictionary<string, string>();

            b.ToList();

            if(!b.Any())
            {
                return dic;
            }

            DateTime dt = DateTime.Now;
            string datumVreme = String.Format("{0:MM/dd/yyyy}", dt);
            //string[] pom1;
            //if (datumVreme.Contains('-'))
            //{
            //    pom1 = datumVreme.Split('-');
            //}
            //else
            //{
            //    pom1 = datumVreme.Split('/');
            //}
            //string trazeniDatum = pom1[0]  + "-" + pom1[1] + "-" + pom1[2];
            Dictionary<string,string> listaVremena = new Dictionary<string, string>();

            for (int i = 0; i < podrucja.Count; i++)
            {

                foreach (PodatakZaProracu p in b)
                {
                    if ((p.VremeProracuna.Substring(0,10)).Equals(datumVreme))
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
                dic.Add(listaVremena.Last().Key,listaVremena.Last().Value);
            }
            return dic;

        }
    }
}

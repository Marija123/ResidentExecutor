using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;

namespace ResidentExecutor
{
    class Program
    {
        public static List<int> l; 
        static void Main(string[] args)
        {
            IFunkcije funkcija;

            do
            {
                IscitajXML();

                foreach (int i in l)
                {
                   if(i == 1)
                    {
                        funkcija = new ProsecnaPotrosnja();
                        
                    }
                   else if(i == 2)
                    {
                        funkcija = new MinimalnaPotrosnja();
                    }
                    else
                    {
                        funkcija = new MaksimalnaPotrosnja();
                    }

                    funkcija.IzvrsiFunkciju();
                
                }
                Console.WriteLine("Izvrsene funkcije.");
                Thread.Sleep(10000);
            } while (true);

        }

        public static void IscitajXML()
        {
            l = new List<int>();
            var path = Path.GetDirectoryName(System.IO.Path.GetDirectoryName(Assembly.GetEntryAssembly().Location));
            path = Path.GetDirectoryName(path);
            path = Path.GetDirectoryName(path);
            string path1 = path + @"\FileSystem\rezidentne_funkcije.xml";

            XmlDocument doc = new XmlDocument();
            doc.Load(path1);
            foreach (XmlNode node in doc.DocumentElement.ChildNodes)
            {
                string text = node.InnerText; //or loop through its children as well
                if (text.Substring(1).Equals("1"))
                {
                    l.Add(Int32.Parse(text.Substring(0, 1)));
                }
               
            }
        }

    }
}

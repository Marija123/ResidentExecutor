﻿using System;
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
        public void UcitajUBazu1(double vrednost, string vreme)
        {
           
            var path = Path.GetDirectoryName(System.IO.Path.GetDirectoryName(Assembly.GetEntryAssembly().Location));
            path = Path.GetDirectoryName(path);
            path = Path.GetDirectoryName(path);
            path = path + @"\DataAccess\EnteredData.xml";
            podaci = serializer.DeSerializeObject<BindingList<Podatak>>(path);
           // string fileName = "EnteredData.xml";
           // string fullPath = Path.GetFullPath(fileName);
            podaci.Add(new Podatak(vrednost,vreme));
            serializer.SerializeObject<BindingList<Podatak>>(podaci, path);
        }
    }
}

using DataAccess;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace UserInterface
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Metode m = new Metode();
        public static List<Podatak> lista;
        public static List<Podrucje> podrucja; 
        public MainWindow()
        {
            InitializeComponent();

            string datum = DateTime.Now.ToString();
            string[] pom = datum.Split(' ');
            string[] pom1 = pom[0].Split('-');
            danasnjiDatum.Content = pom1[2] + "." + pom1[1] + "." + pom1[0] + ".";
            sat.ItemsSource = Liste.sati;
            minut.ItemsSource = Liste.minuti;
            podrucjeBoxS.ItemsSource = Liste.podrucja;
        }

        public static double v = 0;
        public static string vreme = "";
       

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if(validate())
            {

                m.UcitajUBazu1(v, vreme, podrucjeBoxS.SelectedValue.ToString());
            }
           
            
        }

        private bool validate()
        {
            string sifra = "";
            bool retVal = true;
            podrucja = m.IzlistajPodrucja();


            if (podrucjeBoxS.SelectedItem == null)
            {
                retVal = false;
                podrucjeGreska.Content = "Morate da odaberete podrucje!";
                // podrucjeBoxS.BorderThickness = new Thickness(3);
                // podrucjeBoxS.BorderBrush = Brushes.Red;

            }
            else
            {

                podrucjeGreska.Content = "";
                // podrucjeBoxS.BorderBrush = Brushes.Gray;


                
                foreach (Podrucje pod in podrucja)
                {
                    if (pod.Ime.Equals(podrucjeBoxS.SelectedValue.ToString()))
                    {
                        sifra = pod.Sifra;
                    }
                }
            }
            lista = m.IscitajIzBaze();

            if (vrednosText.Text.Trim().Equals(""))
            {
                retVal = false;
                vrednostGreska.Content = "Polje ne sme biti prazno!";
                //vrednosText.BorderBrush = Brushes.Red;
            }
            else if (Double.TryParse(vrednosText.Text, out v))
            {
                if (v < 0)
                {
                    retVal = false;
                    vrednostGreska.Content = "Vrednost ne moze biti negativna!";
                  //  vrednosText.BorderBrush = Brushes.Red;
                }
                else
                {
                    vrednostGreska.Content = "";
                    //vrednosText.BorderBrush = Brushes.Gray;
                }
               

            }
            else
            {
                retVal = false;
                vrednostGreska.Content = "Vrednost mora biti broj!";
               // vrednosText.BorderBrush = Brushes.Red;
            }



            if(sat.SelectedItem == null || minut.SelectedItem == null)
            {
                retVal = false;
                vremeGreska.Content = "Polja ne smeju biti prazna!";
               // sat.BorderBrush = Brushes.Red;
               // sat.BorderThickness = new Thickness(3);
               //minut.BorderBrush = Brushes.Red;
               //minut.BorderThickness = new Thickness(3);

               
            }
            else
            {
                if (sat.SelectedValue.Equals(25))
                {
                    if(!danasnjiDatum.Equals("31.03.2019.") || !danasnjiDatum.Equals("29.03.2020."))
                    {
                        retVal = false;
                        vremeGreska.Content = "Vreme za navedeni datum ne postoji!";
                        //sat.BorderBrush = Brushes.Red;
                        //sat.BorderThickness = new Thickness(3);
                        //minut.BorderBrush = Brushes.Red;
                        //minut.BorderThickness = new Thickness(3);
                    }
                    else if(danasnjiDatum.Equals("28.10.2018.") || danasnjiDatum.Equals("27.10.2019.") || danasnjiDatum.Equals("25.10.2020."))
                    {
                        retVal = false;
                        vremeGreska.Content = "Vreme za navedeni datum ne postoji!";
                    }
                    else
                    {
                        vremeGreska.Content = "";
                        //sat.BorderBrush = Brushes.Gray; 
                        //minut.BorderBrush = Brushes.Gray;
                        vreme = danasnjiDatum.Content + " " + sat.SelectedValue.ToString() + ":" + minut.SelectedValue.ToString();

                    }
                }
                else if (sat.SelectedValue.Equals(24))
                {
                    if (danasnjiDatum.Equals("28.10.2018.") || danasnjiDatum.Equals("27.10.2019.") || danasnjiDatum.Equals("25.10.2020."))
                    {
                        retVal = false;
                        vremeGreska.Content = "Vreme za navedeni datum ne postoji!";
                        //sat.BorderBrush = Brushes.Red;
                        //sat.BorderThickness = new Thickness(3);
                        //minut.BorderBrush = Brushes.Red;
                        //minut.BorderThickness = new Thickness(3);
                    }
                    else
                    {
                        vremeGreska.Content = "";
                        //sat.BorderBrush = Brushes.Gray;
                        //minut.BorderBrush = Brushes.Gray;
                        vreme = danasnjiDatum.Content + " " + sat.SelectedValue.ToString() + ":" + minut.SelectedValue.ToString();
                    }
                }
                else
                {
                    vremeGreska.Content = "";
                    vreme = danasnjiDatum.Content + " " + sat.SelectedValue.ToString() + ":" + minut.SelectedValue.ToString();
                }



            }
            foreach (Podatak q in lista)
            {
                string[] pom2 = q.datum.Split(' ');
                string[] pom3 = pom2[1].Split(':');
                int s = int.Parse(pom3[0]);
                int m = int.Parse(pom3[1]);
                if(q.sifraPod.Equals(sifra))
                {
                    if (q.datum.Equals(vreme))
                    {
                        vremeGreska.Content = "Vrednosti za dato vreme i datu drzavu su vec unete";

                        retVal = false;
                    }
                    if (s > int.Parse(sat.SelectedValue.ToString()))
                    {
                        vremeGreska.Content = "Nije moguce uneti podatke za to vreme";
                        retVal = false;
                    }
                    if (s == int.Parse(sat.SelectedValue.ToString()) &&  m >  int.Parse(minut.SelectedValue.ToString()))
                    {
                        vremeGreska.Content = "Nije moguce uneti podatke za to vreme";
                        retVal = false;
                    }
                }
                
                
            }


            return retVal;
        }



    }
}

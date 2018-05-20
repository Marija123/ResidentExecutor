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
           
            dan.ItemsSource = Liste.dani;
            mesec.ItemsSource = Liste.meseci;
            sat.ItemsSource = Liste.sati;
            minut.ItemsSource = Liste.minuti;
            podrucjeBoxS.ItemsSource = Liste.podrucja;
        }

        public static double v = 0;
        public static string datumVreme = "";
       

        private void Button_Click(object sender, RoutedEventArgs e)
        {
           


            if(validate())
            {
               
                //int dan1 = int.Parse(dan.SelectedValue.ToString());
                //if(dan1<10)
                //{
                //    datumVreme = "0" + dan.SelectedValue.ToString();
                //}
                //else
                //{
                //    datumVreme = dan.SelectedValue.ToString();
                //}

                //datumVreme = datumVreme + ".";

                //int mesec1 = int.Parse(mesec.SelectedValue.ToString());
                //if (mesec1 < 10)
                //{
                //    datumVreme = datumVreme + "0" + mesec.SelectedValue.ToString();
                //}
                //else
                //{
                //    datumVreme = datumVreme + mesec.SelectedValue.ToString();
                //}

                //datumVreme = datumVreme + "." + godina.Text.ToString();
                //datumVreme = datumVreme + " " + sat.SelectedValue.ToString() + ":" + minut.SelectedValue.ToString();

               


                m.UcitajUBazu1(v, datumVreme, podrucjeBoxS.SelectedValue.ToString());
            }
           
            
        }

        private bool validate()
        {
            bool retVal = true;
            podrucja = m.IzlistajPodrucja();

            string sifra = "";
            foreach (Podrucje pod in podrucja)
            {
                if (pod.Ime.Equals(podrucjeBoxS.SelectedValue.ToString()))
                {
                    sifra = pod.Sifra;
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

            if(podrucjeBoxS.SelectedItem == null)
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
            }

            if (dan.SelectedItem == null || mesec.SelectedItem == null || godina.Text.Trim().Equals(""))
            {

                retVal = false;
                datumGreska.Content = "Polja ne smeju biti prazna!";
               // dan.BorderBrush = Brushes.Red;
               // dan.BorderThickness = new Thickness(3);
               // mesec.BorderBrush = Brushes.Red;
               //mesec.BorderThickness = new Thickness(3);
               // godina.BorderBrush = Brushes.Red;

            }
            else
            {
                int br;
                if(Int32.TryParse(godina.Text, out br))
                {
                    if (br < 1900 || br>2018)
                    {
                        retVal = false;
                        datumGreska.Content = "Godina mora da bude izmedju 1900 i 2018!";
                       // godina.BorderBrush = Brushes.Red;
                    }
                    else
                    {

                        if (mesec.SelectedValue.Equals(4) || mesec.SelectedValue.Equals(6) || mesec.SelectedValue.Equals(9) || mesec.SelectedValue.Equals(11))
                        {
                            if (dan.SelectedValue.Equals(31))
                            {
                                retVal = false;
                                datumGreska.Content = "Selektovani mesec nema 31 dan!";
                                //dan.BorderThickness = new Thickness(3);
                                //dan.BorderBrush = Brushes.Red;
                            }
                            else
                            {
                                datumGreska.Content = "";
                                //dan.BorderBrush = Brushes.Gray;

                                //mesec.BorderBrush = Brushes.Gray;

                                //godina.BorderBrush = Brushes.Gray;
                            }
                        }
                        else if(mesec.SelectedValue.Equals(2))
                        {
                            if((br % 4 == 0) && ((br%100 != 0) || (br % 400 == 0)))
                            {
                                if(dan.SelectedValue.Equals(30) || dan.SelectedValue.Equals(31))
                                {
                                    retVal = false;
                                    datumGreska.Content = "Selektovani mesec ima samo 29 dana!";
                                    //dan.BorderThickness = new Thickness(3);
                                    //dan.BorderBrush = Brushes.Red;
                                }
                                else
                                {
                                    datumGreska.Content = "";
                                    //dan.BorderBrush = Brushes.Gray;

                                    //mesec.BorderBrush = Brushes.Gray;

                                    //godina.BorderBrush = Brushes.Gray;
                                }
                            }
                            else
                            {
                                if (dan.SelectedValue.Equals(29) ||dan.SelectedValue.Equals(30) || dan.SelectedValue.Equals(31) )
                                {
                                    retVal = false;
                                    datumGreska.Content = "Selektovani mesec ima samo 28 dana!";
                                    //dan.BorderThickness = new Thickness(3);
                                    //dan.BorderBrush = Brushes.Red;
                                }
                                else
                                {
                                    datumGreska.Content = "";
                                    //dan.BorderBrush = Brushes.Gray;

                                    //mesec.BorderBrush = Brushes.Gray;

                                    //godina.BorderBrush = Brushes.Gray;
                                }
                            }
                            
                        }
                    }
                
                }
                else
                {
                    retVal = false;
                    datumGreska.Content = "Godina mora biti broj!";
                   // godina.BorderBrush = Brushes.Red;
                }
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
                    if(!mesec.SelectedValue.Equals(3) || !dan.SelectedValue.Equals(26))
                    {
                        retVal = false;
                        vremeGreska.Content = "Vreme za navedeni datum ne postoji!";
                        //sat.BorderBrush = Brushes.Red;
                        //sat.BorderThickness = new Thickness(3);
                        //minut.BorderBrush = Brushes.Red;
                        //minut.BorderThickness = new Thickness(3);
                    }
                    else if(mesec.SelectedValue.Equals(10) && dan.SelectedValue.Equals(29))
                    {
                        retVal = false;
                        vremeGreska.Content = "Vreme za navedeni datum ne postoji!";
                    }
                    else
                    {
                        vremeGreska.Content = "";
                        //sat.BorderBrush = Brushes.Gray; 
                        //minut.BorderBrush = Brushes.Gray;
                       
                    }
                }
                else if (sat.SelectedValue.Equals(24))
                {
                    if (mesec.SelectedValue.Equals(10) && dan.SelectedValue.Equals(29))
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

                    }
                }
                else
                {
                    vremeGreska.Content = "";
                }



            }



            int dan1 = int.Parse(dan.SelectedValue.ToString());
            if (dan1 < 10)
            {
                datumVreme = "0" + dan.SelectedValue.ToString();
            }
            else
            {
                datumVreme = dan.SelectedValue.ToString();
            }

            datumVreme = datumVreme + ".";

            int mesec1 = int.Parse(mesec.SelectedValue.ToString());
            if (mesec1 < 10)
            {
                datumVreme = datumVreme + "0" + mesec.SelectedValue.ToString();
            }
            else
            {
                datumVreme = datumVreme + mesec.SelectedValue.ToString();
            }

            datumVreme = datumVreme + "." + godina.Text.ToString();
            datumVreme = datumVreme + " " + sat.SelectedValue.ToString() + ":" + minut.SelectedValue.ToString();


            foreach (Podatak q in lista)
            {
                if (q.datum.Equals(datumVreme) && q.sifraPod.Equals(sifra))
                {
                    MessageBox.Show("Vrednosti za dato vreme i datu drzavu su vec unete");
                    retVal = false;
                }
            }


            return retVal;
        }



    }
}

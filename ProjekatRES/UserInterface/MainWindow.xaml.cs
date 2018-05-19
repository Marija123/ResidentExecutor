using DataAccess;
using System;
using System.Collections.Generic;
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
                
                m.UcitajUBazu1(v, datumVreme, podrucjeBoxS.SelectedValue.ToString());
            }
           
            
        }

        private bool validate()
        {
            bool retVal = true;

            if(vrednosText.Text.Trim().Equals(""))
            {
                retVal = false;
                vrednostGreska.Content = "Polje ne sme biti prazno!";
                vrednosText.BorderBrush = Brushes.Red;
            }
            else if (Double.TryParse(vrednosText.Text, out v))
            {
                if (v < 0)
                {
                    retVal = false;
                    vrednostGreska.Content = "Vrednost ne moze biti negativna!";
                    vrednosText.BorderBrush = Brushes.Red;
                }
                else
                {
                    vrednostGreska.Content = "";
                    vrednosText.BorderBrush = Brushes.Gray;
                }
               

            }
            else
            {
                retVal = false;
                vrednostGreska.Content = "Vrednost moze biti samo broj!";
                vrednosText.BorderBrush = Brushes.Red;
            }

            if(podrucjeBoxS.SelectedItem == null)
            {
                retVal = false;
                podrucjeGreska.Content = "Morate da odaberete podrucje!";
                podrucjeBoxS.BorderBrush = Brushes.Red;
                podrucjeBoxS.BorderThickness = new Thickness(3);
            }
            else
            {
                
                podrucjeGreska.Content = "";
                podrucjeBoxS.BorderBrush = Brushes.Gray;
            }



            return retVal;
        }

        public bool proveriVrednost()
        {
            bool retVal = false;
           

            return retVal;
           
        }


    }
}

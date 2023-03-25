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
using System.Windows.Shapes;
using System.Windows.Forms;
using System.Windows.Diagnostics;
using System.Windows.Navigation;
using System.Collections.ObjectModel;
using System.Windows.Controls.Primitives;
using System.IO;

namespace CMS
{
    /// <summary>
    /// Interaction logic for TabelarniPrikaz.xaml
    /// </summary>
    public partial class TabelarniPrikaz : Window
    {
        private DataIO serializer = new DataIO();

         public static BindingList<Prsten> Prstenovi { get; set; }
       
        
        public TabelarniPrikaz()
        {
            
             Prstenovi = serializer.DeSerializeObject<BindingList<Prsten>>("prstenovi.xml");
         
            if (Prstenovi == null) {

                Prstenovi = new BindingList<Prsten>();   
               
            }

            
            DataContext = this;
           
            InitializeComponent();
          

        }

        private void DugmeZaDodavanje_Click(object sender, RoutedEventArgs e)
        {
             FormaWindow formaWindow = new FormaWindow();
             formaWindow.ShowDialog();
            //IzmjenaWindow izmena = new IzmjenaWindow();
            //izmena.ShowDialog();
        }

       

        private void DugmeZaIzlaz_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void DugmeZaBrisanje_Click(object sender, RoutedEventArgs e)
        {
            
             List<Prsten> Selected = new List<Prsten>();
            Console.WriteLine("Broj prstenova u listi prije cekiranja:{0}", Prstenovi.Count());
            for (int i = 0; i < Prikaz.SelectedItems.Count; i++)
             {
                
                 Selected.Add((Prsten)Prikaz.SelectedItems[i]);
                Console.WriteLine("Cekirano je:{0}", Prstenovi.Count);
                Prstenovi.RemoveAt(i);
                
                
                Console.WriteLine("Obrisano je:{0}", Prstenovi.Count);

             }
           

            //var pom = Prikaz.ItemsSource as List<Prsten>;
              foreach (var item in Selected)
              {
                  Prstenovi.Remove(item);

                 Console.WriteLine("Kraaaj:{0}", Prstenovi.Count);

              }

              Prikaz.Items.Refresh();
           
            
           
        }



        private void Hyperlink_Click_1(object sender, RoutedEventArgs e)
        {
            if (LogIn.TipKorisnika.Equals(LogIn.Admin))
            {
                Prsten novi = Prstenovi.ElementAt(Prikaz.SelectedIndex);
                IzmjenaWindow izmjena = new IzmjenaWindow(novi);
                izmjena.ShowDialog();
            }
            else if (LogIn.TipKorisnika.Equals(LogIn.Korisnik))
            {
                Prsten novi = Prstenovi.ElementAt(Prikaz.SelectedIndex);
                PrikaziWindow p = new PrikaziWindow(novi);
                p.ShowDialog();

            }
            else {
                System.Windows.MessageBox.Show("Pogresno korisniko ime ili lozinka");
            }
            /*
            if (LogIn.Admin == "admin" && LogIn.Password == "admin")
            {
                Prsten novi = Prstenovi.ElementAt(Prikaz.SelectedIndex);
                IzmjenaWindow izmjena = new IzmjenaWindow(novi);
                izmjena.ShowDialog();
            }
            else if (LogIn.Korisnik == "Dajana" && LogIn.Sifra == "Radovic")
            {
                Prsten novi = Prstenovi.ElementAt(Prikaz.SelectedIndex);
                PrikaziWindow p = new PrikaziWindow(novi);
                p.ShowDialog();

            }
            else {
                System.Windows.MessageBox.Show("Pogresno korisniko ime ili lozinka");
            }



            
            if (LogIn.Admin == "admin" && LogIn.Password == "admin") {
               
                // IzmjenaWindow izmjena = new IzmjenaWindow(novi);
                // izmjena.ShowDialog();
                Prsten novi = Prstenovi.ElementAt(Prikaz.SelectedIndex);
                PrikaziWindow p = new PrikaziWindow(novi);
                p.ShowDialog();

            }
            else if (LogIn.Korisnik == "Dajana" && LogIn.Sifra == "Radovic") {

                PrikaziWindow p = new PrikaziWindow();
                p.ShowDialog();
            }*/


        }
        
        private void save(object sender, CancelEventArgs e)
        {
            serializer.SerializeObject<BindingList<Prsten>>(Prstenovi, "prstenovi.xml"); 
            
        }

        
    }   
}

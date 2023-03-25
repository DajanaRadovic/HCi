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
using System.Windows.Shapes;

namespace CMS
{
    /// <summary>
    /// Interaction logic for PrikaziWindow.xaml
    /// </summary>
    public partial class PrikaziWindow : Window
    {
        public PrikaziWindow()
        {
            InitializeComponent();
        }

        public PrikaziWindow(Prsten prsten) {
            InitializeComponent();

            labelId.Content = Convert.ToString(prsten.Id);
            labeMetal.Content = prsten.Metal;
            labelBoja.Content = prsten.Boja;
            labelBrend.Content = prsten.Brend;
            labelOblik.Content = prsten.Oblik;
            labelCena.Content = prsten.Cena;
            labelDatum.Content = prsten.Datum;
            image.Source = new BitmapImage(new Uri(prsten.Slika));
        
        }
    }
}

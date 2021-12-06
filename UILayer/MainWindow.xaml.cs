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
using BusinessLayer.Managers;
using BusinessLayer.Model;

namespace UILayer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private BestellingManager bestellingManager;
        private Bestelling bestelling;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void PlaatsBestellingButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void SelecteerTruitjeButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void SelecteerKlantButton_Click(object sender, RoutedEventArgs e)
        {
            SelecteerKlant w = new SelecteerKlant();
            if(w.ShowDialog() == true)
            {
                bestelling = new Bestelling();
                bestelling.ZetKlant(w.Klant);
                PrijsTextBox.Text = bestelling.Kostprijs().ToString();
                KlantTextBox.Text = w.Klant.ToString();
                MessageBox.Show(w.Klant.Korting().ToString());
                //CheckBestelling();
            }
        }
        private void SelecteerKlantAanpassen_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ZoekBestellingAanpassenButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void MenuItemDelete_Click(object sender, RoutedEventArgs e)
        {

        }
        private void MenuItemUpdate_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}

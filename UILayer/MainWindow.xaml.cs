using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Configuration;
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
using BusinessLayer.Interfaces;
using BusinessLayer.Managers;
using BusinessLayer.Model;
using DataLayer;

namespace UILayer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static string ConnectionString = @"Data Source=DESKTOP-R7T8D5F\SQLEXPRESS;Initial Catalog=VerkoopVoetbalTrui;Integrated Security=True";
        private static IBestellingRepository BestelRepo = new BestellingDatabeheer(ConnectionString);
        private BestellingManager bestellingManager = new BestellingManager(BestelRepo);
        private Bestelling bestelling;
        private ObservableCollection<TruitjeData> truitjes = new ObservableCollection<TruitjeData>();

        public MainWindow()
        {
            InitializeComponent();
            bestelling = new Bestelling();
            DataGridTextColumn c1 = new DataGridTextColumn();
            c1.Header = "Truitje";
            c1.IsReadOnly = true;
            c1.Binding = new Binding("Truitje");
            BestellingTruitjes.Columns.Add(c1);

            DataGridTextColumn c2 = new DataGridTextColumn();
            c2.Header = "Aantal";
            c2.IsReadOnly = false;
            c2.Binding = new Binding("Aantal");
            BestellingTruitjes.Columns.Add(c2);
            BestellingTruitjes.AutoGenerateColumns = false;
            BestellingTruitjes.ItemsSource = truitjes;
        }

        private void PlaatsBestellingButton_Click(object sender, RoutedEventArgs e)
        {
            //checks
            //klanten niet leeg
            if (!string.IsNullOrEmpty(KlantTextBox.Text))
            {
                if(BestellingTruitjes.Items.Count >0)
                {
                    try
                    {
                        if (BetaaldCheckBox.IsChecked == true) {
                            bestelling.ZetBetaald(true);
                        } else {
                            bestelling.ZetBetaald(false);
                        }
                        DateTime nu = DateTime.Now;
                        bestelling.ZetTijdstip(nu);
                        bestelling.ZetPrijs(double.Parse(PrijsTextBox.Text));
                        bestellingManager.VoegBestellingToe(bestelling);
                    }catch(Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }
                    
                }
                else
                {
                    MessageBox.Show("Gelieve truitjes in uw bestelling toe te voegen");
                }
            }
            else
            {
                MessageBox.Show("Gelieve eerst een klant te kiezen");
            }
        }

        private void SelecteerTruitjeButton_Click(object sender, RoutedEventArgs e)
        { 
            BestellingSelecteerTruitje w = new BestellingSelecteerTruitje();
            if (w.ShowDialog() == true)
            {
                
                bestelling.VoegProductToe(w.voetbaltruitje, 1);
                TruitjeData td = new TruitjeData(w.voetbaltruitje, 1);
                td.PropertyChanged += TruitjeData_PropertyChanged;
                truitjes.Add(td);
                //CheckBestelling();
                PrijsTextBox.Text = bestelling.Kostprijs().ToString();

            }
        }

        private void TruitjeData_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            TruitjeData t = (TruitjeData)sender;
            int delta = t.Aantal - bestelling.GeefProducten()[t.Truitje];
            if(delta < 0)
            {
                bestelling.VerwijderProduct(t.Truitje, Math.Abs(delta));
            }
            if(delta > 0)
            {
                bestelling.VoegProductToe(t.Truitje, delta);
            }

            PrijsTextBox.Text = bestelling.Kostprijs().ToString();
            //CheckBestelling();
        }
        private void SelecteerKlantButton_Click(object sender, RoutedEventArgs e)
        {
            SelecteerKlant w = new SelecteerKlant();
            if (w.ShowDialog() == true)
            {
                bestelling = new Bestelling();
                bestelling.ZetKlant(w.Klant);
                PrijsTextBox.Text = bestelling.Kostprijs().ToString();
                KlantTextBox.Text = w.Klant.ToString();
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




        private void DataGridMenuItemDelete_Click(object sender, RoutedEventArgs e)
        {

        }

        
    }
}
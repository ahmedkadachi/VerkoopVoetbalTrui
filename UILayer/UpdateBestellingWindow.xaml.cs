using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using BusinessLayer.Interfaces;
using BusinessLayer.Managers;
using BusinessLayer.Model;
using DataLayer;

namespace UILayer
{
    /// <summary>
    /// Interaction logic for UpdateBestellingWindow.xaml
    /// </summary>
    public partial class UpdateBestellingWindow : Window
    {
        private static string ConnectionString = @"Data Source=DESKTOP-R7T8D5F\SQLEXPRESS;Initial Catalog=VerkoopVoetbalTrui;Integrated Security=True";
        private static IBestellingRepository BestelRepo = new BestellingDatabeheer(ConnectionString);
        private BestellingManager bestellingManager = new BestellingManager(BestelRepo);
        public Bestelling bestelling { get; private set; }
        private ObservableCollection<TruitjeData> truitjes = new ObservableCollection<TruitjeData>();
        private Dictionary<Voetbaltruitje,int> truiLijst = new Dictionary<Voetbaltruitje,int>();


        public UpdateBestellingWindow(Bestelling bestelling)
        {
            InitializeComponent();
            this.bestelling = bestelling;
            
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
            Init();
        }

        private void Init()
        {
            KlantTextBox.Text = bestelling.Klant.ToString();
            PrijsTextBox.Text = bestelling.Prijs.ToString();
            if (bestelling.Betaald)
            {
                BetaaldCheckBox.IsChecked = true;
            }
            else
            {
                BetaaldCheckBox.IsChecked = false;
            }
            datumDatePicker.SelectedDate = bestelling.Tijdstip;
            truiLijst = bestelling.TruiBestelling;
            foreach (KeyValuePair<Voetbaltruitje,int> element in truiLijst)
            {
                TruitjeData td = new TruitjeData(element.Key, element.Value);
                td.PropertyChanged += TruitjeData_PropertyChanged;
                truitjes.Add(td);
            }

            
            BestellingTruitjes.ItemsSource = truitjes;

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
            if (delta < 0)
            {
                bestelling.VerwijderProduct(t.Truitje, Math.Abs(delta));
            }
            if (delta > 0)
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
                bestelling.ZetKlant(w.Klant);
                PrijsTextBox.Text = bestelling.Kostprijs().ToString();
                KlantTextBox.Text = w.Klant.ToString();
                //CheckBestelling();
            }
        }
        private void DataGridMenuItemDelete_Click(object sender, RoutedEventArgs e)
        {
            TruitjeData trui = new TruitjeData(truitjes[BestellingTruitjes.SelectedIndex].Truitje, truitjes[BestellingTruitjes.SelectedIndex].Aantal);
            bestelling.VerwijderProduct(trui.Truitje,trui.Aantal);
            truitjes.Remove(trui);
            //BestellingTruitjes.ItemsSource = truitjes;
            //BestellingTruitjes.Items.Refresh();
            BestellingTruitjes.ItemsSource = null;
            BestellingTruitjes.ItemsSource = truitjes;
        }
    }
}

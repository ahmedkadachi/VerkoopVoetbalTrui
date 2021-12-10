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
using BusinessLayer.Interfaces;
using BusinessLayer.Managers;
using BusinessLayer.Model;
using DataLayer;

namespace UILayer
{
    /// <summary>
    /// Interaction logic for KlantWijzigWindow.xaml
    /// </summary>
    
    public partial class KlantWijzigWindow : Window
    {
        private static string ConnectionString = @"Data Source=DESKTOP-R7T8D5F\SQLEXPRESS;Initial Catalog=VerkoopVoetbalTrui;Integrated Security=True";
        private static IKlantRepository KlantRepo = new KlantDatabeheer(ConnectionString);
        private KlantManager Manager = new KlantManager(KlantRepo);
        private Klant klant { get; set; }
        public KlantWijzigWindow( Klant klant)
        {
            InitializeComponent();
            this.klant = klant;
            WijzigKlantNaamTextBox.Text = klant.Naam;
            WijzigKlantAdresTextBox.Text = klant.Adres;
        }

        private void WijzigKlantButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(WijzigKlantNaamTextBox.Text))
                {
                    if (!string.IsNullOrEmpty(WijzigKlantAdresTextBox.Text))
                    {
                        klant.ZetNaam(WijzigKlantNaamTextBox.Text);
                        klant.ZetAdres(WijzigKlantAdresTextBox.Text);
                        Manager.UpdateKlant(klant);
                        DialogResult = true;
                        Close();
                    }
                    else
                    {
                        MessageBox.Show("Gelieve een geldig adres in te voeren!");
                    }
                }
                else
                {
                    MessageBox.Show("Gelieve een geldig naam in te voeren!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            
        }
    }
}

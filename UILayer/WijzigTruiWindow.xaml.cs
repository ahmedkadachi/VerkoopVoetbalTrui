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
    /// Interaction logic for WijzigTruiWindow.xaml
    /// </summary>
    public partial class WijzigTruiWindow : Window
    {
        private static string ConnectionString = @"Data Source=DESKTOP-R7T8D5F\SQLEXPRESS;Initial Catalog=VerkoopVoetbalTrui;Integrated Security=True";
        private static IVoetbaltruitjeRepository TruiRepo = new VoetbaltruitjeDatabeheer(ConnectionString);
        private VoetbaltruitjeManager Manager = new VoetbaltruitjeManager(TruiRepo);
        private Voetbaltruitje Truitje { get; set; }
        public WijzigTruiWindow(Voetbaltruitje truitje)
        {
            InitializeComponent();
            this.Truitje = truitje;
            List<string> maten = Enum.GetNames(typeof(Kledingmaat)).ToList();
            maten.Insert(0, "<alles>");
            WijzigSelecteerMaatComboBox.ItemsSource = maten;
            WijzigSelecteerMaatComboBox.SelectedIndex = Array.IndexOf(Enum.GetValues(typeof(Kledingmaat)), truitje.Kledingmaat)+1;



            WijzigCompetitieTextBox.Text = truitje.Club.Competitie;
            WijzigClubTextBox.Text = truitje.Club.Ploegnaam;
            WijzigSeizoenTextBox.Text = truitje.Seizoen;
            WijzigPrijsTextBox.Text = truitje.Prijs.ToString();

            if (truitje.ClubSet.Thuis)
            {
                WijzigThuisCheckBox.IsChecked = true;
            }
            else
            {
                WijzigUitCheckBox.IsChecked = true;
            }

            WijzigVersieTextBox.Text = truitje.ClubSet.Versie.ToString();
        }

        private void WijzigBestellingTruitjeButton_Click(object sender, RoutedEventArgs e)
        {
            bool allesOk = true;
            ClubSet nieuweClubSet = new ClubSet();
            try
            {
                if (string.IsNullOrEmpty(WijzigCompetitieTextBox.Text))
                {
                    allesOk = false;
                    MessageBox.Show("Gelieve een correcte compitie in te vullen");
                }
                if (string.IsNullOrEmpty(WijzigClubTextBox.Text))
                {
                    allesOk = false;
                    MessageBox.Show("Gelieve een correcte club in te vullen");
                }
                if (string.IsNullOrEmpty(WijzigSeizoenTextBox.Text))
                {
                    allesOk = false;
                    MessageBox.Show("Gelieve een correcte seizoen in te vullen");
                }
                if (string.IsNullOrEmpty(WijzigPrijsTextBox.Text))
                {
                    allesOk = false;
                    MessageBox.Show("Gelieve een correcte prijs in te vullen");
                }
                if (WijzigThuisCheckBox.IsChecked == true && WijzigUitCheckBox.IsChecked == false)
                {
                    nieuweClubSet.ZetThuis(true);
                }
                else if (WijzigThuisCheckBox.IsChecked == false && WijzigUitCheckBox.IsChecked == true)
                {
                    nieuweClubSet.ZetThuis(false);
                }
                else if (WijzigThuisCheckBox.IsChecked == true && WijzigUitCheckBox.IsChecked == true)
                {
                    allesOk = false;
                    MessageBox.Show("Gelieve alleen maar THUIS of UIT te selecteren");
                }
                else if (WijzigThuisCheckBox.IsChecked == false && WijzigUitCheckBox.IsChecked == false)
                {
                    allesOk = false;
                    MessageBox.Show("Gelieve te kiezen tussen THUIS of UIT");
                }
                if (WijzigSelecteerMaatComboBox.SelectedIndex <= 0)
                {
                    allesOk = false;
                    MessageBox.Show("Gelieve een gelige maat te kiezen");
                }
                if (allesOk)
                {
                    nieuweClubSet.ZetVersie(int.Parse(WijzigVersieTextBox.Text));
                    var maat = (Kledingmaat)Enum.Parse(typeof(Kledingmaat), WijzigSelecteerMaatComboBox.SelectedItem.ToString());
                    Voetbaltruitje nieuweTrui = new Voetbaltruitje(Truitje.Id,new Club(WijzigCompetitieTextBox.Text, WijzigClubTextBox.Text), WijzigSeizoenTextBox.Text,
                        double.Parse(WijzigPrijsTextBox.Text), maat, nieuweClubSet);
                    Manager.UpdateVoetbaltruitje(nieuweTrui);
                    DialogResult = true;
                    Close();
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}

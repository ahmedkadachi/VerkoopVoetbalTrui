using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for BestellingSelecteerTruitje.xaml
    /// </summary>
    public partial class BestellingSelecteerTruitje : Window
    {
        private static string ConnectionString = @"Data Source=DESKTOP-R7T8D5F\SQLEXPRESS;Initial Catalog=VerkoopVoetbalTrui;Integrated Security=True";
        private ObservableCollection<string> competities;
        private ObservableCollection<string> clubs;
        public List<Voetbaltruitje> VoetbaltruitjeLijst { get; private set; } = new List<Voetbaltruitje>();

        private static IVoetbaltruitjeRepository voetbaltruitjeRepo = new VoetbaltruitjeDatabeheer(ConnectionString);
        private VoetbaltruitjeManager Manager = new VoetbaltruitjeManager(voetbaltruitjeRepo);

        public Voetbaltruitje voetbaltruitje { get; private set; }

        public BestellingSelecteerTruitje()
        {
            InitializeComponent();
            InitComboBoxen();
        }

        private void InitComboBoxen()
        {
            //maten comboboxen
            List<string> maten = Enum.GetNames(typeof(Kledingmaat)).ToList();
            maten.Insert(0, "<alles>");
            SelecteerMaatComboBox.ItemsSource = maten;
            SelecteerMaatComboBox.SelectedIndex = 0;

            VoegSelecteerMaatComboBox.ItemsSource = maten;
            VoegSelecteerMaatComboBox.SelectedIndex = 0;

            //competitie en club
            IClubRepository clubRepo = new ClubDatabeheer(ConnectionString);
            ClubManager ClubManager = new ClubManager(clubRepo);

            competities = new ObservableCollection<string>(ClubManager.GeefCompetities());
            competities.Insert(0, "<Geen competitie>");
            SelecteerCompetitieComboBox.ItemsSource = competities;
            SelecteerCompetitieComboBox.SelectedIndex = 0;


        }
        private Dictionary<string,List<string>> GeefCompetitiePloeg()
        {
            IClubRepository clubRepo = new ClubDatabeheer(ConnectionString);
            ClubManager ClubManager = new ClubManager(clubRepo);

            List<Club> clubLijstRAW = ClubManager.GeefClubs().ToList();
            Dictionary<string, List<string>> clubLijst = new Dictionary<string, List<string>>();

            foreach (Club element in clubLijstRAW)
            {
                if (clubLijst.ContainsKey(element.Competitie))
                {
                    clubLijst[element.Competitie].Add(element.Ploegnaam);
                }
                else
                {
                    var ploegList = new List<string>();
                    ploegList.Add(element.Ploegnaam);
                    clubLijst.Add(element.Competitie, ploegList);
                }
            }
            return clubLijst;
        }
        private void ZoekBestellingTruitjeButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                VoetbaltruitjeLijst.Clear();
                SelecteerTruitjes.Items.Clear();
                if (!string.IsNullOrEmpty(IDTextBox.Text))
                {
                    int truiId = int.Parse(IDTextBox.Text);
                    VoetbaltruitjeLijst = Manager.GeefVoetbaltruitjesID(truiId).ToList();
                }

                if(SelecteerMaatComboBox.SelectedIndex > 0 && SelecteerMaatComboBox.SelectedItem != null)
                {
                    VoetbaltruitjeLijst = Manager.GeefVoetbaltruitjesMaat(SelecteerMaatComboBox.SelectedItem.ToString()).ToList();
                }

                if (SelecteerCompetitieComboBox.SelectedIndex > 0 && SelecteerCompetitieComboBox.SelectedItem != null )
                {
                    if (SelecteerClubComboBox.SelectedIndex > 0 && SelecteerClubComboBox.SelectedItem != null)
                    {
                        VoetbaltruitjeLijst = Manager.GeefVoetbaltruitjesPloeg(SelecteerClubComboBox.SelectedItem.ToString()).ToList();
                    }
                    else
                    {
                        VoetbaltruitjeLijst = Manager.GeefVoetbaltruitjesCompetitie(SelecteerCompetitieComboBox.SelectedItem.ToString()).ToList();
                    }
                    
                }

                if (!string.IsNullOrEmpty(SeizoenTextBox.Text))
                {
                    VoetbaltruitjeLijst = Manager.GeefVoetbaltruitjesSeizoen(SeizoenTextBox.Text).ToList();
                }
                if (!string.IsNullOrEmpty(PrijsTextBox.Text))
                {
                    VoetbaltruitjeLijst = Manager.GeefVoetbaltruitjesPrijs(double.Parse(PrijsTextBox.Text)).ToList();
                }

                if (!string.IsNullOrEmpty(PrijsTextBox.Text))
                {
                    VoetbaltruitjeLijst = Manager.GeefVoetbaltruitjesPrijs(double.Parse(PrijsTextBox.Text)).ToList();
                }

                if (ThuisCheckBox.IsChecked == true)
                {
                    VoetbaltruitjeLijst = Manager.GeefVoetbaltruitjesThuis(true).ToList();
                }else if(UitCheckBox.IsChecked == true)
                {
                    VoetbaltruitjeLijst = Manager.GeefVoetbaltruitjesThuis(false).ToList();
                }
                if (!string.IsNullOrEmpty(VersieTextBox.Text))
                {
                    VoetbaltruitjeLijst = Manager.GeefVoetbaltruitjesVersie(VersieTextBox.Text).ToList(); ;
                }

                foreach (Voetbaltruitje element in VoetbaltruitjeLijst)
                {
                    SelecteerTruitjes.Items.Add(element);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void SelecteerTruitjeButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                voetbaltruitje = (Voetbaltruitje)SelecteerTruitjes.SelectedItem;
                DialogResult = true;
                Close();
            }
            catch
            {
                MessageBox.Show("Gelieve een trui te selecteren");
            }
        }

        private void SelecteerCompetitieComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //Dictionary<string,List<string>> clubLijst = GeefCompetitiePloeg();
            //SelecteerClubComboBox.ItemsSource = clubLijst[SelecteerCompetitieComboBox.SelectedItem.ToString()];
            IClubRepository clubRepo = new ClubDatabeheer(ConnectionString);
            ClubManager ClubManager = new ClubManager(clubRepo);

            if (SelecteerCompetitieComboBox.SelectedIndex != 0)
            {
                clubs = new ObservableCollection<string>(
                    ClubManager.GeefClubs(SelecteerCompetitieComboBox.SelectedItem.ToString()));
                clubs.Insert(0, "<Geen club>");
                SelecteerClubComboBox.ItemsSource = clubs;
                SelecteerClubComboBox.SelectedIndex = 0;
            }
            else
            {
                SelecteerClubComboBox.ItemsSource = null;
            }
        }
        private void VoegBestellingTruitjeButton_Click(object sender, RoutedEventArgs e)
        {
            bool allesOK = true;
            
            ClubSet nieuweClubSet = new ClubSet();

            if (string.IsNullOrEmpty(VoegCompetitieTextBox.Text))
            {
                allesOK = false;
                MessageBox.Show("Gelieve een geldige Competitie in te voeren");
            }
            if (string.IsNullOrEmpty(VoegClubTextBox.Text))
            {
                allesOK = false;
                MessageBox.Show("Gelieve een geldige Club in te voeren");
            }
            if (string.IsNullOrEmpty(VoegSeizoenTextBox.Text))
            {
                allesOK = false;
                MessageBox.Show("Gelieve een geldige Seizoen in te voeren");
            }
            if (string.IsNullOrEmpty(VoegPrijsTextBox.Text))
            {
                allesOK = false;
                MessageBox.Show("Gelieve een geldige Prijs in te voeren");
            }
            if (string.IsNullOrEmpty(VoegVersieTextBox.Text))
            {
                allesOK = false;
                MessageBox.Show("Gelieve een geldige versie in te voeren");
            }
            if(VoegThuisCheckBox.IsChecked == true && VoegUitCheckBox.IsChecked == false)
            {
                nieuweClubSet.ZetThuis(true);
            }
            else if(VoegThuisCheckBox.IsChecked == false && VoegUitCheckBox.IsChecked == true)
            {
                nieuweClubSet.ZetThuis(false);
            }
            else if(VoegThuisCheckBox.IsChecked == true && VoegUitCheckBox.IsChecked == true)
            {
                allesOK = false;
                MessageBox.Show("Gelieve alleen maar THUIS of UIT te selecteren");
            }else if(VoegThuisCheckBox.IsChecked == false && VoegUitCheckBox.IsChecked == false)
            {
                allesOK = false;
                MessageBox.Show("Gelieve te kiezen tussen THUIS of UIT");
            }
            if (VoegSelecteerMaatComboBox.SelectedIndex <= 0)
            {
                allesOK = false;
                MessageBox.Show("Gelieve een gelige maat te kiezen");
            }

            try
            {
                if (allesOK)
                {
                    nieuweClubSet.ZetVersie(int.Parse(VoegVersieTextBox.Text));
                    var maat = (Kledingmaat)Enum.Parse(typeof(Kledingmaat), VoegSelecteerMaatComboBox.SelectedItem.ToString());
                    Voetbaltruitje nieuweTrui = new Voetbaltruitje(new Club(VoegCompetitieTextBox.Text,VoegClubTextBox.Text),VoegSeizoenTextBox.Text,
                        double.Parse(VoegPrijsTextBox.Text),maat,nieuweClubSet);
                    Manager.VoegVoetbaltruitjeToe(nieuweTrui);
                }

            }catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void VerwijderBestellingTruitjeButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (SelecteerTruitjes.SelectedIndex >= 0)
                {
                    Voetbaltruitje Voetbaltruitje = (Voetbaltruitje)SelecteerTruitjes.SelectedItem;
                    Manager.VerwijderVoetbaltruitje(Voetbaltruitje);
                    SelecteerTruitjes.Items.Remove(Voetbaltruitje);
                    SelecteerTruitjes.Items.Refresh();
                }
                else
                {
                    MessageBox.Show("Gelieve eerst een trui te selecter");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private void MenuItemWijzig_Click(object sender, RoutedEventArgs e)
        {
            if (SelecteerTruitjes.SelectedIndex < 0)
            {
                MessageBox.Show("Gelieve een Trui te selecteren");
            }
            else
            {
                WijzigTruiWindow  w = new WijzigTruiWindow((Voetbaltruitje)SelecteerTruitjes.SelectedItem);
                if (w.ShowDialog() == true)
                {
                    SelecteerTruitjes.Items.Refresh();
                }
            }
        }
    }
}

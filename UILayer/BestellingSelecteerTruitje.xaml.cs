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
        public List<Voetbaltruitje> Voetbaltruitje { get; private set; } = new List<Voetbaltruitje>();

        private static IVoetbaltruitjeRepository voetbaltruitjeRepo = new VoetbaltruitjeDatabeheer(ConnectionString);
        private VoetbaltruitjeManager Manager = new VoetbaltruitjeManager(voetbaltruitjeRepo);

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
                if (!string.IsNullOrEmpty(IDTextBox.Text))
                {
                    int truiId = int.Parse(IDTextBox.Text);
                    Voetbaltruitje = Manager.GeefVoetbaltruitjesID(truiId).ToList();

                    SelecteerTruitjes.ItemsSource = Voetbaltruitje;
                }

                if(SelecteerMaatComboBox.SelectedIndex > 0 && SelecteerMaatComboBox.SelectedItem != null)
                {
                    SelecteerTruitjes.ItemsSource = Manager.GeefVoetbaltruitjesMaat(SelecteerMaatComboBox.SelectedItem.ToString());
                }

                

                if (SelecteerCompetitieComboBox.SelectedIndex > 0 && SelecteerCompetitieComboBox.SelectedItem != null )
                {
                    if (SelecteerClubComboBox.SelectedIndex > 0 && SelecteerClubComboBox.SelectedItem != null)
                    {
                        SelecteerTruitjes.ItemsSource = Manager.GeefVoetbaltruitjesPloeg(SelecteerClubComboBox.SelectedItem.ToString());
                    }
                    else
                    {
                        SelecteerTruitjes.ItemsSource = Manager.GeefVoetbaltruitjesCompetitie(SelecteerCompetitieComboBox.SelectedItem.ToString());
                    }
                    
                }

                if (!string.IsNullOrEmpty(SeizoenTextBox.Text))
                {
                    SelecteerTruitjes.ItemsSource = Manager.GeefVoetbaltruitjesSeizoen(SeizoenTextBox.Text);
                }
                if (!string.IsNullOrEmpty(PrijsTextBox.Text))
                {
                    SelecteerTruitjes.ItemsSource = Manager.GeefVoetbaltruitjesPrijs(double.Parse(PrijsTextBox.Text));
                }



            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void SelecteerTruitjeButton_Click(object sender, RoutedEventArgs e)
        {

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
    }
}

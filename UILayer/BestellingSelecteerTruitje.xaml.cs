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
    /// Interaction logic for BestellingSelecteerTruitje.xaml
    /// </summary>
    public partial class BestellingSelecteerTruitje : Window
    {
        private string ConnectionString = @"Data Source=DESKTOP-R7T8D5F\SQLEXPRESS;Initial Catalog=VerkoopVoetbalTrui;Integrated Security=True";
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
            
            var clubLijst = GeefCompetitiePloeg();

            SelecteerCompetitieComboBox.ItemsSource = clubLijst.Keys;
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

        }

        private void SelecteerTruitjeButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void SelecteerCompetitieComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Dictionary<string,List<string>> clubLijst = GeefCompetitiePloeg();
            SelecteerClubComboBox.ItemsSource = clubLijst[SelecteerCompetitieComboBox.SelectedItem.ToString()];
        }
    }
}

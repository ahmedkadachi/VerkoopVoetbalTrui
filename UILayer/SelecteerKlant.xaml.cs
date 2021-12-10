﻿using System;
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
    /// Interaction logic for SelecteerKlant.xaml
    /// </summary>
    public partial class SelecteerKlant : Window
    {
        private static string ConnectionString = @"Data Source=DESKTOP-R7T8D5F\SQLEXPRESS;Initial Catalog=VerkoopVoetbalTrui;Integrated Security=True";
        private static IKlantRepository KlantRepo = new KlantDatabeheer(ConnectionString);
        private KlantManager Manager = new KlantManager(KlantRepo);

        private static IBestellingRepository bestelRepo = new BestellingDatabeheer(ConnectionString);
        private BestellingManager bestelManager = new BestellingManager(bestelRepo);
        public Klant Klant { get; private set; }
        
        public SelecteerKlant()
        {
            InitializeComponent();
        }

        private void ZoekKlantButton_Click(object sender, RoutedEventArgs e)
        {
            ObservableCollection<Klant> KlantLijst = new ObservableCollection<Klant>(Manager.GeefKlanten());
            KlantLijst.Clear();
            KlantLijst = new ObservableCollection<Klant>(Manager.GeefKlanten());
            bool NiestIsGeselecteerd = true;
            try
            {
                if (!string.IsNullOrEmpty(KlantIdTextBox.Text))
                {
                    int klantId = int.Parse(KlantIdTextBox.Text);
                    Klant = Manager.GeefKlant(klantId);
                    VulListboxIn(Klant);
                    NiestIsGeselecteerd = false;
                }

                if (!string.IsNullOrEmpty(KlantNaamTextBox.Text))
                {
                    Klant = Manager.GeefKlantNaam(KlantNaamTextBox.Text);
                    VulListboxIn(Klant);
                    NiestIsGeselecteerd = false;
                }

                if (!string.IsNullOrEmpty(KlantAdresTextBox.Text))
                {
                    Klant = Manager.GeefKlantAdres(KlantAdresTextBox.Text);
                    VulListboxIn(Klant);
                    NiestIsGeselecteerd = false;
                }
                if (NiestIsGeselecteerd)
                {
                    
                    KlantenListBox.ItemsSource = KlantLijst;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            
        }
        private void VerwijderKlantButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Klant = (Klant)KlantenListBox.SelectedItem;
                Manager.VerwijderKlant(Klant);

            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private void SelecteerKlantButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Klant = (Klant)KlantenListBox.SelectedItem;
                DialogResult = true;
                Close();
            }
            catch
            {
                MessageBox.Show("Gelieve een klant te selecteren");
            }
            
           
        }
        private void VulListboxIn(Klant klant)
        {
            List<Bestelling> bestellingen;
            bestellingen = bestelManager.GeefBestellingenVanKlant(klant).ToList();
            string gegevens = "";

            //KlantenListBox.Items.Add(Klant);
            foreach (Bestelling element in bestellingen)
            {
                gegevens += element.ToString();
                foreach (KeyValuePair<Voetbaltruitje, int> el in element.TruiBestelling)
                {
                    gegevens += "\n" + el.Value + " - " + el.Key;
                }
                gegevens += "\n";
            }
            klant.Bestellingen = bestellingen;
            KlantenListBox.Items.Add(klant);
            KlantenListBox.Items.Add(gegevens);
        }

        private void VoegKlantButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(VoegKlantAdresTextBox.Text))
                {
                    if (!string.IsNullOrEmpty(VoegKlantNaamTextBox.Text))
                    {
                        Klant = new Klant(VoegKlantNaamTextBox.Text, VoegKlantAdresTextBox.Text);
                        Manager.VoegKlantToe(Klant);
                        MessageBox.Show("De klant " + VoegKlantNaamTextBox.Text + " werd succesvol aangemaakt!");
                    }
                    else
                    {
                        MessageBox.Show("Gelieve een naam in te vullen!");
                    }
                }
                else
                {
                    MessageBox.Show("Gelieve een adres in te vullen!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            
        }
    }
}

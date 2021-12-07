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
    /// Interaction logic for WindowBestelling.xaml
    /// </summary>
    public partial class WindowBestelling : Window
    {
        private static string ConnectionString = @"Data Source=DESKTOP-R7T8D5F\SQLEXPRESS;Initial Catalog=VerkoopVoetbalTrui;Integrated Security=True";
        private static IBestellingRepository BestelRepo = new BestellingDatabeheer(ConnectionString);
        private BestellingManager bestellingManager = new BestellingManager(BestelRepo);
        public Bestelling bestelling { get; private set; }
        public WindowBestelling()
        {
            InitializeComponent();

            
        }
    }
}

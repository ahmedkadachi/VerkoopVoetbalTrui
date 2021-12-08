using System;
using BusinessLayer.Interfaces;
using BusinessLayer.Managers;
using BusinessLayer.Model;
using DataLayer;

namespace ConsoleTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            
            IKlantRepository repoKlant = new KlantDatabeheer(@"Data Source=DESKTOP-R7T8D5F\SQLEXPRESS;Initial Catalog=VerkoopVoetbalTrui;Integrated Security=True");
            IVoetbaltruitjeRepository repoTrui = new VoetbaltruitjeDatabeheer(@"Data Source=DESKTOP-R7T8D5F\SQLEXPRESS;Initial Catalog=VerkoopVoetbalTrui;Integrated Security=True");
            IBestellingRepository repoBestelling = new BestellingDatabeheer(@"Data Source=DESKTOP-R7T8D5F\SQLEXPRESS;Initial Catalog=VerkoopVoetbalTrui;Integrated Security=True");

            KlantManager km = new KlantManager(repoKlant);
            VoetbaltruitjeManager vtm = new VoetbaltruitjeManager(repoTrui);
            BestellingManager bm = new BestellingManager(repoBestelling);

            Klant k = new Klant(2,"Ahmed", "DeLeukeStraat");
            Voetbaltruitje v = new Voetbaltruitje(2,new Club("Champions league", "Liverpool"), "2021", 25.00, Kledingmaat.S, new ClubSet(true, 1));

            //km.VoegKlantToe(k);
            //km.UpdateKlant(new Klant(1, "walid", "debestestraat"));
            //Console.WriteLine(km.GeefKlant(1));

            Bestelling bestelling = new Bestelling();
            bestelling.VoegProductToe(v, 3);
            bestelling.ZetKlant(k);
            bestelling.ZetBetaald(true);
            bestelling.ZetTijdstip(DateTime.Now);
            bestelling.ZetPrijs(25);
            bm.VoegBestellingToe(bestelling);

        }
    }
}

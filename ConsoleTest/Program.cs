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
            KlantManager km = new KlantManager(repoKlant);

            Klant k = new Klant("Ahmed", "DeLeukeStraat");

            //km.VoegKlantToe(k);
            //km.UpdateKlant(new Klant(1, "walid", "debestestraat"));
            //Console.WriteLine(km.GeefKlant(1));
            Kledingmaat dom = Kledingmaat.L;
            Console.WriteLine(dom.ToString());
        }
    }
}

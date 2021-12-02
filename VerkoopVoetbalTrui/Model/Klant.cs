using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLayer.Exceptions;
using VerkoopVoetbalTrui;

namespace BusinessLayer.Model
{
    public class Klant : IKlant
    {
        public string Adres { get; private set; }

        public int KlantId { get; private set; }

        public string Naam { get; private set; }
        public List<Bestelling> Bestellingen = new List<Bestelling>();
        public Klant(int klantId, string naam, string adres, List<Bestelling> bestellingen) : this(klantId, naam, adres)
        {
            ZetKlantId(klantId);
            ZetNaam(naam);
            ZetAdres(adres);
            Bestellingen = bestellingen;
        }
        public Klant(int klantId, string naam, string adres) : this(naam, adres)
        {
            ZetKlantId(klantId);
            ZetNaam(naam);
            ZetAdres(adres);
        }
        public Klant(string naam, string adres)
        {
            ZetNaam(naam);
            ZetAdres(adres);
        }
        public IReadOnlyList<Bestelling> GetBestellingen()
        {
            return Bestellingen;
        }

        public bool HeeftBestelling(Bestelling bestelling)
        {
            if (Bestellingen.Contains(bestelling)) return true;
            else return false;
        }

        public int Korting()
        {
            int korting = 0;
            if (Bestellingen.Count() > 10)
            {
                korting = 20;
            }
            else if (Bestellingen.Count() > 5)
            {
                korting = 10;
            }
            return korting;
        }

        public void VerwijderBestelling(Bestelling bestelling)
        {
            bool bestaat = false;
            foreach (Bestelling element in Bestellingen)
            {
                if (element.BestellingId == bestelling.BestellingId) bestaat = true;
            }
            if (bestaat) Bestellingen.Remove(bestelling);
            else throw new Exception("Deze bestelling zit niet bij deze klant");
        }

        public void VoegToeBestelling(Bestelling bestelling)
        {
            if (bestelling == null) throw new KlantException("Klant : verijderBestelling");
            if (Bestellingen.Contains(bestelling))
            {
                throw new KlantException("Klant : Addbestelling - bestelling bestaat al");
            }
            else
            {
                Bestellingen.Add(bestelling);
                if (bestelling.Klant != this)
                    bestelling.ZetKlant(this);
            }
        }

        public void ZetAdres(string adres)
        {
            if (adres == null || adres == "") throw new KlantException("De adres mag niet leeg zijn");
            if (adres.Length < 5) throw new KlantException("De adres moet langer dan 5 letters zijn");
            Adres = adres;
        }

        public void ZetKlantId(int id)
        {
            if (id <= 0) throw new KlantException("De ID mag niet kleiner dan 0 zijn");
            KlantId = id;
        }

        public void ZetNaam(string naam)
        {
            if (naam == null || naam == "") throw new KlantException("De naam mag niet leeg zijn");
            Naam = naam;
        }
        public override string ToString()
        {
            return "Klant: " + KlantId + " " + Naam + " " + Adres;
        }
    }
}

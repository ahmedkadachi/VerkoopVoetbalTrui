using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLayer.Exceptions;
using VerkoopVoetbalTrui;

namespace BusinessLayer.Model
{
    public class Bestelling : IBestelling
    {
        public int BestellingId { get; private set; }

        public bool Betaald { get; private set; }

        public Klant Klant { get; private set; }

        public double Prijs { get; private set; }

        public DateTime? Tijdstip { get; private set; }
        public Dictionary<Voetbaltruitje, int> TruiBestelling = new Dictionary<Voetbaltruitje, int>();
        public Bestelling()
        {

        }
        public Bestelling(int bestellingId, DateTime? tijdstip) : this(tijdstip)
        {
            ZetBestellingId(bestellingId);
            ZetTijdstip(tijdstip);
        }
        public Bestelling(int bestellingId, Klant klant, DateTime? tijdstip)
        {
            ZetBestellingId(bestellingId);
            ZetKlant(klant);
            ZetTijdstip(tijdstip);
        }
        public Bestelling(int bestellingId, Klant klant, DateTime? tijdstip, Dictionary<Voetbaltruitje, int> producten)
        {
            ZetBestellingId(bestellingId);
            ZetKlant(klant);
            ZetTijdstip(tijdstip);
            ZetBestelling(producten);
        }
        public Bestelling(int bestellingId, Klant klant, DateTime? tijdstip, double prijs, bool betaald, Dictionary<Voetbaltruitje, int> producten)
        {
            ZetBestellingId(bestellingId);
            ZetKlant(klant);
            ZetTijdstip(tijdstip);
            ZetPrijs(prijs);
            Betaald = betaald;
            ZetBestelling(producten);

        }
        public Bestelling(DateTime? tijdstip)
        {
            ZetTijdstip(tijdstip);
        }
        public IReadOnlyDictionary<Voetbaltruitje, int> GeefProducten()
        {
            if (TruiBestelling == null) throw new BestellingException("Deze bestelling is leeg");
            return TruiBestelling;
        }

        public double Kostprijs()
        {
            double prijs = 0.0;
            int korting;
            if (Klant is null)
            {
                korting = 0;
            }
            else
            {
                korting = Klant.Korting();
            }

            foreach (KeyValuePair<Voetbaltruitje, int> element in TruiBestelling)
            {
                prijs += element.Key.Prijs * element.Value;
            }
            return prijs -= prijs / 100 * korting;
        }

        public void VerwijderKlant()
        {
            if (this.Klant == null) throw new BestellingException("Deze klant bestaat niet");
            this.Klant = null;
        }

        public void VerwijderProduct(Voetbaltruitje voetbaltruitje, int aantal)
        {
            if (aantal <= 0) throw new BestellingException("Het aantal mag niet negatief of kleiner zijn dan 0");
            if (TruiBestelling.ContainsKey(voetbaltruitje))
            {
                if (TruiBestelling[voetbaltruitje] <= aantal)
                {
                    TruiBestelling.Remove(voetbaltruitje);
                }
                else
                {
                    TruiBestelling[voetbaltruitje] -= aantal;
                }
            }
            else throw new BestellingException("Deze trui bestaat niet in deze bestelling");
        }

        public void VoegProductToe(Voetbaltruitje voetbaltruitje, int aantal)
        {
            if (aantal <= 0) throw new BestellingException("Het aantal mag niet negatief of kleiner zijn dan 0");

            if (TruiBestelling.ContainsKey(voetbaltruitje))
            {
                TruiBestelling[voetbaltruitje] += aantal;
            }
            else
            {
                TruiBestelling.Add(voetbaltruitje, aantal);
            }
        }

        public void ZetBestellingId(int id)
        {
            if (id <= 0) throw new BestellingException("De id mag niet kleiner of gelijk aan 0 zijn");
            BestellingId = id;
        }

        public void ZetBetaald(bool betaald)
        {
            if (betaald == null) throw new BestellingException("De betaald status mag niet leeg zijn");
            Betaald = betaald;
        }
        public void ZetPrijs(double prijs)
        {
            if (prijs < 0) throw new BestellingException("De prijs kan niet kleiner dan 0 zijn");
            Prijs = prijs;
        }
        public void ZetKlant(Klant newKlant)
        {
            if (newKlant == null) throw new BestellingException("Bestelling - invalid klant");
            if (newKlant == Klant) throw new BestellingException("Bestelling - zetKlant - not new");

            if (Klant != null)
                if (Klant.HeeftBestelling(this))
                    Klant.VerwijderBestelling(this);
            if (!newKlant.HeeftBestelling(this))
                newKlant.VoegToeBestelling(this);
            Klant = newKlant;
        }

        public void ZetTijdstip(DateTime? tijdstip)
        {
            if (tijdstip == null) throw new BestellingException("De datum mag niet null zijn");
            Tijdstip = tijdstip;
        }
        public void ZetBestelling(Dictionary<Voetbaltruitje,int> product)
        {
            if (product == null) throw new BestellingException("De bestellingslijst mag niet leeg zijn");
            TruiBestelling = product;
        }
        public override string ToString()
        {
            return "Bestelling: " + BestellingId + ", " + Betaald + ", " + Tijdstip + ", klant:  " +Klant.KlantId + ", "+ Klant.Naam+ ", "+ Klant.Adres ;
        }
    }
}

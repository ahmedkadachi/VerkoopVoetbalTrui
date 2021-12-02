using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLayer.Exceptions;
using BusinessLayer.Model;
using Xunit;

namespace TestProjectBusinessLayer
{
    public class UnitTestBestelling
    {
        [Fact]
        public void Test_ctor_Valid()
        {
            Bestelling bestelling = new Bestelling(1, new Klant(1,"ahmed","Ronse"), DateTime.Today,22.5,true, new Dictionary<Voetbaltruitje, int> ());

            Assert.Equal(1, bestelling.BestellingId);
            Assert.Equal(1, bestelling.Klant.KlantId);
            Assert.Equal("ahmed", bestelling.Klant.Naam);
            Assert.Equal("Ronse", bestelling.Klant.Adres);
            Assert.Equal(DateTime.Today, bestelling.Tijdstip);
            Assert.Equal(22.5, bestelling.Prijs);
            Assert.True(bestelling.Betaald);
            Assert.Equal(new Dictionary<Voetbaltruitje, int>(), bestelling.TruiBestelling);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-11)]
        public void Test_ctor_noId_InValid(int id)
        {
            Assert.Throws<BestellingException>(() => new Bestelling(id, new Klant(1, "ahmed", "Ronse"), DateTime.Today, new Dictionary<Voetbaltruitje, int>()));
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-11)]
        public void Test_ctor_noKlantId_InValid(int klantid)
        {
            Assert.Throws<KlantException>(() => new Bestelling(1, new Klant(klantid, "ahmed", "Ronse"), DateTime.Today, new Dictionary<Voetbaltruitje, int>()));
        }

        [Fact]
        public void Test_ctor_noKlantNaam_InValid()
        {
            Assert.Throws<KlantException>(() => new Bestelling(1, new Klant(1, null, "Ronse"), DateTime.Today, new Dictionary<Voetbaltruitje, int>()));
        }

        [Fact]
        public void Test_ctor_noKlantAdres_InValid()
        {
            Assert.Throws<KlantException>(() => new Bestelling(1, new Klant(1, "ahmed", null), DateTime.Today, new Dictionary<Voetbaltruitje, int>()));
        }
        [Theory]
        [InlineData(-0.25)]
        [InlineData(-11)]
        [InlineData(-11.25)]
        public void Test_ctor_noPrijs_InValid(double prijs)
        {
            Assert.Throws<BestellingException>(() => new Bestelling(1, new Klant(1, "ahmed", "Ronse"), DateTime.Today,prijs,true, new Dictionary<Voetbaltruitje, int>()));
        }
        [Fact]
        public void Test_ctor_noBestelling_InValid()
        {
            Assert.Throws<BestellingException>(() => new Bestelling(1, new Klant(1, "ahmed", "Ronse"), DateTime.Today, null));
        }

        [Fact]
        public void Test_ZetId_Valid()
        {
            Bestelling bestelling = new Bestelling(1, new Klant(1, "ahmed", "Ronse"), DateTime.Today, 22.5, true, new Dictionary<Voetbaltruitje, int>());
            bestelling.ZetBestellingId(1);
            Assert.Equal(1, bestelling.BestellingId);
        }

        [Fact]
        public void Test_ZetKlant_Valid()
        {
            Bestelling bestelling = new Bestelling(1, new Klant(1, "ahmed", "Ronse"), DateTime.Today, 22.5, true, new Dictionary<Voetbaltruitje, int>());
            bestelling.ZetKlant(new Klant("Ahmed","Ronse"));
            Assert.Equal("Ahmed", bestelling.Klant.Naam);
            Assert.Equal("Ronse", bestelling.Klant.Adres);
        }

        [Fact]
        public void Test_ZetPrijs_Valid()
        {
            Bestelling bestelling = new Bestelling(1, new Klant(1, "ahmed", "Ronse"), DateTime.Today, 22.5, true, new Dictionary<Voetbaltruitje, int>());
            bestelling.ZetPrijs(22.5);
            Assert.Equal(22.5, bestelling.Prijs);
        }

        [Fact]
        public void Test_ZetBestelling_Valid()
        {
            Bestelling bestelling = new Bestelling(1, new Klant(1, "ahmed", "Ronse"), DateTime.Today, 22.5, true, new Dictionary<Voetbaltruitje, int>());
            bestelling.ZetBestelling(new Dictionary<Voetbaltruitje, int>());
            Assert.Equal(new Dictionary<Voetbaltruitje, int>(), bestelling.TruiBestelling);
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(0)]
        public void Test_ZetId_InValid(int id)
        {
            Bestelling bestelling = new Bestelling(1, new Klant(1, "ahmed", "Ronse"), DateTime.Today, 22.5, true, new Dictionary<Voetbaltruitje, int>());
            var ex = Assert.Throws<BestellingException>(() => bestelling.ZetBestellingId(id));
            Assert.Equal("De id mag niet kleiner of gelijk aan 0 zijn", ex.Message);
        }

        [Fact]
        public void Test_ZetKlant_InValid()
        {
            Bestelling bestelling = new Bestelling(1, new Klant(1, "ahmed", "Ronse"), DateTime.Today, 22.5, true, new Dictionary<Voetbaltruitje, int>());
            var ex = Assert.Throws<BestellingException>(() => bestelling.ZetKlant(null));
            Assert.Equal("Bestelling - invalid klant", ex.Message);
        }

        [Theory]
        [InlineData(-0.25)]
        [InlineData(-11)]
        [InlineData(-11.25)]
        public void Test_ZetPrijs_InValid(double prijs)
        {
            Bestelling bestelling = new Bestelling(1, new Klant(1, "ahmed", "Ronse"), DateTime.Today, 22.5, true, new Dictionary<Voetbaltruitje, int>());
            var ex = Assert.Throws<BestellingException>(() => bestelling.ZetPrijs(prijs));
            Assert.Equal("De prijs kan niet kleiner dan 0 zijn", ex.Message);
        }

        [Fact]
        public void Test_ZetBestelling_InValid()
        {
            Bestelling bestelling = new Bestelling(1, new Klant(1, "ahmed", "Ronse"), DateTime.Today, 22.5, true, new Dictionary<Voetbaltruitje, int>());
            var ex = Assert.Throws<BestellingException>(() => bestelling.ZetBestelling(null));
            Assert.Equal("De bestellingslijst mag niet leeg zijn", ex.Message);
        }
    }
}

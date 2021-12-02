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
    public class UnitTestKlant
    {
        [Fact]
        public void Test_ctor_noId_Valid()
        {
            Klant klant = new Klant(1,"Ahmed","Ronse",new List<Bestelling>());

            Assert.Equal(1, klant.KlantId);
            Assert.Equal("Ahmed", klant.Naam);
            Assert.Equal("Ronse", klant.Adres);
            Assert.Equal(new List<Bestelling>(), klant.Bestellingen);
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(0)]
        public void Test_Ctor_Id_Invalid(int klantid)
        {
            Assert.Throws<KlantException>(() => new Klant(klantid, "Ahmed","Ronse",new List<Bestelling>()));
        }

        [Fact]
        public void Test_Ctor_Naam_Invalid()
        {
            Assert.Throws<KlantException>(() => new Klant(1, null, "Ronse", new List<Bestelling>()));
        }

        [Theory]
        [InlineData(null)]
        [InlineData("aa")]
        public void Test_Ctor_Adres_Invalid(string adres)
        {
            Assert.Throws<KlantException>(() => new Klant(1, "Ahmed", adres, new List<Bestelling>()));
        }

        [Fact]
        public void Test_ZetId_Valid()
        {
            Klant klant = new Klant(1, "Ahmed", "Ronse", new List<Bestelling>());
            klant.ZetKlantId(1);
            Assert.Equal(1, klant.KlantId);
        }

        [Fact]
        public void Test_ZetNaam_Valid()
        {
            Klant klant = new Klant(1, "Ahmed", "Ronse", new List<Bestelling>());
            klant.ZetNaam("Ahmed");
            Assert.Equal("Ahmed", klant.Naam);
        }
        [Fact]
        public void Test_Adres_Valid()
        {
            Klant klant = new Klant(1, "Ahmed", "Ronse", new List<Bestelling>());
            klant.ZetAdres("Ronse");
            Assert.Equal("Ronse", klant.Adres);
        }
        

        [Theory]
        [InlineData(-1)]
        [InlineData(0)]
        public void Test_ZetId_InValid(int id)
        {
            Klant klant = new Klant(1, "Ahmed", "Ronse", new List<Bestelling>());
            var ex = Assert.Throws<KlantException>(() => klant.ZetKlantId(id));
            Assert.Equal("De ID mag niet kleiner dan 0 zijn", ex.Message);
        }

        [Fact]
        public void Test_ZetNaam_InValid()
        {
            Klant klant = new Klant(1, "Ahmed", "Ronse", new List<Bestelling>());
            var ex = Assert.Throws<KlantException>(() => klant.ZetNaam(null));
            Assert.Equal("De naam mag niet leeg zijn", ex.Message);
        }
        
        [Fact]
        public void Test_ZetAdres_KleinerDan5_InValid()
        {
            Klant klant = new Klant(1, "Ahmed", "Ronse", new List<Bestelling>());
            var ex = Assert.Throws<KlantException>(() => klant.ZetAdres("aa"));
            Assert.Equal("De adres moet langer dan 5 letters zijn", ex.Message);
        }

        [Fact]
        public void Test_ZetAdres_Leeg_InValid()
        {
            Klant klant = new Klant(1, "Ahmed", "Ronse", new List<Bestelling>());
            var ex = Assert.Throws<KlantException>(() => klant.ZetAdres(""));
            Assert.Equal("De adres mag niet leeg zijn", ex.Message);
        }
    }
}

using BusinessLayer.Model;
using BusinessLayer.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace TestProjectBusinessLayer
{
    public class UnitTestTrui
    {
        [Fact]
        public void Test_ctor_noId_Valid()
        {
            Voetbaltruitje truitje = new Voetbaltruitje(new Club("premier league", "city"), "2021-2022", 87, Kledingmaat.M, new ClubSet(true, 1));

            Assert.Equal("premier league", truitje.Club.Competitie);
            Assert.Equal("city", truitje.Club.Ploegnaam);
            Assert.Equal("2021-2022", truitje.Seizoen);
            Assert.Equal(87, truitje.Prijs);
            Assert.Equal(Kledingmaat.M, truitje.Kledingmaat);
            Assert.True(truitje.ClubSet.Thuis);
            Assert.Equal(1, truitje.ClubSet.Versie);
        }
        [Theory]
        [InlineData(-10.5)]
        [InlineData(-0.5)]
        [InlineData(0)]
        public void Test_ctor_noId_InValid(double prijs)
        {
            Assert.Throws<TruiException>(() => new Voetbaltruitje(new Club("premier league", "city"), "2021-2022", prijs, Kledingmaat.M, new ClubSet(true, 1)));
        }
        [Fact]
        public void Test_ctor_noId_noClub_InValid()
        {
            Assert.Throws<TruiException>(() => new Voetbaltruitje(null, "2021-2022", 87, Kledingmaat.M, new ClubSet(true, 1)));
        }
        [Fact]
        public void Test_ctor_noId_noClubSet_InValid()
        {
            Assert.Throws<TruiException>(() => new Voetbaltruitje(new Club("premier league", "city"), "2021-2022", 87, Kledingmaat.M, null));
        }
        [Fact]
        public void Test_ctor_Id_Valid()
        {
            Voetbaltruitje truitje = new Voetbaltruitje(10, new Club("premier league", "city"), "2021-2022", 87, Kledingmaat.M, new ClubSet(true, 1));
            Assert.Equal(10, truitje.Id);
            Assert.Equal("premier league", truitje.Club.Competitie);
            Assert.Equal("city", truitje.Club.Ploegnaam);
            Assert.Equal("2021-2022", truitje.Seizoen);
            Assert.Equal(87, truitje.Prijs);
            Assert.Equal(Kledingmaat.M, truitje.Kledingmaat);
            Assert.True(truitje.ClubSet.Thuis);
            Assert.Equal(1, truitje.ClubSet.Versie);
        }
        [Theory]
        [InlineData(10, -10.5)]
        [InlineData(10, -0.5)]
        [InlineData(10, 0)]
        [InlineData(-10, 100)]
        [InlineData(0, 100)]
        [InlineData(-1, 100)]
        [InlineData(-10, 0)]
        public void Test_ctor_Id_InValid(int id, double prijs)
        {
            Assert.Throws<TruiException>(() => new Voetbaltruitje(id, new Club("premier league", "city"), "2021-2022", prijs, Kledingmaat.M, new ClubSet(true, 1)));
        }
        [Fact]
        public void Test_ctor_Id_noClub_InValid()
        {
            Assert.Throws<TruiException>(() => new Voetbaltruitje(10, null, "2021-2022", 87, Kledingmaat.M, new ClubSet(true, 1)));
        }
        [Fact]
        public void Test_ctor_Id_noClubSet_InValid()
        {
            Assert.Throws<TruiException>(() => new Voetbaltruitje(10, new Club("premier league", "city"), "2021-2022", 87, Kledingmaat.M, null));
        }
        [Fact]
        public void Test_ZetId_Valid()
        {
            Voetbaltruitje truitje = new Voetbaltruitje(new Club("premier league", "city"), "2021-2022", 87, Kledingmaat.M, new ClubSet(true, 1));
            truitje.ZetId(1);
            Assert.Equal(1, truitje.Id);
        }
        [Theory]
        [InlineData(-1)]
        [InlineData(0)]
        public void Test_ZetId_InValid(int id)
        {
            Voetbaltruitje truitje = new Voetbaltruitje(new Club("premier league", "city"), "2021-2022", 87, Kledingmaat.M, new ClubSet(true, 1));
            var ex = Assert.Throws<TruiException>(() => truitje.ZetId(id));
            Assert.Equal("De ID mag niet kleiner dan 0 zijn", ex.Message);
        }
        [Fact]
        public void Test_ZetPrijs_Valid()
        {
            Voetbaltruitje truitje = new Voetbaltruitje(new Club("premier league", "city"), "2021-2022", 87, Kledingmaat.M, new ClubSet(true, 1));
            truitje.ZetPrijs(10.5);
            Assert.Equal(10.5, truitje.Prijs);
        }
        [Theory]
        [InlineData(-10.5)]
        [InlineData(-0.5)]
        [InlineData(0)]
        public void Test_ZetPrijs_InValid(double prijs)
        {
            Voetbaltruitje truitje = new Voetbaltruitje(new Club("premier league", "city"), "2021-2022", 87, Kledingmaat.M, new ClubSet(true, 1));
            var ex = Assert.Throws<TruiException>(() => truitje.ZetPrijs(prijs));
            Assert.Equal("De prijs mag niet 0 of kleiner zijn", ex.Message);
        }
        [Fact]
        public void Test_ZetClub_ValidReference()
        {
            Voetbaltruitje truitje = new Voetbaltruitje(new Club("premier league", "city"), "2021-2022", 87, Kledingmaat.M, new ClubSet(true, 1));
            Club club = new Club("premier league", "Leicester");
            truitje.ZetClub(club);
            Assert.Equal(club, truitje.Club);
        }
        [Fact]
        public void Test_ZetClub_ValidValue()
        {
            Voetbaltruitje truitje = new Voetbaltruitje(new Club("premier league", "city"), "2021-2022", 87, Kledingmaat.M, new ClubSet(true, 1));
            Club club = new Club("premier league", "Leicester");
            truitje.ZetClub(club);

            Assert.Equal("premier league", truitje.Club.Competitie);
            Assert.Equal("Leicester", truitje.Club.Ploegnaam);
        }
        [Fact]
        public void Test_ZetClub_InValid()
        {
            Voetbaltruitje truitje = new Voetbaltruitje(new Club("premier league", "city"), "2021-2022", 87, Kledingmaat.M, new ClubSet(true, 1));
            Assert.Throws<TruiException>(() => truitje.ZetClub(null));
        }
        [Fact]
        public void Test_ZetClubSet_ValidReference()
        {
            Voetbaltruitje truitje = new Voetbaltruitje(new Club("premier league", "city"), "2021-2022", 87, Kledingmaat.M, new ClubSet(true, 1));
            ClubSet clubset = new ClubSet(true, 1);
            truitje.ZetClubSet(clubset);
            Assert.Equal(clubset, truitje.ClubSet);
        }
        [Fact]
        public void Test_ZetClubSet_ValidValue()
        {
            Voetbaltruitje truitje = new Voetbaltruitje(new Club("premier league", "city"), "2021-2022", 87, Kledingmaat.M, new ClubSet(true, 1));
            ClubSet clubset = new ClubSet(true, 1);
            truitje.ZetClubSet(clubset);

            Assert.True(truitje.ClubSet.Thuis);
            Assert.Equal(1, truitje.ClubSet.Versie);
        }
        [Fact]
        public void Test_ZetClubSet_InValid()
        {
            Voetbaltruitje truitje = new Voetbaltruitje(new Club("premier league", "city"), "2021-2022", 87, Kledingmaat.M, new ClubSet(true, 1));
            Assert.Throws<TruiException>(() => truitje.ZetClubSet(null));
        }

    

    }
}

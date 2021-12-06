using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLayer.Exceptions;
using VerkoopVoetbalTrui;

namespace BusinessLayer.Model
{
    public class Voetbaltruitje : IVoetbaltruitje
    {
        public Club Club { get; private set; }

        public ClubSet ClubSet { get; private set; }

        public int Id { get; private set; }

        public Kledingmaat Kledingmaat { get; set; }

        public double Prijs { get; private set; }

        public string Seizoen { get; private set; }
        public Voetbaltruitje(int id, Club club, string seizoen, double prijs, Kledingmaat kledingmaat, ClubSet clubSet)
        {
            ZetId(id);
            ZetClub(club);
            ZetSeizoen(seizoen);
            ZetPrijs(prijs);
            Kledingmaat = kledingmaat;
            ZetClubSet(clubSet);
        }
        public Voetbaltruitje(Club club, string seizoen, double prijs, Kledingmaat kledingmaat, ClubSet clubSet)
        {
            ZetClub(club);
            ZetSeizoen(seizoen);
            ZetPrijs(prijs);
            Kledingmaat = kledingmaat;
            ZetClubSet(clubSet);
        }

        public void ZetClub(Club club)
        {
            if (club == null) throw new TruiException("De club mag niet null zijn");
            Club = club;
        }

        public void ZetClubSet(ClubSet clubSet)
        {
            if (clubSet == null) throw new TruiException("De clubSet mag niet null zijn");
            ClubSet = clubSet;
        }

        public void ZetId(int id)
        {
            if (id <= 0) throw new TruiException("De ID mag niet kleiner dan 0 zijn");
            Id = id;
        }

        public void ZetPrijs(double prijs)
        {
            if (prijs <= 0) throw new TruiException("De prijs mag niet 0 of kleiner zijn");
            Prijs = prijs;
        }

        public void ZetSeizoen(string seizoen)
        {
            if (seizoen == null || seizoen == "") throw new TruiException("De naam mag niet leeg zijn");
            Seizoen = seizoen;
        }
        public override string ToString()
        {
            return  Id + " - " + Club.Ploegnaam + " - " + Seizoen +" - " + Prijs + " - " +Kledingmaat.ToString() +  " - " + ClubSet.Thuis + " - " + ClubSet.Versie;
        }
    }
}

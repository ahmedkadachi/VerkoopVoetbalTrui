using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLayer.Exceptions;

namespace BusinessLayer.Model
{
    public class Club
    {
        public Club(string competitie, string ploegnaam)
        {
            ZetCompetitie(competitie);
            ZetPloegnaam(ploegnaam);
        }
        public Club(int id,string competitie, string ploegnaam)
        {
            ZetCompetitie(competitie);
            ZetPloegnaam(ploegnaam);
            ZetClubtId(id);
        }
        public string Ploegnaam { get; private set; }
        public string Competitie { get; private set; }
        public int ClubId { get; private set; }

        public void ZetPloegnaam(string ploegnaam)
        {
            if (ploegnaam == null || ploegnaam == "") throw new ClubException("De ploegnaam mag niet leeg zijn");
            Ploegnaam = ploegnaam;
        }
        public void ZetCompetitie(string competitie)
        {
            if (competitie == null || competitie == "") throw new ClubException("De competitie naam mag niet leeg zijn");
            Competitie = competitie;
        }
        public void ZetClubtId(int id)
        {
            if (id <= 0) throw new ClubException("De ID mag niet kleiner dan 0 zijn");
            ClubId = id;
        }

        //public override bool Equals(object obj)
        //{
        //    return base.Equals(obj);
        //}
    }
}

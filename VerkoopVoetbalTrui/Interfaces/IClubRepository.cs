using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLayer.Model;

namespace BusinessLayer.Interfaces
{
    public interface IClubRepository
    {
        void VoegClub(Club club);
        bool BestaatClub(int clubId);
        bool BestaatClub(Club club);
        IReadOnlyList<Club> GeefClubs();
        IReadOnlyList<string> GeefCompetities();
        IReadOnlyList<string> GeefClubs(string competitie);
        Club GeefClub(int clubId);
        void VerwijderClub(Club club);
        void UpdateClub(Club club);
    }
}

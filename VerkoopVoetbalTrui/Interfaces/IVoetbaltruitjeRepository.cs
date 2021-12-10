using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLayer.Model;

namespace BusinessLayer.Interfaces
{
    public interface IVoetbaltruitjeRepository
    {
        void VoegVoetbaltruitjeToe(Voetbaltruitje truitje);
        bool BestaatVoetbaltruitje(Voetbaltruitje truitje);
        bool BestaatVoetbaltruitje(int voetbaltruitjeId);
        bool HeeftTruiBestellingen(Voetbaltruitje truitje);
        IReadOnlyList<Voetbaltruitje> GeefVoetbaltruitjes();
        public IReadOnlyList<Voetbaltruitje> GeefVoetbaltruitjesID(int id);
        public IReadOnlyList<Voetbaltruitje> GeefVoetbaltruitjesMaat(string maat);
        public IReadOnlyList<Voetbaltruitje> GeefVoetbaltruitjesSeizoen(string seizoen);
        public IReadOnlyList<Voetbaltruitje> GeefVoetbaltruitjesPrijs(double prijs);
        public IReadOnlyList<Voetbaltruitje> GeefVoetbaltruitjesVersie(string versie);
        public IReadOnlyList<Voetbaltruitje> GeefVoetbaltruitjesThuis(bool thuis);
        public IReadOnlyList<Voetbaltruitje> GeefVoetbaltruitjesPloeg(string ploeg);
        public IReadOnlyList<Voetbaltruitje> GeefVoetbaltruitjesCompetitie(string competitie);
        Voetbaltruitje GeefVoetbaltruitje(int voetbaltruitjeId);
        void VerwijderVoetbaltruitje(Voetbaltruitje truitje);
        void UpdateVoetbaltruitje(Voetbaltruitje truitje);
    }
}

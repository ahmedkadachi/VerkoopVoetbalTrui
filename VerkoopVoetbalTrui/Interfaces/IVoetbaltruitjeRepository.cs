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
        IReadOnlyList<Voetbaltruitje> GeefVoetbaltruitjes();
        Voetbaltruitje GeefVoetbaltruitje(int voetbaltruitjeId);
        void VerwijderVoetbaltruitje(Voetbaltruitje truitje);
        void UpdateVoetbaltruitje(Voetbaltruitje truitje);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLayer.Model;

namespace BusinessLayer.Interfaces
{
    public interface IKlantRepository
    {
        void VoegKlantToe(Klant klant);
        bool BestaatKlant(Klant klant);
        bool BestaatKlant(int klantId);
        IReadOnlyList<Klant> GeefKlanten();
        Klant GeefKlant(int klantId);
        void VerwijderKlant(Klant klant);
        void UpdateKlant(Klant klant);
    }
}

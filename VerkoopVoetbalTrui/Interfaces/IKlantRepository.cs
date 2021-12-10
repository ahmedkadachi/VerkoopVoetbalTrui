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
        bool HeeftKlantBestellingen(Klant klant);
        IReadOnlyList<Klant> GeefKlanten();
        Klant GeefKlant(int klantId);
        Klant GeefKlantNaam(string klantNaam);
        Klant GeefKlantAdres(string klantAdres);
        void VerwijderKlant(Klant klant);
        void UpdateKlant(Klant klant);
    }
}

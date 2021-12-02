using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLayer.Model;

namespace BusinessLayer.Interfaces
{
    public interface IBestellingRepository
    {
        void VoegBestellingToe(Bestelling bestelling);
        bool BestaatBestelling(Bestelling bestelling);
        bool BestaatBestelling(int bestellingId);
        IReadOnlyList<Bestelling> GeefBestellingen(int id);
        Bestelling GeefBestelling(int bestellingId);
        void VerwijderBestelling(Bestelling bestelling);
        void UpdateBestelling(Bestelling bestelling);
    }
}

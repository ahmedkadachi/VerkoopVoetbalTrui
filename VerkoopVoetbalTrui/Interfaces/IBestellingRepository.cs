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
        IReadOnlyList<Bestelling> GeefBestellingen();
        Bestelling GeefBestelling(int bestellingId);
        void VerwijderBestelling(Bestelling bestelling);
        void UpdateBestelling(Bestelling bestelling);
        IReadOnlyList<Bestelling> GeefBestellingenVanKlant(Klant klant);
        IReadOnlyList<Bestelling> GeefBestellingenTussenDatums(DateTime? startDatum, DateTime? eindDatum);
        Bestelling GeefBestellingVanKlant(int klantId);
        bool BestaatKlant(int klantId);
        bool BestaatKlant(Klant klant);
        Dictionary<Voetbaltruitje, int> GeefVoetbaltruiVanBestelling(int bestellingId);
        Klant GeefKlant(int klantId);
        void UpdateBestelTrui(int bestellingId, int truiId, int aantal);
    }
}

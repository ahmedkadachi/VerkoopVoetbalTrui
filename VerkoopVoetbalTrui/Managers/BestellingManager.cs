using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLayer.Exceptions;
using BusinessLayer.Interfaces;
using BusinessLayer.Model;

namespace BusinessLayer.Managers
{
    public class BestellingManager
    {
        public IBestellingRepository repo;

        public BestellingManager(IBestellingRepository repo)
        {
            this.repo = repo;
        }
        public void VoegBestellingToe(Bestelling bestelling)
        {
            try
            {
                if (repo.BestaatBestelling(bestelling))
                {
                    throw new BestellingManagerException("VoegBestellingToe - bestaat al");
                }
                else
                {
                    repo.VoegBestellingToe(bestelling);
                }
            }
            catch (Exception ex)
            {
                throw new BestellingManagerException("VoegBestellingToe " + ex);
            }
        }
        IReadOnlyList<Bestelling> GeefBestellingen()
        {
            try
            {
                
                return repo.GeefBestellingen();
                
            }
            catch (Exception ex)
            {
                throw new BestellingManagerException("GeefBestellingen " + ex);
            }
        }
        public Bestelling GeefBestelling(int bestellingId)
        {
            try
            {
                if (!repo.BestaatBestelling(bestellingId))
                {
                    throw new BestellingManagerException("GeefBestelling - bestaat niet");
                }
                else
                {
                    return repo.GeefBestelling(bestellingId);
                }
            }
            catch (Exception ex)
            {
                throw new BestellingManagerException("GeefBestelling " + ex);
            }
        }
        public void VerwijderBestelling(Bestelling bestelling)
        {
            try
            {
                if (!repo.BestaatBestelling(bestelling))
                {
                    throw new BestellingManagerException("VerwijderBestelling - bestaat niet");
                }
                else
                {
                    repo.VerwijderBestelling(bestelling);
                }
            }
            catch (Exception ex)
            {
                throw new BestellingManagerException("VerwijderBestelling " + ex);
            }
        }
        public void UpdateBestelling(Bestelling bestelling)
        {
            try
            {
                if (!repo.BestaatBestelling(bestelling))
                {
                    throw new BestellingManagerException("UpdateBestelling - bestaat niet");
                }
                else
                {
                    repo.UpdateBestelling(bestelling);
                }
            }
            catch (Exception ex)
            {
                throw new BestellingManagerException("UpdateBestelling " + ex);
            }
        }
        public IReadOnlyList<Bestelling> GeefBestellingenVanKlant(Klant klant)
        {
            try
            {
                if (!repo.BestaatKlant(klant))
                {
                    throw new BestellingManagerException("GeefBestellingVanKlant - bestaat niet");
                }
                else
                {
                    return repo.GeefBestellingenVanKlant(klant);
                }
            }
            catch (Exception ex)
            {
                throw new BestellingManagerException("GeefBestellingVanKlant " + ex);
            }
        }
        public IReadOnlyList<Bestelling> GeefBestellingenTussenDatums(DateTime? startDatum, DateTime? eindDatum)
        {
            try
            {
                return repo.GeefBestellingenTussenDatums(startDatum, eindDatum);
                
            }
            catch (Exception ex)
            {
                throw new BestellingManagerException("GeefBestellingenTussenDatums " + ex);
            }
        }
        public Bestelling GeefBestellingVanKlant(int klantId)
        {
            try
            {
                if (!repo.BestaatKlant(klantId))
                {
                    throw new BestellingManagerException("GeefBestellingVanKlant - bestaat niet");
                }
                else
                {
                    return repo.GeefBestellingVanKlant(klantId);
                }
            }
            catch (Exception ex)
            {
                throw new BestellingManagerException("GeefBestellingVanKlant " + ex);
            }
        }
        void UpdateBestelTrui(int bestellingId, int truiId, int aantal)
        {
            try
            {
                if (!repo.BestaatBestelling(bestellingId)) throw new BestellingManagerException("UpdateBestelTrui - BestelingID bestaat niet");
                repo.UpdateBestelTrui(bestellingId, truiId, aantal);
            }
            catch (Exception ex)
            {
                throw new BestellingManagerException("UpdateBestelTrui " + ex);
            }
        }
        public Klant GeefKlant(int klantId)
        {
            try
            {
                if (!repo.BestaatKlant(klantId)) throw new BestellingManagerException("GeefKlant - klant bestaat niet");
                return repo.GeefKlant(klantId);
            }
            catch (Exception ex)
            {
                throw new BestellingManagerException("GeefKlant " + ex);
            }
        }

    }
}

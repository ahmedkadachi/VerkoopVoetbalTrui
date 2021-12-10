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
    public class KlantManager
    {
        public IKlantRepository repo;

        public KlantManager(IKlantRepository repo)
        {
            this.repo = repo;
        }
        
        public IReadOnlyList<Klant> GeefKlanten()
        {
            try
            {
                return repo.GeefKlanten();
                
            }
            catch (Exception ex)
            {
                throw new KlantManagerException("GeefKlanten " + ex);
            }
        }
        public void VerwijderKlant(Klant klant)
        {
            try
            {
                if (!repo.BestaatKlant(klant))
                {
                    throw new KlantManagerException("VerwijderKlant - bestaat niet");
                }
                else if (repo.HeeftKlantBestellingen(klant))
                {
                    throw new KlantManagerException("VerwijderKlant - Klant heeft al bestellingen");
                }
                else
                {
                    repo.VerwijderKlant(klant);
                }
            }
            catch (Exception ex)
            {
                throw new KlantManagerException("VerwijderKlant " + ex);
            }
        }
        public Klant GeefKlant(int klantId)
        {
            try
            {
                if (!repo.BestaatKlant(klantId))
                {
                    throw new KlantManagerException("GeefKlant - bestaat niet");
                }
                else
                {
                    return repo.GeefKlant(klantId);
                }
            }
            catch (Exception ex)
            {
                throw new KlantManagerException("GeefKlant " + ex);
            }
        }
        public Klant GeefKlantNaam(string klantNaam)
        {
            try
            {
                return repo.GeefKlantNaam(klantNaam);
            }
            catch (Exception ex)
            {
                throw new KlantManagerException("GeefKlantNaam " + ex);
            }
        }
        public Klant GeefKlantAdres(string klantAdres)
        {
            try
            {
                return repo.GeefKlantAdres(klantAdres);
            }
            catch (Exception ex)
            {
                throw new KlantManagerException("GeefKlantNaam " + ex);
            }
        }

        public void UpdateKlant(Klant klant)
        {
            try
            {
                if (!repo.BestaatKlant(klant))
                {
                    throw new KlantManagerException("UpdateKlant - bestaat niet");
                }
                else
                {
                    repo.UpdateKlant(klant);
                }
            }
            catch (Exception ex)
            {
                throw new KlantManagerException("UpdateKlant " + ex);
            }
        }
        public void VoegKlantToe(Klant klant)
        {
            try
            {
                if (repo.BestaatKlant(klant))
                {
                    throw new KlantManagerException("VoegKlantToe - bestaat al");
                }
                else
                {
                    repo.VoegKlantToe(klant);
                }
            }
            catch (Exception ex)
            {
                throw new KlantManagerException("VoegKlantToe " + ex);
            }
        }
    }
}

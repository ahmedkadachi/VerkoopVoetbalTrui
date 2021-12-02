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
    }
}

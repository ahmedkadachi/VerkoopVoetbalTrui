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
    public class VoetbaltruitjeManager
    {

        public IVoetbaltruitjeRepository repo;
        public VoetbaltruitjeManager(IVoetbaltruitjeRepository repo)
        {
            this.repo = repo;
        }

        public void VoegVoetbaltruitjeToe(Voetbaltruitje truitje)
        {
            try
            {
                if (repo.BestaatVoetbaltruitje(truitje))
                {
                    throw new VoetbaltruitjeManagerException("VoegTruitjeToe - bestaat al");
                }
                else
                {
                    repo.VoegVoetbaltruitjeToe(truitje);
                }
            }
            catch (Exception ex)
            {
                throw new VoetbaltruitjeManagerException("VoegTruitjeToe " + ex);
            }
        }
        public IReadOnlyList<Voetbaltruitje> GeefVoetbaltruitjes()
        {
            try
            {
                return repo.GeefVoetbaltruitjes();
            }
            catch (Exception ex)
            {
                throw new VoetbaltruitjeManagerException("GeefVoetbaltruitjes " + ex);
            }
        }
        public IReadOnlyList<Voetbaltruitje> GeefVoetbaltruitjesID(int id)
        {
            try
            {
                return repo.GeefVoetbaltruitjesID(id);
            }
            catch (Exception ex)
            {
                throw new VoetbaltruitjeManagerException("GeefVoetbaltruitjesID " + ex);
            }
        }
        public IReadOnlyList<Voetbaltruitje> GeefVoetbaltruitjesMaat(string maat)
        {
            try
            {
                return repo.GeefVoetbaltruitjesMaat(maat);
            }
            catch (Exception ex)
            {
                throw new VoetbaltruitjeManagerException("GeefVoetbaltruitjesMaat " + ex);
            }
        }
        public IReadOnlyList<Voetbaltruitje> GeefVoetbaltruitjesSeizoen(string seizoen)
        {
            try
            {
                return repo.GeefVoetbaltruitjesSeizoen(seizoen);
            }
            catch (Exception ex)
            {
                throw new VoetbaltruitjeManagerException("GeefVoetbaltruitjesSeizoen " + ex);
            }
        }
        public IReadOnlyList<Voetbaltruitje> GeefVoetbaltruitjesPrijs(double prijs)
        {
            try
            {
                return repo.GeefVoetbaltruitjesPrijs(prijs);
            }
            catch (Exception ex)
            {
                throw new VoetbaltruitjeManagerException("GeefVoetbaltruitjesPrijs " + ex);
            }
        }
        public IReadOnlyList<Voetbaltruitje> GeefVoetbaltruitjesVersie(string versie)
        {
            try
            {
                return repo.GeefVoetbaltruitjesVersie(versie);
            }
            catch (Exception ex)
            {
                throw new VoetbaltruitjeManagerException("GeefVoetbaltruitjesVersie " + ex);
            }
        }
        public IReadOnlyList<Voetbaltruitje> GeefVoetbaltruitjesThuis(bool thuis)
        {
            try
            {
                return repo.GeefVoetbaltruitjesThuis(thuis);
            }
            catch (Exception ex)
            {
                throw new VoetbaltruitjeManagerException("GeefVoetbaltruitjesThuis " + ex);
            }
        }
        public IReadOnlyList<Voetbaltruitje> GeefVoetbaltruitjesPloeg(string ploeg)
        {
            try
            {
                return repo.GeefVoetbaltruitjesPloeg(ploeg);
            }
            catch (Exception ex)
            {
                throw new VoetbaltruitjeManagerException("GeefVoetbaltruitjesThuis " + ex);
            }
        }
        public IReadOnlyList<Voetbaltruitje> GeefVoetbaltruitjesCompetitie(string competitie)
        {
            try
            {
                return repo.GeefVoetbaltruitjesCompetitie(competitie);
            }
            catch (Exception ex)
            {
                throw new VoetbaltruitjeManagerException("GeefVoetbaltruitjesCompetitie " + ex);
            }
        }

        public Voetbaltruitje GeefVoetbaltruitje(int voetbaltruitjeId)
        {
            try
            {
                if (!repo.BestaatVoetbaltruitje(voetbaltruitjeId))
                {
                    throw new VoetbaltruitjeManagerException("GeefVoetbaltruitje - bestaat niet");
                }
                else
                {
                    return repo.GeefVoetbaltruitje(voetbaltruitjeId);
                }
            }
            catch (Exception ex)
            {
                throw new VoetbaltruitjeManagerException("GeefVoetbaltruitje " + ex);
            }
        }
        public void VerwijderVoetbaltruitje(Voetbaltruitje truitje)
        {
            try
            {
                if(!repo.BestaatVoetbaltruitje(truitje))
                {
                    throw new VoetbaltruitjeManagerException("VerwijderVoetbaltruitje - bestaat niet");
                }
                else
                {
                    repo.VerwijderVoetbaltruitje(truitje);
                }
            }
            catch (Exception ex)
            {
                throw new VoetbaltruitjeManagerException("VerwijderVoetbaltruitje " + ex);
            }
        }
        public void UpdateVoetbaltruitje(Voetbaltruitje truitje)
        {
            try
            {
                if (!repo.BestaatVoetbaltruitje(truitje))
                {
                    throw new VoetbaltruitjeManagerException("UpdateVoetbaltruitje - bestaat niet");
                }
                else
                {
                    repo.UpdateVoetbaltruitje(truitje);
                }
            }
            catch(Exception ex)
            {
                throw new VoetbaltruitjeManagerException("UpdateVoetbaltruitje " + ex);
            }
        }

        
    }
}

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
        IReadOnlyList<Voetbaltruitje> GeefVoetbaltruitjes()
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

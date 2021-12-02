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
    public class ClubManager
    {
        public IClubRepository repo;

        public ClubManager(IClubRepository repo)
        {
            this.repo = repo;
        }
        public void VoegClub(Club club)
        {
            try
            {
                if (repo.BestaatClub(club))
                {
                    throw new ClubManagerException("VoegClubToe - bestaat al");
                }
                else
                {
                    repo.VoegClub(club);
                }
            }
            catch (Exception ex)
            {
                throw new ClubManagerException("VoegClubToe " + ex);
            }
        }
        IReadOnlyList<Club> GeefClubs()
        {
            try
            {
                return repo.GeefClubs();
            }
            catch (Exception ex)
            {
                throw new ClubManagerException("GeefClubs " + ex);
            }
        }
        public Club GeefClub(int clubId)
        {
            try
            {
                if (!repo.BestaatClub(clubId))
                {
                    throw new ClubManagerException("GeefClub - bestaat niet");
                }
                else
                {
                    return repo.GeefClub(clubId);
                }
            }
            catch (Exception ex)
            {
                throw new ClubManagerException("GeefClub " + ex);
            }
        }
        public void VerwijderClub(Club club)
        {
            try
            {
                if (!repo.BestaatClub(club))
                {
                    throw new ClubManagerException("VerwijderClub - bestaat niet");
                }
                else
                {
                    repo.VerwijderClub(club);
                }
            }
            catch (Exception ex)
            {
                throw new ClubManagerException("VerwijderClub " + ex);
            }
        }
        public void UpdateClub(Club club)
        {
            try
            {
                if (!repo.BestaatClub(club))
                {
                    throw new ClubManagerException("UpdateClub - bestaat niet");
                }
                else
                {
                    repo.UpdateClub(club);
                }
            }
            catch (Exception ex)
            {
                throw new ClubManagerException("UpdateClub " + ex);
            }
        }
        
    }
}

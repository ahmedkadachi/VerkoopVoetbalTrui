using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLayer.Exceptions;

namespace BusinessLayer.Model
{
    public class ClubSet
    {
        public ClubSet(bool thuis, int versie)
        {
            ZetThuis(thuis);
            ZetVersie(versie);
            
        }
        public bool Thuis { get; private set; }
        public int Versie { get; private set; }

        public void ZetVersie(int versie)
        {
            if (versie <= 0) throw new ClubSetException("De versie mag niet 0 zijn");
            Versie = versie;
        }
        public void ZetThuis(bool thuis)
        {
            if (thuis == null) throw new ClubSetException("Thuis mag niet leeg zijn");
            Thuis = thuis;
        }
        public override string ToString()
        {
            string thuis = "";
            if (Thuis) thuis = "Thuis";
            if (!Thuis) thuis = "Uit";
            return thuis + " " + Versie;
        }
    }

}

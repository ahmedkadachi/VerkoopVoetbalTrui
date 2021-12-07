using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Model
{
    public class TruitjeData : INotifyPropertyChanged
    {
        public TruitjeData(Voetbaltruitje truitje, int aantal)
        {
            Truitje = truitje;
            Aantal = aantal;
        }
        public Voetbaltruitje Truitje { get; set; }
        private int aantal;

        public int Aantal
        {
            get { return aantal; }
            set
            {
                aantal = value;
                OnPropertyChanged();
            }
        }
        private void OnPropertyChanged(string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        public event PropertyChangedEventHandler PropertyChanged;
    }
}

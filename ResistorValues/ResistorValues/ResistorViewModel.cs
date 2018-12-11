using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using ResistorValues.Annotations;
using Xamarin.Forms;

namespace ResistorValues
{
    class ResistorViewModel : INotifyPropertyChanged
    {
        private int _bandOne;
        public int BandOne { get; private set; }
        public string OneColor { get; private set; }
        public int BandTwo { get; private set; }
        public int Multiplier { get; private set; }

        public ICommand BandOneCommand { get; set; }
        public ICommand BandTwoCommand { get; set; }
        public ICommand MultiplierCommand { get; set; }

        public ResistorViewModel()
        {
            BandOne = 0;
            BandTwo = 0;
            Multiplier = 1;
            BandOneCommand = new Command(BandOneClick);
            BandTwoCommand = new Command(BandTwoClick);
            MultiplierCommand = new Command(MultiplierClick);
        }
        public void BandOneClick()
        {
            BandOne++;
            if (BandOne == 10) BandOne = 0;
            OnPropertyChanged("BandOne");
        }
        public void BandTwoClick()
        {
            BandTwo++;
            if (BandTwo == 10) BandTwo = 0;
            OnPropertyChanged("BandTwo");
        }
        public void MultiplierClick()
        {
            Multiplier = Multiplier * 10;
            if (Multiplier == 10000000) Multiplier = 1;
            OnPropertyChanged("Multiplier");
        }


        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public enum BandColors
    {
        Black = 0,
        Brown = 1,
        Red = 2,
        Orange = 3,
        Yellow = 4,
        Green = 5,
        Blue = 6,
        Violet = 7,
        Grey = 8,
        White = 9,


    }
}

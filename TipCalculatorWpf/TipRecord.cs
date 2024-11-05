using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TipCalculatorWpf
{
    public class TipRecord : INotifyPropertyChanged
    {
        private decimal amount;
        private int tip;

        public decimal Amount
        {
            get => amount;
            set
            {
                if (amount != value)
                {
                    amount = value;
                    OnPropertyChanged(nameof(Amount));
                    OnPropertyChanged(nameof(Total));
                }
            }
        }

        public int Tip
        {
            get => tip;
            set
            {
                if (tip != value)
                {
                    tip = value;
                    OnPropertyChanged(nameof(Tip));
                    OnPropertyChanged(nameof(Total));
                }
            }
        }

        public decimal Total => Math.Round(Amount * (1 + Tip / 100m), 2);

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}

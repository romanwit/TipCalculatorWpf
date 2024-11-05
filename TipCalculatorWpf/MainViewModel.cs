using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace TipCalculatorWpf
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<TipRecord> Records { get; } = new ObservableCollection<TipRecord>();

        public decimal TotalAmount => Records.Sum(record => record.Amount);

        public ICommand AddRecordCommand { get; }

        public MainViewModel()
        {
            AddRecordCommand = new RelayCommand(AddRecord);
            Records.CollectionChanged += (s, e) => OnPropertyChanged(nameof(TotalAmount));
        }

        private void AddRecord()
        {
            Records.Add(new TipRecord());
            OnPropertyChanged(nameof(TotalAmount));
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}

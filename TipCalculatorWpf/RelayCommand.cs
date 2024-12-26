using System;
using System.Windows.Input;

namespace TipCalculatorWpf
{
    public class RelayCommand : ICommand
    {
        private readonly Action _execute;
        private readonly Action<object> _executeWithParam;
        private readonly Predicate<object> _canExecute;

        public RelayCommand(Action execute) : this(execute, null) { }

        public RelayCommand(Action<object> executeWithParam) : this(executeWithParam, null) { }

        public RelayCommand(Action execute, Predicate<object> canExecute)
        {
            _execute = execute;
            _canExecute = canExecute;
        }

        public RelayCommand(Action<object> executeWithParam, Predicate<object> canExecute)
        {
            _executeWithParam = executeWithParam;
            _canExecute = canExecute;
        }

        public bool CanExecute(object parameter)
        {
            return _canExecute == null || _canExecute(parameter);
        }

        public void Execute(object parameter)
        {
            if (_execute != null)
            {
                _execute();
            }
            else if (_executeWithParam != null)
            {
                _executeWithParam(parameter);
            }
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
    }
}

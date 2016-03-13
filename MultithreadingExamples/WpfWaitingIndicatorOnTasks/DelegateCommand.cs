using System;
using System.Windows.Input;

namespace WpfWaitingIndicatorOnTasks
{
    public class DelegateCommand : ICommand
    {
        private Action _action;
        private Func<bool> _canExecute;

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return _canExecute != null && _canExecute();
        }

        public void Execute(object parameter)
        {
            _action();
        }
        
        public DelegateCommand(Action action,  Func<bool> canExecute)
        {
            _action = action;
            _canExecute = canExecute;
        }
    }
}

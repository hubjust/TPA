using System;
using System.Windows.Input;

namespace ViewModel
{
    public class RelayCommand : ICommand
    {

        public RelayCommand(Action execute) : this(execute, null) {}
        public RelayCommand(Action execute, Func<bool> canExecute)
        {
            this.execute = execute ?? throw new ArgumentNullException(nameof(execute));
            this.canExecute = canExecute;
        }

        public bool CanExecute(object parameter)
        {
            if (canExecute == null)
                return true;
            if (parameter == null)
                return canExecute();
            return canExecute();
        }

        public virtual void Execute(object parameter)
        {
            execute();
        }

        public event EventHandler CanExecuteChanged;
        internal void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }

        private readonly Action execute;
        private readonly Func<bool> canExecute;

    }
}

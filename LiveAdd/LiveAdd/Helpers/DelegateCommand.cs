using System;
using System.Windows.Input;

namespace LiveAdd.Helpers
{
    public class DelegateCommand<T> : ICommand
    {
        private Action<T> execute;

        public DelegateCommand(Action<T> excecute)
        {
            this.execute = excecute;
        }
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            this.execute((T)parameter);
        }
    }
}

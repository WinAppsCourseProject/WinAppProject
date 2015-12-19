using System;
using System.Windows.Input;

namespace LiveAdd.Helpers
{
    public class DelegateCommand : ICommand
    {
        private Action execute;

        public DelegateCommand(Action excecute)
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
            this.execute();
        }
    }
}

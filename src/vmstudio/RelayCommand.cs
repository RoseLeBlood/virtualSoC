using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace vmstudio
{
    public class RelayCommand : ICommand
    {

        private readonly Action<object> ExecuteAction;
        private readonly Predicate<object> CanExecuteAction;


        public RelayCommand(Action<object> execute)
          : this(execute, _ => true)
        {
        }
        public RelayCommand(Action<object> action, Predicate<object> canExecute)
        {
            ExecuteAction = action;
            CanExecuteAction = canExecute;
        }

        public bool CanExecute(object parameter)
        {
            return CanExecuteAction(parameter);
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public void Execute(object parameter)
        {
            ExecuteAction(parameter);
        }

       
    }

}

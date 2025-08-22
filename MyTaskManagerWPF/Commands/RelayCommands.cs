using System.Windows.Input;

namespace MyTaskManagerWPF.Commands
{
    public class RelayCommands : ICommand
    {
        public event EventHandler? CanExecuteChanged;
        private Action<object> executeAction { get; set; }
        private Predicate<object> canExecutePredicate { get; set; }

        public RelayCommands(Action<object> _executeMethod, Predicate<object> _canExecuteMethod)
        {
            executeAction = _executeMethod;
            canExecutePredicate = _canExecuteMethod;
        }

        public bool CanExecute(object? parameter)
        {
            return canExecutePredicate.Invoke(parameter);
        }

        public void Execute(object? parameter)
        {
            executeAction(parameter);
        }
    }
}

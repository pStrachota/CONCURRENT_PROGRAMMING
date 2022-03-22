using System;
using System.Windows.Input;

namespace CSHARP_PW_PROJECT.Commands
{
    internal class CircleCommands : ICommand
    {
        private readonly Func<object, bool> _canExecuteAction;
        private readonly Action<object> _executeAction;
        public CircleCommands(Action<object> executeAction, Func<object, bool> canExecuteAction)
        {
            _executeAction = executeAction;
            _canExecuteAction = canExecuteAction;
        }
        public void InvokeCanExecuteChanged() => CanExecuteChanged?.Invoke(this, EventArgs.Empty);

        public event EventHandler? CanExecuteChanged;
        public bool CanExecute(object? parameter) => _canExecuteAction?.Invoke(parameter) ?? true;
        public void Execute(object? parameter) => _executeAction(parameter);
    }
}

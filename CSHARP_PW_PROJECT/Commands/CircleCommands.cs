using System;
using System.Windows.Input;

namespace CSHARP_PW_PROJECT.Commands
{

    /// <summary>
    /// Helper CircleCommand class inherit from ICommand interface
    /// In CircleViewModel.cs we wrap this custom class in ICommand
    /// and then pass it for binding in CircleView.xaml
    /// </summary>
    internal class CircleCommands : ICommand
    {
        /// <summary>
        /// Func<object, bool> - takes object, return bool
        /// Action<object> - takes object, return void
        /// equivalent for java functional interfaces
        /// </summary>
        private readonly Func<object, bool> _canExecuteAction;
        private readonly Action<object> _executeAction;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="executeAction"> action to execute</param>
        /// <param name="canExecuteAction"> condition that must be met to enable above action</param>
        public CircleCommands(Action<object> executeAction, Func<object, bool> canExecuteAction)
        {
            _executeAction = executeAction;
            _canExecuteAction = canExecuteAction;
        }

        /// <summary>
        /// => means 'Expression-Bodied Members'
        /// it is used to simplify methods body
        /// </summary>
        public void InvokeCanExecuteChanged() => CanExecuteChanged?.Invoke(this, EventArgs.Empty);

        public event EventHandler? CanExecuteChanged;
        public bool CanExecute(object? parameter) => _canExecuteAction?.Invoke(parameter) ?? true;
        public void Execute(object? parameter) => _executeAction(parameter);
    }
}

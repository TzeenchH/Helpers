using System;
using System.Windows.Input;

namespace HelpersLib
{ 
    public class DelegateCommand<T> : ICommand
    {
        protected Action<T> _execute;
        protected Func<T, bool> _canExecute;

        protected DelegateCommand() { }

        public DelegateCommand(Action<T> execute, Func<T, bool> canExecute)
        {
            if (execute == null)
                throw new ArgumentNullException(nameof(execute));
            _execute = execute;
            _canExecute = canExecute ?? (p => true);
        }
        public DelegateCommand(Action<T> execute) : this(execute, (Func<T, bool>)null) { }
        public DelegateCommand(Action<T> execute, Func<bool> canExecute) : this(execute, p => canExecute()) { }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public void Execute(object parameter) => _execute((T)parameter);
        public bool CanExecute(object parameter) => _canExecute((T)parameter);
    }

    public class DelegateCommand : DelegateCommand<object>
    {
        public DelegateCommand(Action<object> execute, Func<object, bool> canExecute) : base(execute, canExecute) { }

        public DelegateCommand(Action execute) : base(p => execute()) { }
        public DelegateCommand(Action execute, Func<bool> canExecute) : base(p => execute(), p => canExecute()) { }

        public DelegateCommand(Action<object> execute) : base(execute) { }
        public DelegateCommand(Action<object> execute, Func<bool> canExecute) : base(execute, canExecute) { }
    }
}

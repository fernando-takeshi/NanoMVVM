using System;
using System.Windows.Input;

namespace NanoMVVM.Commands
{
    public class DelegateCommand : DelegateCommand<object>
    {
        #region Constructors

        public DelegateCommand(Action pExecute) : base(vCommand => pExecute()) { }

        public DelegateCommand(Action pExecute, Func<bool> pCanExecute) : base(vCommand => pExecute(), vCommand => pCanExecute()) { }

        #endregion
    }

    public class DelegateCommand<T> : ICommand, IRaiseCanExecuteChanged
    {
        #region Fields

        private readonly Func<T, bool> fCanExecute;
        private readonly Action<T> fExecute;
        private bool fIsExecuting;

        #endregion

        #region Events

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        #endregion

        #region Constructors

        public DelegateCommand(Action<T> pExecute) : this(pExecute, null) { }

        public DelegateCommand(Action<T> pExecute, Func<T, bool> pCanExecute)
        {
            if ((pExecute == null))
            {
                throw new ArgumentException("pExecute", "Method to execute cannot be null");
            }

            fExecute = pExecute;
            fCanExecute = pCanExecute;
        }

        #endregion

        #region ICommand methods

        bool ICommand.CanExecute(object pParameter)
        {
            return !fIsExecuting && CanExecute((T)pParameter);
        }

        void ICommand.Execute(object pParameter)
        {
            fIsExecuting = true;

            try
            {
                RaiseCanExecuteChanged();
                Execute((T)pParameter);
            }
            finally
            {
                fIsExecuting = false;
                RaiseCanExecuteChanged();
            }
        }

        #endregion

        #region Public methods

        public void RaiseCanExecuteChanged()
        {
            CommandManager.InvalidateRequerySuggested();
        }

        public bool CanExecute(T pParameter)
        {
            return (fCanExecute == null) ? true : fCanExecute(pParameter);
        }

        public void Execute(T pParameter)
        {
            fExecute(pParameter);
        }

        #endregion
    }
}

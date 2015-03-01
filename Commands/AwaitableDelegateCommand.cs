using System;
using System.Threading.Tasks;
using System.Windows.Input;

namespace NanoMVVM.Commands
{
    public class AwaitableDelegateCommand : AwaitableDelegateCommand<object>, IAsyncCommand
    {
        #region Constructors

        public AwaitableDelegateCommand(Func<Task> pExecute) : base(vCommand => pExecute()) { }

        public AwaitableDelegateCommand(Func<Task> pExecute, Func<bool> pCanExecute) : base(vCommand => pExecute(), vCommand => pCanExecute()) { }

        #endregion
    }

    public class AwaitableDelegateCommand<T> : IAsyncCommand<T>, ICommand
    {
        #region Fields

        private readonly Func<T, Task> fExecute;
        private readonly DelegateCommand<T> fDelegateCommand;
        private bool fIsExecuting;

        #endregion

        #region Properties

        public ICommand Command { get { return this; } }

        #endregion

        #region Events

        public event EventHandler CanExecuteChanged
        {
            add { fDelegateCommand.CanExecuteChanged += value; }
            remove { fDelegateCommand.CanExecuteChanged -= value; }
        }

        #endregion

        #region Constructors

        public AwaitableDelegateCommand(Func<T, Task> pExecute) : this(pExecute, pCanExecute => true) { }

        public AwaitableDelegateCommand(Func<T, Task> pExecute, Func<T, bool> pCanExecute)
        {
            fExecute = pExecute;
            fDelegateCommand = new DelegateCommand<T>(vExecuteDummy => { }, pCanExecute);
        }

        #endregion

        #region Public methods

        public void RaiseCanExecuteChanged()
        {
            fDelegateCommand.RaiseCanExecuteChanged();
        }

        public bool CanExecute(object pParameter)
        {
            return !fIsExecuting && fDelegateCommand.CanExecute((T)pParameter);
        }

        public async void Execute(object pParameter)
        {
            await ExecuteAsync((T)pParameter);
        }

        public async Task ExecuteAsync(T pObject)
        {
            try
            {
                fIsExecuting = true;
                RaiseCanExecuteChanged();
                await fExecute(pObject);
            }
            finally
            {
                fIsExecuting = false;
                RaiseCanExecuteChanged();
            }
        }

        #endregion
    }
}

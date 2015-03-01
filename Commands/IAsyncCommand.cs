using System.Threading.Tasks;
using System.Windows.Input;

namespace NanoMVVM.Commands
{
    internal interface IAsyncCommand : IAsyncCommand<object> { }

    internal interface IAsyncCommand<in T> : IRaiseCanExecuteChanged
    {
        #region Properties

        ICommand Command { get; }

        #endregion

        #region Methods

        bool CanExecute(object pObject);

        Task ExecuteAsync(T pObject);

        #endregion
    }
}

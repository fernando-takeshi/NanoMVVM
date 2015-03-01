using System.Windows.Input;

namespace NanoMVVM.Commands
{
    internal interface IRaiseCanExecuteChanged
    {
        #region Methods

        void RaiseCanExecuteChanged();

        #endregion
    }
}

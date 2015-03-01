using System.Windows.Input;

namespace NanoMVVM.Commands
{
    internal static class CommandExtensions
    {
        #region Public methods

        public static void RaiseCanExecuteChanged(this ICommand pCommand)
        {
            IRaiseCanExecuteChanged vCanExecuteChanged = pCommand as IRaiseCanExecuteChanged;

            if (vCanExecuteChanged != null)
            {
                vCanExecuteChanged.RaiseCanExecuteChanged();
            }
        }

        #endregion
    }
}

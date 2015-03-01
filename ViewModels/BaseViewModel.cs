using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace NanoMVVM.ViewModels
{
    public abstract class BaseViewModel : INotifyPropertyChanged
    {
        #region Fields

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Protected methods

        protected void RaisePropertyChanged([CallerMemberName] string pPropertyName = "")
        {
            OnPropertyChanged(new PropertyChangedEventArgs(pPropertyName));
        }

        protected virtual void OnPropertyChanged(PropertyChangedEventArgs pArgs)
        {
            PropertyChangedEventHandler vHandler = this.PropertyChanged;

            if (vHandler != null)
            {
                vHandler(this, pArgs);
            }
        }

        #endregion
    }
}

using System.ComponentModel;
using System.Runtime.CompilerServices;
using PropertyChanged;

namespace PlotApp.MVVM.ViewModels.BaseViewModel {
    [AddINotifyPropertyChangedInterface]
    public abstract class BaseViewModel : INotifyPropertyChanged {
        public event PropertyChangedEventHandler? PropertyChanged;

        /// <summary>
        ///     Inform view, that some property have changed
        /// </summary>
        /// <param name="propertyName">
        ///     Caller property name
        /// </param>
        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}

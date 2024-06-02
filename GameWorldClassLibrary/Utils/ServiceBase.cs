using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace GameWorldClassLibrary.Resources.Utils
{
    public class ServiceBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
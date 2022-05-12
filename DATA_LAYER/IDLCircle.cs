using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace DATA_LAYER
{
    public interface IDLCircle : INotifyPropertyChanged
    {
        int X { get; }
        int Y { get; }
        int R { get; }
        int Speed { get; }
        void RaisePropertyChanged(string propertyName);

    }
}

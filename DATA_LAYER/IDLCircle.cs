using System.ComponentModel;


namespace DATA_LAYER
{
    public interface IDLCircle : INotifyPropertyChanged
    {
        int X { get; }
        int Y { get; }
        int R { get; }
        int Speed { get; }
        void RaisePropertyChanged(string propertyName = null);

    }
}

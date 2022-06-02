using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Logic
{
    public interface IBLCircle : INotifyPropertyChanged
    {
        int X { get; set; }
        int Y { get; set; }
        int R { get; }
        double Mass { get; }
        double VelocityX { get; set; }
        double VelocityY { get; set; }
        double LastUpdate { get; set; }
        string Name { get; set; }
        void RaisePropertyChanged(string propertyName);

    }
}

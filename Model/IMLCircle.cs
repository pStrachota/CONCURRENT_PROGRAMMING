using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Model
{
    public interface IMLCircle : INotifyPropertyChanged
    {
        int X_Center { get; }
        int Y_Center { get; }
        int Radius { get; }
        int Diameter { get; }
        int CenterTransform { get; }
        string Color { get; }

        void RaisePropertyChanged(string propertyName);
    }
}

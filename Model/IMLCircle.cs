using System.ComponentModel;

namespace Presentation.Model
{
    public interface IMLCircle : INotifyPropertyChanged
    {
        int X_Center { get; }
        int Y_Center { get; }
        int Radius { get; }
        int Diameter { get; }
        int CenterTransform { get; }
        string color { get; }

        void RaisePropertyChanged(string propertyName = null);
    }
}

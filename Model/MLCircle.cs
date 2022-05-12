using Logic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Model
{
    public class MLCircle : IMLCircle
    {
        private readonly List<string?> _colorList = new()
        {
            "Black",
            "Yellow",
            "Red",
            "Blue",
            "Brown",
            "Purple",
            "Orange",
            "Green",
            "Pink",
            "Gray"
        };

        public MLCircle(IBLCircle ball)
        {
            ball.PropertyChanged += BallPropertyChanged;
            X_Center = ball.X;
            Y_Center = ball.Y;
            Radius = ball.R;
            this.Color = _colorList.ElementAt(new Random().Next(0, 8));
        }

        private int x_center;
        private int y_center;
        private int radius;

        public int Radius
        {
            get => radius;
            set
            {
                radius = value;
                RaisePropertyChanged(nameof(Radius));
            }

        }
        public int X_Center
        {
            get => x_center;
            set
            {
                x_center = value;
                RaisePropertyChanged(nameof(X_Center));
            }
        }
        public int Y_Center
        {
            get => y_center;
            set
            {
                y_center = value;
                RaisePropertyChanged(nameof(Y_Center));
            }
        }
        public int CenterTransform { get => -1 * Radius; }
        public string Color { get; }
        public int Diameter { get => 2 * Radius; }

        public event PropertyChangedEventHandler? PropertyChanged;

        public void RaisePropertyChanged(string propertyName)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        private void BallPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            IBLCircle b = (IBLCircle)sender;
            switch (e.PropertyName)
            {
                case "X":
                    X_Center = b.X;
                    break;
                case "Y":
                    Y_Center = b.Y;
                    break;
            }
        }
    }
}

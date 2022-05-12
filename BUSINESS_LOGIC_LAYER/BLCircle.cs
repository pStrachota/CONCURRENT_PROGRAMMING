using System.ComponentModel;
using System.Runtime.CompilerServices;
using DATA_LAYER;

namespace Logic
{
    internal class BLCircle : IBLCircle
    {
        internal BLCircle(IDLCircle iball)
        {
            Random r = new();
            double angle = 2 * Math.PI * r.NextDouble();

            _x = iball.X;
            _y = iball.Y;
            _velocityX = iball.Speed * Math.Cos(angle);
            _velocityY = iball.Speed * Math.Cos(angle);
            _r = iball.R;
            _mass = Math.PI * Math.Pow(_r, 2);
        }

        private int _x;
        private int _y;
        private int _r;
        private double _mass;
        private double _velocityX;
        private double _velocityY;
        private double _lastUpdate = 0;
        public double LastUpdate
        {
            get => _lastUpdate;
            set => _lastUpdate = value;
        }
        public int R
        {
            get => _r;
        }

        public double Mass
        {
            get => _mass;
        }

        public double VelocityX
        {
            get => _velocityX;
            set => _velocityX = value;
        }

        public double VelocityY
        {
            get => _velocityY;
            set => _velocityY = value;
        }

        public int X
        {
            get => _x;
            set
            {
                _x = value;
                RaisePropertyChanged(nameof(X));
            }
        }

        public int Y
        {
            get => _y;
            set
            {
                _y = value;
                RaisePropertyChanged(nameof(Y));
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        public void RaisePropertyChanged(string propertyName)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
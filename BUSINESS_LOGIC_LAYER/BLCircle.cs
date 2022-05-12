using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows;
using DATA_LAYER;

namespace Logic
{
    internal class BLCircle : IBLCircle
    {

        internal BLCircle(IDLCircle iball)
        {
            Random r = new Random();
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

        public double LastUpdate { get => _lastUpdate; set => _lastUpdate = value; }




        private double calculateDx(double v1, double v2, double m1, double m2, double theta1,
            double theta2, double phi)
        {
            return (v1 * Math.Cos(theta1 - phi) * (m1 - m2) + 2 * m2 * v2 * Math.Cos(theta2 - phi))
                / (m1 + m2) * Math.Cos(phi) + v1 * Math.Sin(theta1 - phi) * Math.Cos(phi + Math.PI / 2);
        }

        private double calculateDy(double v1, double v2, double m1, double m2, double theta1,
            double theta2, double phi)
        {
            return (v1 * Math.Cos(theta1 - phi) * (m1 - m2) + 2 * m2 * v2 * Math.Cos(theta2 - phi))
                / (m1 + m2) * Math.Sin(phi) + v1 * Math.Sin(theta1 - phi) * Math.Sin(phi + Math.PI / 2);
        }
        
        #region interface implementation

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

      
        public int R
        {
            get => _r;
            set
            {
                if (value > 0)
                {
                    _r = value;
                }

                else
                {
                    throw new ArgumentException();
                }
            }
        }
        public double Mass
        {
            get => _mass;
            set => _mass = value;
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

        public event PropertyChangedEventHandler PropertyChanged;


        public void RaisePropertyChanged(string propertyName)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

   

        #endregion
    }
}
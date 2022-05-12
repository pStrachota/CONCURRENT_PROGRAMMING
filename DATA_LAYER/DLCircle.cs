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

namespace DATA_LAYER
{
    internal class DLCircle : IDLCircle
    {
        internal DLCircle(int x, int y, int minRadius, int maxRadius, int speed)
        {
            _x = x;
            _y = y;
            _r = new Random().Next(minRadius, maxRadius);
            _speed = speed;
        }
        private int _x;
        private int _y;
        private int _r;
        private int _speed;

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
        }
        public int Speed
        {
            get => _speed;
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        public void RaisePropertyChanged(string propertyName)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
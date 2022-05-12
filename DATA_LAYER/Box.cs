using System;
using System.Collections.Generic;

namespace DATA_LAYER
{
    internal class Box
    {
        private readonly int _width;
        private readonly int _height;
        private readonly List<IDLCircle> _balls = new List<IDLCircle>();

        internal Box(int height, int width, int numberOfBalls, int minRadius, int maxRadius, int speed)
        {        
            _width = width;
            _height = height;
            generateBalls(numberOfBalls, minRadius, maxRadius, speed);
        }

        internal void deleteBalls()
        {
            this._balls.Clear();
        }    
    
        internal void generateBalls(int number, int minRadius, int maxRadius, int speed)
        {
            Random r = new Random();

            for (int i = 0; i < number; i++)
            {
                int x = r.Next(minRadius, _width - maxRadius);
                int y = r.Next(minRadius, _height - maxRadius);
                _balls.Add(new DLCircle(x, y, minRadius, maxRadius, speed));
            }
        }   
        internal int Width { get => _width; }
        internal int Height { get => _height; }
        internal List<IDLCircle> GetBalls()
        {
            return _balls;
        }
    }
}

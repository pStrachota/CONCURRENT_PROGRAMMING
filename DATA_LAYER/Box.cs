
namespace DATA_LAYER
{
    internal class Box
    {
        private readonly List<IDLCircle> _dllCircles = new();

        internal Box(int boxWidth, int boxHeight, int numberOfBalls, int minRadius, int maxRadius, int speed)
        {        
            GenerateDllCircles(boxWidth, boxHeight, numberOfBalls, minRadius, maxRadius, speed);
        }
    
        internal void GenerateDllCircles(int boxWidth, int boxHeight, int number, int minRadius, int maxRadius, int speed)
        {
            Random r = new();

            for (int i = 0; i < number; i++)
            {
                int x = r.Next(minRadius, boxWidth - maxRadius);
                int y = r.Next(minRadius, boxHeight - maxRadius);
                _dllCircles.Add(new DLCircle("Circle_" + i, x, y, minRadius, maxRadius, speed));
            }
        }   
        internal List<IDLCircle> GetDllCircles()
        {
            return _dllCircles;
        }
    }
}

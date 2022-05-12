
namespace DATA_LAYER
{
    internal class Box
    {
        private readonly List<IDLCircle> _dllCircles = new();
        public const int BOX_WIDTH = 1445;
        public const int BOX_HEIGHT = 504;

        internal Box(int numberOfBalls, int minRadius, int maxRadius, int speed)
        {        
            GenerateDllCircles(numberOfBalls, minRadius, maxRadius, speed);
        }
    
        internal void GenerateDllCircles(int number, int minRadius, int maxRadius, int speed)
        {
            Random r = new();

            for (int i = 0; i < number; i++)
            {
                int x = r.Next(minRadius, BOX_WIDTH - maxRadius);
                int y = r.Next(minRadius, BOX_HEIGHT - maxRadius);
                _dllCircles.Add(new DLCircle(x, y, minRadius, maxRadius, speed));
            }
        }   
        internal List<IDLCircle> GetDllCircles()
        {
            return _dllCircles;
        }
    }
}

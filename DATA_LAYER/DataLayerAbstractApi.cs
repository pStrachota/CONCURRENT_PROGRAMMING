using System.Data;

namespace DATA_LAYER;

public abstract class DataLayerAbstractApi
{
    public abstract List<IDLCircle> GetBallsFromBox(int height, int width, int numberOfBalls, int minRadius, int maxRadius, int speed);
    public static DataLayerAbstractApi? CreateLinq2Sql()
    {
        return new Linq2Balls();
    }

    private class Linq2Balls : DataLayerAbstractApi
    {
        public override List<IDLCircle> GetBallsFromBox(int height, int width, int numberOfBalls, int minRadius, int maxRadius, int speed)
        {
            return new Box(height, width, numberOfBalls, minRadius, maxRadius, speed).GetBalls();
        }
    }
}
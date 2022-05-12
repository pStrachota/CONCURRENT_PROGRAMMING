using System.Data;

namespace DATA_LAYER;

public abstract class DataLayerAbstractApi
{
    public abstract List<IDLCircle> GetDllCirclesFromBox(int numberOfBalls, int minRadius, int maxRadius, int speed);
    public static DataLayerAbstractApi? CreateLinq2DLCircles()
    {
        return new Linq2DLCircles();
    }

    private class Linq2DLCircles : DataLayerAbstractApi
    {
        public override List<IDLCircle> GetDllCirclesFromBox(int numberOfBalls, int minRadius, int maxRadius, int speed)
        {
            return new Box(numberOfBalls, minRadius, maxRadius, speed).GetDllCircles();
        }
    }
}
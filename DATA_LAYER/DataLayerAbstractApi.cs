using System.Data;

namespace DATA_LAYER;

public abstract class DataLayerAbstractApi
{
    public int BOX_WIDTH = 1445;
    public int BOX_HEIGHT = 504;

    public abstract List<IDLCircle> GetDllCirclesFromBox(int numberOfBalls, int minRadius, int maxRadius, int speed);
    public static DataLayerAbstractApi? CreateLinq2DLCircles()
    {
        return new Linq2DLCircles();
    }

    private class Linq2DLCircles : DataLayerAbstractApi
    {
        public override List<IDLCircle> GetDllCirclesFromBox(int numberOfBalls, int minRadius, int maxRadius, int speed)
        {
            return new Box(BOX_WIDTH, BOX_HEIGHT, numberOfBalls, minRadius, maxRadius, speed).GetDllCircles();
        }
    }
}
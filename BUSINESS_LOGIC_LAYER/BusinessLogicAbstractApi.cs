using DATA_LAYER;

namespace BUSINESS_LOGIC_LAYER;

public abstract class BusinessLogicAbstractApi
{
    public static BusinessLogicAbstractApi CreateLayer(DataLayerAbstractApi data = default(DataLayerAbstractApi))
    {
        return new BusinessLogic(data == null ? DataLayerAbstractApi.CreateLinq2Sql() : data);
    }
    
    public abstract void moveBall(BllCircle bllCircle);

    public abstract BllCircle CreateBllCircle(int width, int height);
    
    private class BusinessLogic : BusinessLogicAbstractApi
    {
        public BusinessLogic(DataLayerAbstractApi dataLayerAPI)
        {
            MyDataLayer = dataLayerAPI;
        }
        private readonly DataLayerAbstractApi MyDataLayer;
        public override void moveBall(BllCircle bllCircle)
        {
            Random random = new();
            int toMoveHorizontal = random.Next(10, 800);
            int toMoveVertical = random.Next(10, 400);
            
            int whichSide = random.Next(0, 200);
            int whichSide2 = random.Next(0, 200);

            if (whichSide > 100)
            {
                toMoveHorizontal = -toMoveHorizontal;
            }
            if (whichSide2 > 100)
            {
                toMoveVertical = -toMoveVertical;
            }
            if (toMoveHorizontal + bllCircle.startingLeftPosition + bllCircle.wide > 1500
                || toMoveHorizontal + bllCircle.startingLeftPosition - bllCircle.wide < 80)
            {
                toMoveHorizontal = 0;
            }

            if (toMoveVertical + bllCircle.startingTopPosition + bllCircle.height > 750
                || toMoveVertical + bllCircle.startingTopPosition - bllCircle.height < 270)
            {
                toMoveVertical = 0;
            }

            bllCircle.lastLeftPosition = bllCircle.leftPosition;
            bllCircle.lastTopPosition = bllCircle.topPosition;

            bllCircle.leftPosition = toMoveHorizontal;
            bllCircle.topPosition = toMoveVertical;
        }

        public override BllCircle CreateBllCircle(int width, int height)
        {
            return new BllCircle(width, height);
        }
    }
}
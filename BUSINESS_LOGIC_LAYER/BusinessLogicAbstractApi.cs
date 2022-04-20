using DATA_LAYER;

namespace BUSINESS_LOGIC_LAYER;

public abstract class BusinessLogicAbstractApi
{
    public static BusinessLogicAbstractApi CreateLayer(DataLayerAbstractApi data = default(DataLayerAbstractApi))
    {
        return new BusinessLogic(data == null ? DataLayerAbstractApi.CreateLinq2Sql() : data);
    }
    
    private class BusinessLogic : BusinessLogicAbstractApi
    {
        public BusinessLogic(DataLayerAbstractApi dataLayerAPI)
        {
            MyDataLayer = dataLayerAPI;
        }
        private readonly DataLayerAbstractApi MyDataLayer;
    }
}
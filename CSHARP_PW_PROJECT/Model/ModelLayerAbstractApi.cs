using System;
using System.Windows.Media;
using System.Windows.Media.Animation;
using BUSINESS_LOGIC_LAYER;

namespace CSHARP_PW_PROJECT.Model;

public class ModelLayerAbstractApi
{
    public static ModelLayerAbstractApi CreateAPI(BusinessLogicAbstractApi _businessLogicAbstractApi = default)
    {
        return new ModelLayer(_businessLogicAbstractApi ?? BusinessLogicAbstractApi.CreateLayer());
    }

    public class ModelLayer : ModelLayerAbstractApi
    {
        public ModelLayer(BusinessLogicAbstractApi businessLogicAbstractApi)
        {
            _businessLogicAbstractApi = businessLogicAbstractApi;
        }

        private readonly BusinessLogicAbstractApi _businessLogicAbstractApi;
    }
}
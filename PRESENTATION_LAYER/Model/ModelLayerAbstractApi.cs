using System;
using System.Windows.Media;
using System.Windows.Media.Animation;
using BUSINESS_LOGIC_LAYER;

namespace CSHARP_PW_PROJECT.Model;

public abstract class ModelLayerAbstractApi
{
    public static ModelLayerAbstractApi CreateAPI(BusinessLogicAbstractApi _businessLogicAbstractApi = default)
    {
        return new ModelLayer(_businessLogicAbstractApi ?? BusinessLogicAbstractApi.CreateLayer());
    }
    
    public abstract void moveCircles(ModelCircle modelCircle, double circlesSpeed);
    public abstract ModelCircle CreateModelCircles(int wide, int height);

    public class ModelLayer : ModelLayerAbstractApi
    {
        public ModelLayer(BusinessLogicAbstractApi businessLogicAbstractApi)
        {
            _businessLogicAbstractApi = businessLogicAbstractApi;
        }
        
        private BllCircle readCircleData(ModelCircle modelCircle)
        {
            BllCircle bllCircle = new BllCircle(modelCircle.wide, modelCircle.height);
            bllCircle.leftPosition = modelCircle.leftPosition;
            bllCircle.topPosition = modelCircle.topPosition;
            bllCircle.lastLeftPosition = modelCircle.lastLeftPosition;
            bllCircle.lastTopPosition = modelCircle.lastTopPosition;
            bllCircle.startingTopPosition = modelCircle.startingTopPosition;
            bllCircle.startingLeftPosition = modelCircle.startingLeftPosition;
            return bllCircle;
        }

        private void readBallData(BllCircle bllCircle, ModelCircle modelCircle)
        {
            modelCircle.leftPosition = bllCircle.leftPosition;
            modelCircle.topPosition = bllCircle.topPosition;
            modelCircle.lastLeftPosition = bllCircle.lastLeftPosition;
            modelCircle.lastTopPosition = bllCircle.lastTopPosition;
            modelCircle.startingTopPosition = bllCircle.startingTopPosition;
            modelCircle.startingLeftPosition = bllCircle.startingLeftPosition;
        }

        private readonly BusinessLogicAbstractApi _businessLogicAbstractApi;
        public override void moveCircles(ModelCircle modelCircle, double circlesSpeed)
        {
            BllCircle bllCircle = readCircleData(modelCircle);
            _businessLogicAbstractApi.moveBllCircle(bllCircle);
            readBallData(bllCircle, modelCircle);
            
            DoubleAnimation anim1 = new(modelCircle.lastTopPosition, modelCircle.topPosition, TimeSpan.FromSeconds(circlesSpeed));
            DoubleAnimation anim2 = new(modelCircle.lastLeftPosition, modelCircle.leftPosition, TimeSpan.FromSeconds(circlesSpeed));

            modelCircle.RenderTransform.BeginAnimation(TranslateTransform.XProperty, anim2);
            modelCircle.RenderTransform.BeginAnimation(TranslateTransform.YProperty, anim1);
        }

        public override ModelCircle CreateModelCircles(int wide, int height)
        {
            return new ModelCircle(_businessLogicAbstractApi.CreateBllCircle(wide, height));
        }
    }
}
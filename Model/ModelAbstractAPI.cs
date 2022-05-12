using Logic;
using System.Collections.ObjectModel;

namespace Model
{
    public abstract class ModelAbstractApi
    {
        public static ModelAbstractApi CreateAPI(BusinessLogicAbstractApi logicLayer = default)
        {
            return new ModelLayer(logicLayer ?? BusinessLogicAbstractApi.CreateAPI());
        }

        public abstract void CreateCirclesForPresentation(int numberOfBalls, int minRadius, int maxRadius, int speed);
        public abstract void StartCirclesMove();
        public abstract void StopCirclesMove();
        public abstract void DeleteCircles();

        public ObservableCollection<IMLCircle> Circles
        {
            get => circles;
            set => circles = value;
        }

        private ObservableCollection<IMLCircle> circles = new();

        internal class ModelLayer : ModelAbstractApi
        {
            public ModelLayer(BusinessLogicAbstractApi logicLayer)
            {
                MyLogicLayer = logicLayer;
            }

            public override void CreateCirclesForPresentation(int numberOfBalls,
                int minRadius, int maxRadius, int speed)
            {
                MyLogicLayer.RemoveCircles();
                MyLogicLayer.StopBllCircles();
                MyLogicLayer.CreateBox(numberOfBalls, minRadius, maxRadius, speed);
                circles.Clear();
                foreach (IBLCircle ball in MyLogicLayer.GetBllCircles())
                {
                    circles.Add(new MLCircle(ball));
                }

                StartCirclesMove();
            }

            public override void StartCirclesMove()
            {
                MyLogicLayer.StartMovingBalls();              
            }

            public override void StopCirclesMove()
            {
                MyLogicLayer.StopBllCircles();
            }

            public override void DeleteCircles()
            {
                MyLogicLayer.RemoveCircles();
            }

            private readonly BusinessLogicAbstractApi MyLogicLayer;
        }
    }
}
using Logic;
using System.Collections.ObjectModel;
using System.Threading;
using System.Windows;


namespace Model
{
    public abstract class ModelLayerAbstractAPI
    {
        public static ModelLayerAbstractAPI CreateAPI(LogicLayerAbstractAPI logicLayer = default)
        {
            return new ModelLayer(logicLayer ?? LogicLayerAbstractAPI.CreateAPI());
        }

        public abstract void generateBallsRepresentative(int height, int width, int numberOfBalls, int minRadius, int maxRadius, int speed);
        public abstract void startSimulation();
        public abstract void stopSimulation();
        public abstract void deleteCircles();
        public abstract void resumeCircles();

        public ObservableCollection<IMLCircle> Circles
        {
            get => circles;
            set => circles = value;
        }

        private ObservableCollection<IMLCircle> circles = new ObservableCollection<IMLCircle>();


        internal class ModelLayer : ModelLayerAbstractAPI
        {
            public ModelLayer(LogicLayerAbstractAPI logicLayer)
            {
                MyLogicLayer = logicLayer;
            }


            public override void generateBallsRepresentative(int height, int width, int numberOfBalls,
                int minRadius, int maxRadius, int speed)
            {
                MyLogicLayer.removeCircles();
                MyLogicLayer.DestroyThreads();
                MyLogicLayer.CreateBox(height, width, numberOfBalls, minRadius, maxRadius, speed);
                circles.Clear();
                foreach (IBLCircle ball in MyLogicLayer.GetBallBlls())
                {
                    circles.Add(new MLCircle(ball));
                }

                startSimulation();
            }

            public override void startSimulation()
            {
                MyLogicLayer.StartMovingBalls();              
            }

            public override void stopSimulation()
            {
                MyLogicLayer.DestroyThreads();
            }

            public override void deleteCircles()
            {
                MyLogicLayer.removeCircles();
            }

            public override void resumeCircles()
            {
                MyLogicLayer.ResumeBalls();
            }


            private readonly LogicLayerAbstractAPI MyLogicLayer;
        }
    }
}
using System.Diagnostics;
using DATA_LAYER;

namespace Logic
{
    public abstract class BusinessLogicAbstractApi
    {

        public static BusinessLogicAbstractApi CreateAPI(DataLayerAbstractApi data = default)
        {
            return new BusinessLogic(data ?? DataLayerAbstractApi.CreateLinq2DLCircles());
        }

        public abstract void RemoveCircles();
        public abstract List<IBLCircle> GetBllCircles();
        public abstract void UpdateBLCircle(IBLCircle circle, double time);
        public abstract void CreateBox(int numberOfBalls, int minRadius, int maxRadius, int speed);
        public abstract void StartMovingBalls();
        public abstract void StopBllCircles();
        public abstract void BllCircleUpdate(IBLCircle blCircle, double time);
        private List<Thread> threads = new();
        private bool isMoving = false;
        private static readonly object locker = new();
        private List<IBLCircle> ballBlls = new();

        internal class BusinessLogic : BusinessLogicAbstractApi
        {
            internal BusinessLogic(DataLayerAbstractApi dataLayerAbstractAPI)
            {
                _dataLayerAbstractApi = dataLayerAbstractAPI;
            }

            public override void CreateBox(int numberOfBalls, int minRadius, int maxRadius, int speed)
            {
                _dataLayerAbstractApi.GetLogger().Information("Creating box with {numberOfBalls} balls, " +
                   "{minRadius} min radius, {maxRadius} max radius, {speed} speed", numberOfBalls, minRadius, maxRadius, speed);

                List<IDLCircle> balls = _dataLayerAbstractApi.GetDllCirclesFromBox(numberOfBalls, minRadius, maxRadius, speed);

                foreach (IDLCircle ball in balls)
                {
                    IBLCircle ballBll = new BLCircle(ball);
                    ballBlls.Add(ballBll);

                    _dataLayerAbstractApi.GetLogger().Information("Created new IBLCircle: {@ballBll}", ballBll);

                }

                foreach (IBLCircle ballBll in ballBlls)
                {
                    Stopwatch stopwatch = new();
                    stopwatch.Start();

                    Thread t = new(() =>
                    {
                        while (isMoving)
                        {
                            BllCircleUpdate(ballBll, stopwatch.ElapsedMilliseconds / 50);
                        }
                    });
                    threads.Add(t);
                }
            }

            public override void StartMovingBalls()
            {
                _dataLayerAbstractApi.GetLogger().Information("Starting moving IBLCircles");

                if (threads.Count > 0)
                {
                    if (!isMoving)
                    {
                        isMoving = true;
                        foreach (Thread t in threads)
                        {
                            t.Start();
                        }
                    }
                }
            }

            public override void StopBllCircles()
            {
                isMoving = false;
                _dataLayerAbstractApi.GetLogger().Information("Stopping moving IBLCircles");
            }

            public override List<IBLCircle> GetBllCircles()
            {
                return ballBlls;
            }

            public override void UpdateBLCircle(IBLCircle circle, double time)
            {
                lock (locker)
                {
                    double timeElapsed = time - circle.LastUpdate;

                    ballBlls.ForEach(entity =>
                    {
                        var distance = Math.Sqrt((Math.Pow(circle.X - entity.X, 2) + Math.Pow(circle.Y - entity.Y, 2)));

                        var nextX = circle.X + circle.VelocityX * timeElapsed;
                        var nextY = circle.Y + circle.VelocityY * timeElapsed;
                        var nextEntityX = entity.X + entity.VelocityX * timeElapsed;
                        var nextEntityY = entity.Y + entity.VelocityY * timeElapsed;
                        var nextDistance = Math.Sqrt((Math.Pow(nextX - nextEntityX, 2) + Math.Pow(nextY - nextEntityY, 2)));

                        if (distance <= circle.R + entity.R && nextDistance < distance)
                        {
                            _dataLayerAbstractApi.GetLogger().Information("Collision between {@circle} " +
                               "and {@entity}", circle, entity);

                            var v1 = Math.Sqrt((circle.VelocityX * circle.VelocityX) + (circle.VelocityY * circle.VelocityY));
                            var v2 = Math.Sqrt((entity.VelocityX * entity.VelocityX) + (entity.VelocityY * entity.VelocityY));

                            double theta1 = Math.Atan2(circle.VelocityY, circle.VelocityX),
                                theta2 = Math.Atan2(entity.VelocityY, entity.VelocityX),
                                phi = Math.Atan2(circle.Y - entity.Y,
                                    circle.X - entity.X);

                            circle.VelocityX = (int)CalculateDx(v1, v2, circle.Mass, entity.Mass, theta1, theta2, phi);
                            circle.VelocityY = (int)CalculateDy(v1, v2, circle.Mass, entity.Mass, theta1, theta2, phi);
                            entity.VelocityX = (int)CalculateDx(v2, v1, entity.Mass, circle.Mass, theta2, theta1, phi);
                            entity.VelocityY = (int)CalculateDy(v2, v1, entity.Mass, circle.Mass, theta2, theta1, phi);
                        }
                    });
                }
            }

            double CalculateDx(double v1, double v2, double m1, double m2, double theta1,
                  double theta2, double phi)
            {
                return (v1 * Math.Cos(theta1 - phi) * (m1 - m2) + 2 * m2 * v2 * Math.Cos(theta2 - phi))
                    / (m1 + m2) * Math.Cos(phi) + v1 * Math.Sin(theta1 - phi) * Math.Cos(phi + Math.PI / 2);
            }

            double CalculateDy(double v1, double v2, double m1, double m2, double theta1,
                double theta2, double phi)
            {
                return (v1 * Math.Cos(theta1 - phi) * (m1 - m2) + 2 * m2 * v2 * Math.Cos(theta2 - phi))
                    / (m1 + m2) * Math.Sin(phi) + v1 * Math.Sin(theta1 - phi) * Math.Sin(phi + Math.PI / 2);
            }

            public override void RemoveCircles()
            {
                threads.Clear();
                ballBlls.Clear();

                _dataLayerAbstractApi.GetLogger().Information("Box cleared");
            }

            public override void BllCircleUpdate(IBLCircle blCircle, double time)
            {
                double timeElapsed = time - blCircle.LastUpdate;

                int newLocationX = (int)(blCircle.X + blCircle.VelocityX * timeElapsed);
                int newLocationY = (int)(blCircle.Y + blCircle.VelocityY * timeElapsed);

                if (newLocationX != blCircle.X)
                {
                    _dataLayerAbstractApi.GetLogger().Information($"{blCircle.Name} moved X " +
                        $"from {blCircle.X} to {newLocationX}");
                }

                if (newLocationY != blCircle.Y)
                {
                    _dataLayerAbstractApi.GetLogger().Information($"{blCircle.Name} moved Y " +
                        $"from {blCircle.Y} to {newLocationY}");
                }

                if (newLocationX - blCircle.R > 0 && newLocationX + blCircle.R < _dataLayerAbstractApi.BOX_WIDTH)
                {
                    blCircle.X = newLocationX;
                }
                else
                {
                    blCircle.VelocityX = -blCircle.VelocityX;
                    _dataLayerAbstractApi.GetLogger().Information("{@blCircle} hit the wall", blCircle);
                }

                if (newLocationY - blCircle.R > 0 && newLocationY + blCircle.R < _dataLayerAbstractApi.BOX_HEIGHT)
                {
                    blCircle.Y = newLocationY;
                }
                else
                {
                    blCircle.VelocityY = -blCircle.VelocityY;
                    _dataLayerAbstractApi.GetLogger().Information("{@blCircle} hit the wall", blCircle);
                }

                UpdateBLCircle(blCircle, time);

                blCircle.LastUpdate = time;
            }

            private readonly DataLayerAbstractApi _dataLayerAbstractApi;
        }
    }

}
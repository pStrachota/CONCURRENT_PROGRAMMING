using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using DATA_LAYER;

namespace Logic
{
    public abstract class LogicLayerAbstractAPI
    {
        public static LogicLayerAbstractAPI CreateAPI(DataLayerAbstractApi data = default)
        {
            return new LogicLayer(data ?? DataLayerAbstractApi.CreateLinq2Sql());
        }

        public abstract void removeCircles();
        public abstract List<IBLCircle> GetBallBlls();
        public abstract void UpdateBLCircle(IBLCircle circle, double time);
        public abstract void CreateBox(int height, int width, int numberOfBalls, int minRadius, int maxRadius, int speed);
        public abstract void StartMovingBalls();
        public abstract void DestroyThreads();
        public abstract void ballUpdate(IBLCircle blCircle, double time);
        public abstract void ResumeBalls();
        private List<Task> threads = new List<Task>();
        private bool isMoving = false;
        //Stopwatch stopwatch = new Stopwatch();
        private static readonly object locker = new object();
        private List<IBLCircle> ballBlls = new List<IBLCircle>();

      
        
        internal class LogicLayer : LogicLayerAbstractAPI
        {
            internal LogicLayer(DataLayerAbstractApi dataLayerAbstractAPI)
            {
                //stopwatch.Start();
                MyDataLayer = dataLayerAbstractAPI;
            }

            public override void CreateBox(int height, int width, int numberOfBalls, int minRadius, int maxRadius, int speed)
            {
                List<IDLCircle> balls = MyDataLayer.GetBallsFromBox(height, width, numberOfBalls, minRadius, maxRadius, speed);

                foreach (IDLCircle ball in balls)
                {
                    IBLCircle ballBll = new BLCircle(ball);
                    ballBlls.Add(ballBll);
                }

                foreach (IBLCircle ballBll in ballBlls)
                {
                    Stopwatch stopwatch = new Stopwatch();
                    stopwatch.Start();

                    Task t = new Task(() =>
                    {                                          
                        while (isMoving)
                        {
                            //to ponizej odpowiada za predkosc poruszania kulek
                            //im mniej jest po znaku dzielenia, tym szybciej sie poruszaja
                            ballUpdate(ballBll, stopwatch.ElapsedMilliseconds / 50);
                            //niewiem dlaczego, ale jak sie zakomentuje ten kod ponizej
                            //to kulki zaczynaja sie do siebie przyczepiac...
                            //i potem juz sie nie ruszaja
                          //  Thread.Sleep(6);
                        }
                    });
                    threads.Add(t);
                }
            }



            public override void StartMovingBalls()
            {
                if(threads.Count > 0)
                {
                    if (!isMoving)
                    {
                        isMoving = true;
                        foreach (Task t in threads)
                        {
                            t.Start();
                            //t.Sleep(22);
                        }
                    }
                }
                

             
            }
            public override void ResumeBalls()
            {
                if (!isMoving)
                {
                    isMoving = true;
                }
            }
            
            

            public override void DestroyThreads()
            {
                //if (threads.Count > 0)
                //{
                //    isMoving = false;
                //}
              //  Thread.Sleep(10000);

                    isMoving = false;
            }

            public override List<IBLCircle> GetBallBlls()
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
                            var v1 = Math.Sqrt((circle.VelocityX * circle.VelocityX) + (circle.VelocityY * circle.VelocityY));
                            var v2 = Math.Sqrt((entity.VelocityX * entity.VelocityX) + (entity.VelocityY * entity.VelocityY));

                            double theta1 = Math.Atan2(circle.VelocityY, circle.VelocityX),
                                theta2 = Math.Atan2(entity.VelocityY, entity.VelocityX),
                                phi = Math.Atan2(circle.Y - entity.Y,
                                    circle.X - entity.X);

                            circle.VelocityX = (int)calculateDx(v1, v2, circle.Mass, entity.Mass, theta1, theta2, phi);
                            circle.VelocityY = (int)calculateDy(v1, v2, circle.Mass, entity.Mass, theta1, theta2, phi);
                            entity.VelocityX = (int)calculateDx(v2, v1, entity.Mass, circle.Mass, theta2, theta1, phi);
                            entity.VelocityY = (int)calculateDy(v2, v1, entity.Mass, circle.Mass, theta2, theta1, phi);
                        }
                    });
                }

                //_lastUpdate = stopwatch.ElapsedMilliseconds / 50;

            }

            double calculateDx(double v1, double v2, double m1, double m2, double theta1,
                  double theta2, double phi)
            {
                return (v1 * Math.Cos(theta1 - phi) * (m1 - m2) + 2 * m2 * v2 * Math.Cos(theta2 - phi))
                    / (m1 + m2) * Math.Cos(phi) + v1 * Math.Sin(theta1 - phi) * Math.Cos(phi + Math.PI / 2);
            }

            double calculateDy(double v1, double v2, double m1, double m2, double theta1,
                double theta2, double phi)
            {
                return (v1 * Math.Cos(theta1 - phi) * (m1 - m2) + 2 * m2 * v2 * Math.Cos(theta2 - phi))
                    / (m1 + m2) * Math.Sin(phi) + v1 * Math.Sin(theta1 - phi) * Math.Sin(phi + Math.PI / 2);
            }

            public override void removeCircles()
            {
                threads.Clear();
                ballBlls.Clear();
            }

            public override void ballUpdate(IBLCircle blCircle, double time)
            {
                double timeElapsed = time - blCircle.LastUpdate;

                int newLocationX = (int)(blCircle.X + blCircle.VelocityX * timeElapsed);
                int newLocationY = (int)(blCircle.Y + blCircle.VelocityY * timeElapsed);

                if (newLocationX - blCircle.R > 0 && newLocationX + blCircle.R < 1445)
                {
                    blCircle.X = newLocationX;
                }
                else
                {
                    blCircle.VelocityX = -blCircle.VelocityX;
                }

                if (newLocationY - blCircle.R > 0 && newLocationY + blCircle.R < 504)
                {
                    blCircle.Y = newLocationY;
                }
                else
                {
                    blCircle.VelocityY = -blCircle.VelocityY;
                }

                lock (locker)
                {
                    UpdateBLCircle(blCircle, time);
                }

                blCircle.LastUpdate = time;
            }

            private readonly DataLayerAbstractApi MyDataLayer;
            private double _lastUpdate = 0;
        }
    }



}
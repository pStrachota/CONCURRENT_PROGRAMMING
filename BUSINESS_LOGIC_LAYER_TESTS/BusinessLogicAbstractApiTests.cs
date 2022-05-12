using Logic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace BUSINESS_LOGIC_TESTS
{
    [TestClass]
    public class BusinessLogicTests
    {

        BusinessLogicAbstractApi _logicLayerAbstractAPI;

        [TestInitialize]
        public void Initialize()
        {
            _logicLayerAbstractAPI = BusinessLogicAbstractApi.CreateAPI();
        }

        [TestMethod]
        public void CheckIf_LogicLayerIsNotNull_AfterInitialization()
        {
            Assert.IsNotNull(_logicLayerAbstractAPI);

        }

        [TestMethod]
        public void CheckIf_BllCirclesIsNotNull_AfterInitialization()
        {
            Assert.IsNotNull(_logicLayerAbstractAPI.GetBllCircles());
        }

        [TestMethod]
        public void CheckIf_SeparateBallsFromBox_AreNotEqual()
        {
            _logicLayerAbstractAPI.CreateBox(2, 20, 50, 20);
            var ball = _logicLayerAbstractAPI.GetBllCircles().ElementAt(0);
            var AnotherBall = _logicLayerAbstractAPI.GetBllCircles().ElementAt(1);

            System.Console.WriteLine(ball.VelocityX);

            Assert.IsNotNull(ball);
            Assert.IsNotNull(AnotherBall);
            Assert.AreNotEqual(ball, AnotherBall);

            _logicLayerAbstractAPI.StartMovingBalls();

            var ball2 = _logicLayerAbstractAPI.GetBllCircles().ElementAt(0);
            System.Console.WriteLine(ball2.VelocityX);

            _logicLayerAbstractAPI.BllCircleUpdate(ball2, 5000);

            System.Console.WriteLine(ball2.VelocityX);

        }
        [TestMethod]
        public void CheckIf_BallChangedPosition_AfterMoved()
        {
            _logicLayerAbstractAPI.CreateBox(1, 20, 50, 20);
            var ball = _logicLayerAbstractAPI.GetBllCircles().ElementAt(0);
            var ballY = ball.VelocityY;
            var ballX = ball.VelocityX;

            _logicLayerAbstractAPI.BllCircleUpdate(ball, 500);

            Assert.AreNotEqual(ballY, ball.VelocityY);
            Assert.AreNotEqual(ballX, ball.VelocityX);
        }
    }
}